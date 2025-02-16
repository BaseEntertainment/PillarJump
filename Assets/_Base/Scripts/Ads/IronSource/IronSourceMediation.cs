using com.unity3d.mediation;
using System;
using UnityEngine;

public sealed class IronSourceMediation : AdMediation
{
	public override bool IsInterstitialReady => InterstitialAd.IsAdReady();
	public override bool IsRewardedVideoAvailable => IronSource.Agent.isRewardedVideoAvailable();

	public LevelPlayInterstitialAd InterstitialAd { get; private set; }
	public LevelPlayBannerAd BannerAd { get; private set; }

	private Action _tempOnRewardedVideoAdRewarded;

	private void OnApplicationPause(bool isPaused)
	{
		IronSource.Agent.onApplicationPause(isPaused);
	}

	private void OnEnable()
	{
		if (Instance != this)
		{
			return;
		}

		IronSourceEvents.onSdkInitializationCompletedEvent += OnInitializationFinished;
		IronSourceRewardedVideoEvents.onAdRewardedEvent += OnRewardedVideoAdRewarded;
	}

	private void OnDisable()
	{
		if (Instance != this)
		{
			return;
		}

		IronSourceEvents.onSdkInitializationCompletedEvent -= OnInitializationFinished;
		IronSourceRewardedVideoEvents.onAdRewardedEvent -= OnRewardedVideoAdRewarded;
	}

	#region Initialization
	public override void Initialize()
	{
		base.Initialize();

		InterstitialAd = new(nameof(InterstitialAd));
		InterstitialAd.OnAdClosed += OnInterstitialAdClosed;

		BannerAd = new(nameof(BannerAd), position: LevelPlayBannerPosition.BottomCenter, displayOnLoad: true);

		IronSource.Agent.validateIntegration();
		IronSource.Agent.shouldTrackNetworkState(true);

		LevelPlay.OnInitSuccess += SdkInitializationCompletedEvent;
		LevelPlay.OnInitFailed += SdkInitializationFailedEvent;

		IronSource.Agent.init(AppKey);
	}

	public override void SetConsent(bool consent)
	{
		IronSource.Agent.setConsent(consent);
	}

	private void OnInitializationFinished()
	{
		Debug.Log("IronSource: Initialization Finished.");

		LoadInterstitial();
	}

	private void SdkInitializationCompletedEvent(LevelPlayConfiguration configuration)
	{
		Debug.Log("IronSource: Initialization Completed.");

		LoadInterstitial();
	}

	private void SdkInitializationFailedEvent(LevelPlayInitError error)
	{
		Debug.LogWarning("IronSource: Initialization Failed.");
	}
	#endregion

	#region Interstitial
	public override void ShowInterstitial()
	{
		if (IsInterstitialReady)
		{
			InterstitialAd.ShowAd();
		}
		else
		{
			LoadInterstitial();

			if (IsInterstitialReady)
			{
				ShowInterstitial();
			}
		}
	}
	private void LoadInterstitial()
	{
		InterstitialAd.LoadAd();
	}

	private void OnInterstitialAdClosed(LevelPlayAdInfo info)
	{
		LoadInterstitial();
	}
	#endregion

	#region Rewarded
	public override void ShowRewardedVideo(Action onRewardedVideoAdRewarded)
	{
		if (IsRewardedVideoAvailable)
		{
			_tempOnRewardedVideoAdRewarded = onRewardedVideoAdRewarded;

			IronSource.Agent.showRewardedVideo();
		}
	}

	private void OnRewardedVideoAdRewarded(IronSourcePlacement placement, IronSourceAdInfo info)
	{
		_tempOnRewardedVideoAdRewarded?.Invoke();
		_tempOnRewardedVideoAdRewarded = null;
	}
	#endregion

	#region Banner
	public void LoadBanner()
	{
		BannerAd.LoadAd();
	}

	public override void ShowBanner()
	{
		BannerAd.ShowAd();
	}

	public override void HideBanner()
	{
		BannerAd.HideAd();
	}
	#endregion
}
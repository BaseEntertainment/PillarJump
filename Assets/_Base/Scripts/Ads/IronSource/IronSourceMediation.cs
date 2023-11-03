using System;
using UnityEngine;

public sealed class IronSourceMediation : AdMediation
{
	public override bool IsRewardedVideoAvailable => IronSource.Agent.isRewardedVideoAvailable();
	public override bool IsInterstitialReady => IronSource.Agent.isInterstitialReady();

	private Action _tempOnRewardedVideoAdRewarded;

	void OnApplicationPause(bool isPaused) => IronSource.Agent.onApplicationPause(isPaused);

	private void OnEnable()
	{
		IronSourceEvents.onSdkInitializationCompletedEvent += OnInitializationFinished;

		IronSourceRewardedVideoEvents.onAdRewardedEvent += OnRewardedVideoAdRewarded;

		IronSourceInterstitialEvents.onAdClosedEvent += OnInterstitialAdClosed;
	}

	private void OnDisable()
	{
		IronSourceEvents.onSdkInitializationCompletedEvent -= OnInitializationFinished;

		IronSourceRewardedVideoEvents.onAdRewardedEvent -= OnRewardedVideoAdRewarded;

		IronSourceInterstitialEvents.onAdClosedEvent -= OnInterstitialAdClosed;
	}

	public override void Initialize()
	{
		base.Initialize();

		IronSource.Agent.validateIntegration();
		IronSource.Agent.shouldTrackNetworkState(true);
		IronSource.Agent.init(AppKey);
	}

	public override void ShowBanner() => IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM);

	public override void HideBanner() => IronSource.Agent.destroyBanner();

	public void DisplayLoadedBanner() => IronSource.Agent.displayBanner();

	public void HideLoadedBanner() => IronSource.Agent.hideBanner();

	public override void ShowInterstitial()
	{
		if (IsInterstitialReady)
		{
			IronSource.Agent.showInterstitial();
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

	public override void ShowRewardedVideo(Action onRewardedVideoAdRewarded)
	{
		if (IsRewardedVideoAvailable)
		{
			_tempOnRewardedVideoAdRewarded = onRewardedVideoAdRewarded;

			IronSource.Agent.showRewardedVideo();
		}
	}

	private void OnInitializationFinished()
	{
		Debug.Log("IronSource: Initialization Finished.");

		LoadInterstitial();
	}

	private void OnRewardedVideoAdRewarded(IronSourcePlacement placement, IronSourceAdInfo info)
	{
		_tempOnRewardedVideoAdRewarded?.Invoke();
		_tempOnRewardedVideoAdRewarded = null;
	}

	private void OnInterstitialAdClosed(IronSourceAdInfo info) => LoadInterstitial();

	private void LoadInterstitial() => IronSource.Agent.loadInterstitial();

	public override void SetConsent(bool consent) => IronSource.Agent.setConsent(consent);
}
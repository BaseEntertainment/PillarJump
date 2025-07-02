using System;
using Unity.Services.LevelPlay;
using UnityEngine;

public sealed class IronSourceMediation : AdMediation
{
	public override bool IsRewardedVideoAvailable => RewardedAd != null && RewardedAd.IsAdReady();
	public override bool IsInterstitialReady => InterstitialAd != null && InterstitialAd.IsAdReady();

	public LevelPlayRewardedAd RewardedAd { get; private set; }
	public LevelPlayInterstitialAd InterstitialAd { get; private set; }
	public LevelPlayBannerAd BannerAd { get; private set; }

	private Action _tempOnRewardedVideoAdRewarded;

	#region Initialization
	public override void Initialize()
	{
		base.Initialize();

		if (string.IsNullOrEmpty(RewardedAdUnitID) == false)
		{
			RewardedAd = new(RewardedAdUnitID);
			RewardedAd.OnAdLoaded += OnAdLoadedSuccessfully;
			RewardedAd.OnAdLoadFailed += OnAdLoadFailed;
			RewardedAd.OnAdRewarded += OnRewardedVideoAdRewarded;
		}

		if (string.IsNullOrEmpty(InterstitialAdUnitID) == false)
		{
			InterstitialAd = new(InterstitialAdUnitID);
			InterstitialAd.OnAdLoaded += OnAdLoadedSuccessfully;
			InterstitialAd.OnAdLoadFailed += OnAdLoadFailed;
			InterstitialAd.OnAdClosed += OnInterstitialAdClosed;
		}

		if (string.IsNullOrEmpty(BannerAdUnitID) == false)
		{
			BannerAd = new(BannerAdUnitID, displayOnLoad: true);
			BannerAd.OnAdLoaded += OnAdLoadedSuccessfully;
			BannerAd.OnAdLoadFailed -= OnAdLoadFailed;
		}

		LevelPlay.OnInitSuccess += OnLevelPlayInitSuccess;
		LevelPlay.OnInitFailed += OnLevelPlayInitFailed;

		LevelPlay.Init(AppKey);
	}

	public override void SetConsent(bool consent)
	{
		LevelPlay.SetConsent(consent);
	}

	private void OnLevelPlayInitSuccess(LevelPlayConfiguration configuration)
	{
		Debug.Log("LevelPlay: Initialization success.");

		LoadRewardedAd();
		LoadInterstitial();
	}

	private void OnLevelPlayInitFailed(LevelPlayInitError error)
	{
		Debug.LogWarning("LevelPlay: Initialization Failed.");
	}
	#endregion

	#region AdLoading
	private void OnAdLoadedSuccessfully(LevelPlayAdInfo info)
	{
		Debug.Log($"OnRewardedAdLoaded ({info.AdFormat}): {info}");
	}

	private void OnAdLoadFailed(LevelPlayAdError error)
	{
		Debug.Log($"OnRewardedAdLoadFailed ({error.AdUnitId}): {error.ErrorMessage}");
	}
	#endregion

	#region Rewarded Ad
	private void OnRewardedVideoAdRewarded(LevelPlayAdInfo info, LevelPlayReward reward)
	{
		_tempOnRewardedVideoAdRewarded?.Invoke();
		_tempOnRewardedVideoAdRewarded = null;
	}

	public override void ShowRewardedVideo(Action onRewardedVideoAdRewarded)
	{
		if (IsRewardedVideoAvailable)
		{
			_tempOnRewardedVideoAdRewarded = onRewardedVideoAdRewarded;
			RewardedAd?.ShowAd();
		}
		else
		{
			LoadRewardedAd();

			if (IsRewardedVideoAvailable)
			{
				ShowRewardedVideo(onRewardedVideoAdRewarded);
			}
		}
	}

	private void LoadRewardedAd()
	{
		RewardedAd?.LoadAd();
	}
	#endregion

	#region Interstitial Ad
	private void OnInterstitialAdClosed(LevelPlayAdInfo info)
	{
		LoadInterstitial();
	}

	public override void ShowInterstitial()
	{
		if (IsInterstitialReady)
		{
			InterstitialAd?.ShowAd();
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
		InterstitialAd?.LoadAd();
	}
	#endregion

	#region Banner Ad
	public void LoadBannerAd()
	{
		BannerAd?.LoadAd();
	}

	public override void ShowBanner()
	{
		BannerAd?.ShowAd();
	}

	public override void HideBanner()
	{
		BannerAd?.HideAd();
	}
	#endregion
}
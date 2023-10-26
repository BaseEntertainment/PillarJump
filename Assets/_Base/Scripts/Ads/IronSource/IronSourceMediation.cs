using System;
using UnityEngine;

public sealed class IronSourceMediation : MonoBehaviour, IAdMediation
{
	[Header("API Keys"), Space(5)]
	[SerializeField] private string _appKeyAndroid;
	[SerializeField] private string _appKeyIOS;

	[Header("Initialization"), Space(5)]
	[SerializeField] private bool _initializeOnAwake = true;

	public bool IsRewardedVideoAvailable => IronSource.Agent.isRewardedVideoAvailable();
	public bool IsInterstitialReady => IronSource.Agent.isInterstitialReady();

	public string AppKey { get; private set; }

	private Action _tempOnRewardedVideoAdRewarded;

	void OnApplicationPause(bool isPaused) => IronSource.Agent.onApplicationPause(isPaused);

	private void Awake()
	{
		if (_initializeOnAwake)
		{
			Initialize();
		}
	}

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

	public void Initialize()
	{
		SetAppKey();

		IronSource.Agent.validateIntegration();
		IronSource.Agent.shouldTrackNetworkState(true);
		IronSource.Agent.init(AppKey);
	}

	public void ShowBanner()
	{
		IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM);
	}

	public void HideBanner()
	{
		IronSource.Agent.destroyBanner();
	}

	public void DisplayLoadedBanner()
	{
		IronSource.Agent.displayBanner();
	}

	public void HideLoadedBanner()
	{
		IronSource.Agent.hideBanner();
	}

	public void ShowInterstitial()
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

	public void ShowRewardedVideo(Action onRewardedVideoAdRewarded)
	{
		if (IsRewardedVideoAvailable)
		{
			_tempOnRewardedVideoAdRewarded = onRewardedVideoAdRewarded;

			IronSource.Agent.showRewardedVideo();
		}
	}

	public void SetConsent(bool consent)
	{
		IronSource.Agent.setConsent(consent);
	}

	private void SetAppKey()
	{
#if UNITY_ANDROID
		AppKey = _appKeyAndroid;
#elif UNITY_IOS
		AppKey = _appKeyIOS;
#endif
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

	private void OnInterstitialAdClosed(IronSourceAdInfo info)
	{
		LoadInterstitial();
	}

	private void LoadInterstitial()
	{
		IronSource.Agent.loadInterstitial();
	}
}
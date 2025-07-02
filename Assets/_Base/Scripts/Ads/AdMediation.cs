using System;
using UnityEngine;

public abstract class AdMediation : MonoBehaviour
{
	[Header("API Keys"), Space(5)]
	[SerializeField] private string _appKeyIOS;
	[SerializeField] private string _appKeyAndroid;

	[Header("Ad Units")]
	[Header("Rewarded")]
	[SerializeField] private string _IosRewardedAdUnitID;
	[SerializeField] private string _androidRewardedAdUnitID;

	[Header("Interstitial")]
	[SerializeField] private string _IosInterstitialAdUnitID;
	[SerializeField] private string _androidInterstitialAdUnitID;

	[Header("Banner")]
	[SerializeField] private string _androidBannerAdUnitID;
	[SerializeField] private string _IosBannerAdUnitID;

	[Header("Initialization"), Space(5)]
	[SerializeField] private bool _initializeOnAwake = true;
	[SerializeField] private bool _dontDestroyOnLoad = true;

	public static AdMediation Instance { get; private set; }

	public string AppKey { get; private set; }
	public string RewardedAdUnitID { get; private set; }
	public string InterstitialAdUnitID { get; private set; }
	public string BannerAdUnitID { get; private set; }

	public abstract bool IsRewardedVideoAvailable { get; }
	public abstract bool IsInterstitialReady { get; }

	protected void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;

		if (_dontDestroyOnLoad)
		{
			transform.parent = null;
			DontDestroyOnLoad(gameObject);
		}

		if (_initializeOnAwake)
		{
			Initialize();
		}
	}

	public virtual void Initialize()
	{
		SetAppKey();
		SetAdUnitsID();
	}

	public abstract void HideBanner();

	public abstract void ShowBanner();

	public abstract bool TryShowInterstitial();

	public abstract bool TryShowRewardedVideo(Action finished);

	public abstract void SetConsent(bool consent);

	private void SetAppKey()
	{
#if UNITY_ANDROID
		AppKey = _appKeyAndroid;
#elif UNITY_IOS
		AppKey = _appKeyIOS;
#endif
	}

	private void SetAdUnitsID()
	{
#if UNITY_ANDROID
		RewardedAdUnitID = _androidRewardedAdUnitID;
		InterstitialAdUnitID = _androidInterstitialAdUnitID;
		BannerAdUnitID = _androidBannerAdUnitID;
#elif UNITY_IOS
		RewardedAdUnitID = _IosRewardedAdUnitID;
		InterstitialAdUnitID = _IosInterstitialAdUnitID;
		BannerAdUnitID = _IosBannerAdUnitID;
#endif
	}
}
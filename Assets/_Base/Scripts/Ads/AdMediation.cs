using System;
using UnityEngine;

public abstract class AdMediation : MonoBehaviour
{
	[Header("API Keys"), Space(5)]
	[SerializeField] private string _appKeyAndroid;
	[SerializeField] private string _appKeyIOS;

	[Header("Initialization"), Space(5)]
	[SerializeField] private bool _initializeOnAwake = true;
	[SerializeField] private bool _dontDestroyOnLoad = true;

	public static AdMediation Instance { get; private set; }

	public string AppKey { get; private set; }

	public abstract bool IsRewardedVideoAvailable { get; }
	public abstract bool IsInterstitialReady { get; }

	private void Awake()
	{
		if (Instance != null)
		{
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

	public virtual void Initialize() => SetAppKey();

	public abstract void HideBanner();

	public abstract void ShowBanner();

	public abstract void ShowInterstitial();

	public abstract void ShowRewardedVideo(Action finished);

	public abstract void SetConsent(bool consent);

	private void SetAppKey()
	{
#if UNITY_ANDROID
		AppKey = _appKeyAndroid;
#elif UNITY_IOS
		AppKey = _appKeyIOS;
#endif
	}
}

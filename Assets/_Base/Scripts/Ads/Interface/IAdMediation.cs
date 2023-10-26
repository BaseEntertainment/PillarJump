using System;

public interface IAdMediation
{
	public void Initialize();

	public void ShowBanner();

	public void HideBanner();

	public void ShowInterstitial();

	public void ShowRewardedVideo(Action finished);

	public void SetConsent(bool consent);
}
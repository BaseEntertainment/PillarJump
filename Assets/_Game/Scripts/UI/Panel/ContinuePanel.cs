using UnityEngine;

public class ContinuePanel : MonoBehaviour
{
	[SerializeField] private GameUI _gameUI;

	public void OnClickContinue()
	{
		AdMediation.Instance.ShowRewardedVideo(OnRewardedVideoFinished);
	}

	private void OnRewardedVideoFinished()
	{
		_gameUI.ShowCountDownPanel();
	}
}

using System;
using UnityEngine;

public class ContinuePanel : MonoBehaviour
{
	[SerializeField] private GameUI _gameUI;

	public static event Action TappedContinue;

	public void OnClickContinue()
	{
		AdMediation.Instance.ShowRewardedVideo(OnRewardedVideoFinished);
	}

	private void OnRewardedVideoFinished()
	{
		_gameUI.ShowCountDownPanel();

		TappedContinue?.Invoke();
	}
}

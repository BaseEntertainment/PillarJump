using System;
using UnityEngine;
using UnityEngine.UI;

public class TapToPlayPanel : MonoBehaviour
{
	[SerializeField] private Button _button;

	[SerializeField] private GameUI _gameUI;
	[SerializeField] private Ball _ball;

	public static event Action TappedPlay;

	private void Awake()
	{
		ResetButton();
	}

	private void ResetButton()
	{
		_button.onClick.RemoveAllListeners();
		_button.onClick.AddListener(OnClick);
	}

	private void OnClick()
	{
		_gameUI.SwitchToGameplay();
		_ball.gameObject.SetActive(true);

		TappedPlay?.Invoke();
	}
}
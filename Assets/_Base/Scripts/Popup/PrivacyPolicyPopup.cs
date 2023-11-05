using System;
using UnityEngine;
using UnityEngine.UI;

public class PrivacyPolicyPopup : Popup
{
	[SerializeField] private Button _acceptButton;

	public void Show(Action onClickAccept)
	{
		ResetButton(onClickAccept);
		gameObject.SetActive(true);
	}

	private void ResetButton(Action onClickAccept)
	{
		_acceptButton.onClick.RemoveAllListeners();
		_acceptButton.onClick.AddListener(() => onClickAccept?.Invoke());
		_acceptButton.onClick.AddListener(() => gameObject.SetActive(false));
	}
}
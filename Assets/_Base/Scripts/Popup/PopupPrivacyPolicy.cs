using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopupPrivacyPolicy : Popup
{
	[Header("Links"), Space(5)]
	[SerializeField] private string _linkPrivacyPolicy;
	[SerializeField] private string _linkTermsOfUse;

	[Header("Buttons"), Space(5)]
	[SerializeField] private Button _buttonPrivacyPolicy;
	[SerializeField] private Button _buttonTermsOfUse;

	[Space(5)]
	[SerializeField] private Button _buttonAccept;
	[Space(5)]
	[SerializeField] private UnityEvent _onClickAccept;

	private void Awake()
	{
		ResetAcceptButton();
		ResetLinkButtons();
	}

	private void ResetAcceptButton()
	{
		_buttonAccept.onClick.RemoveAllListeners();
		_buttonAccept.onClick.AddListener(() => _onClickAccept?.Invoke());
	}

	private void ResetLinkButtons()
	{
		_buttonPrivacyPolicy.onClick.RemoveAllListeners();
		_buttonTermsOfUse.onClick.RemoveAllListeners();

		_buttonPrivacyPolicy.onClick.AddListener(() => OpenURL(_linkPrivacyPolicy));
		_buttonTermsOfUse.onClick.AddListener(() => OpenURL(_linkTermsOfUse));
	}

	private void OpenURL(string url)
	{
		Application.OpenURL(url);
	}
}

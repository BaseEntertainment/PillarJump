using UnityEngine;
using UnityEngine.UI;

public class PrivacyPolicyLinks : Popup
{
	[Header("Links"), Space(5)]
	[SerializeField] private string _linkPrivacyPolicy;
	[SerializeField] private string _linkTermsOfUse;

	[Header("Buttons"), Space(5)]
	[SerializeField] private Button _buttonPrivacyPolicy;
	[SerializeField] private Button _buttonTermsOfUse;

	private void Awake()
	{
		ResetLinkButtons();
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

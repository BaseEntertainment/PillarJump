using System;
using UnityEngine;

public class PrivacyPolicyConsent : MonoBehaviour
{
	[SerializeField] private PrivacyPolicyPopup _popup;

	private const string PLAYER_PREF_PRIVACY_POLICY_CONSENT_STATUS = "PRIVACY_POLICY_CONSENT_STATUS";

	private void Start()
	{
		var consentStatus = GetConsentStatus();

		if (consentStatus == true)
		{
			return;
		}

		_popup.Show(OnClickAccept);
	}

	private bool GetConsentStatus()
	{
		return PlayerPrefs.GetInt(PLAYER_PREF_PRIVACY_POLICY_CONSENT_STATUS, Convert.ToInt32(false)) == Convert.ToInt32(true);
	}

	private void OnClickAccept()
	{
		PlayerPrefs.SetInt(PLAYER_PREF_PRIVACY_POLICY_CONSENT_STATUS, Convert.ToInt32(true));
		PlayerPrefs.Save();
	}
}

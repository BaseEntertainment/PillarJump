using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotificationPopup : MonoBehaviour
{
	[SerializeField] private TMP_Text _text;
	[SerializeField] private Image _icon;

	private void OnEnable()
	{
		SoundSystem.PlayNotificationSound();
	}

	public void Show(string text, Sprite icon = null)
	{
		SetText(text);
		SetIcon(icon);

		gameObject.SetActive(true);
	}

	private void SetText(string text)
	{
		var localizedText = LocalizationSystem.GetTranslation(text);

		_text.text = localizedText;
	}

	private void SetIcon(Sprite icon)
	{
		if (icon != null)
		{
			_icon.sprite = icon;
		}

		_icon.gameObject.SetActive(icon != null);
	}
}

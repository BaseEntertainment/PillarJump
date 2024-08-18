using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonPlatformOpenLink : MonoBehaviour
{
	[SerializeField] private string _linkAndroid;
	[SerializeField] private string _linkIOS;

	private Button _button;

	private void Awake()
	{
		_button = GetComponent<Button>();

		AddButtonListener();
	}

	private void AddButtonListener()
	{
		string link;

#if UNITY_IOS
		link = _linkIOS;
#else
		link = _linkAndroid;
#endif

		_button.onClick.AddListener(() => OpenURL(link));
	}

	private void OpenURL(string url)
	{
		Application.OpenURL(url);
	}
}

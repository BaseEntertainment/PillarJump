using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonOpenLink : MonoBehaviour
{
	[SerializeField] private string _link = "https://www.google.com";

	private Button _button;

	private void Awake()
	{
		_button = GetComponent<Button>();

		AddButtonListener();
	}

	private void AddButtonListener()
	{
		_button.onClick.AddListener(() => OpenURL(_link));
	}

	private void OpenURL(string url)
	{
		Application.OpenURL(url);
	}
}

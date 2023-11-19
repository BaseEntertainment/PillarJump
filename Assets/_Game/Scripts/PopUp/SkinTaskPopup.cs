using System.Collections;
using TMPro;
using UnityEngine;

public class SkinTaskPopup : MonoBehaviour
{
	[SerializeField] private TMP_Text _text;
	[SerializeField] private bool _autoHide = true;
	[SerializeField, Min(1)] private float _autoHideDelay = 3.0f;

	public void Show(string text)
	{
		_text.text = text;

		gameObject.SetActive(true);

		if (_autoHide == false)
		{
			return;
		}

		StopAllCoroutines();
		StartCoroutine(Hide());
	}

	private IEnumerator Hide()
	{
		yield return new WaitForSeconds(_autoHideDelay);

		gameObject.SetActive(false);
	}
}

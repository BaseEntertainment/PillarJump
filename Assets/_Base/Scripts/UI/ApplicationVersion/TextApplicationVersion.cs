using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TextApplicationVersion : MonoBehaviour
{
	private void Awake()
	{
		UpdateText();
	}

	private void UpdateText()
	{
		GetComponent<TMP_Text>().text = "v" + Application.version;
	}
}

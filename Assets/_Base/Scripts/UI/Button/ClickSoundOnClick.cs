using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ClickSoundOnClick : MonoBehaviour
{
	void Start()
	{
		GetComponent<Button>().onClick.AddListener(PlayClickSound);
	}

	private void PlayClickSound()
	{
		SoundSystem.PlayClickSound();
	}
}

#if UNITY_IOS && !UNITY_EDITOR
using System.Runtime.InteropServices;
#endif

using UnityEngine;

public class IosSilentModeDeactivator : MonoBehaviour
{
#if UNITY_IOS && !UNITY_EDITOR
	[DllImport("__Internal")]
	private static extern void ForcePlayAudioEvenInSilentMode();

	private void Awake()
	{
		ForcePlayAudio();
	}

	public void ForcePlayAudio()
	{
		ForcePlayAudioEvenInSilentMode();
	}
#endif
}

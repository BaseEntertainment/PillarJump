using UnityEngine;

public class Screenshoter : MonoBehaviour
{
	[SerializeField] private KeyCode _keyToMakeScreenshot = KeyCode.C;
	[SerializeField, Range(1, 4)] private int _size = 4;

	private void Update()
	{
		if (Input.GetKeyDown(_keyToMakeScreenshot))
		{
			MakeScreenShot();
		}
	}

	private void MakeScreenShot()
	{
		ScreenCapture.CaptureScreenshot($"Screenshot_{System.DateTime.Now:(HH-mm-ss}.png", _size);

		Debug.Log("Screenshot was taken.");
	}
}

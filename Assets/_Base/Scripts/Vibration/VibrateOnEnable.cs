using UnityEngine;

public class VibrateOnEnable : MonoBehaviour
{
	private void OnEnable()
	{
		VibrationSystem.LightImpact();
	}
}

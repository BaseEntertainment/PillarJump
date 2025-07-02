using UnityEngine;
using UnityEngine.EventSystems;

public class VibrationWithPointerEvents : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	[Header("Vibration (Light Impact)"), Space(5)]
	[SerializeField] private bool _vibrateOnPointerDown = true;
	[SerializeField] private bool _vibrateOnPointerUp = false;

	public void OnPointerDown(PointerEventData eventData)
	{
		if (_vibrateOnPointerDown == false)
		{
			return;
		}

		VibrationSystem.LightImpact();
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (_vibrateOnPointerUp == false)
		{
			return;
		}

		VibrationSystem.LightImpact();
	}
}
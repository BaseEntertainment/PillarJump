using UnityEngine;

public class NotificationPopup : MonoBehaviour
{
	private void OnEnable()
	{
		SoundSystem.PlayNotificationSound();
	}
}

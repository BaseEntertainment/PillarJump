using UnityEngine;

public class TargetFrameRate : MonoBehaviour
{
	[SerializeField, Range(30, 300)] private int _frameRate = 60;

	void Awake()
	{
		Application.targetFrameRate = _frameRate;
	}
}

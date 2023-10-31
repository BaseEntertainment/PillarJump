using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraAdaptiveViewSize : MonoBehaviour
{
	private Camera _camera;

	private const float ASPECT_RATION = 16.0f / 9.0f;

	private void Awake()
	{
		_camera = GetComponent<Camera>();

		_camera.fieldOfView = GetFOVSize();
		_camera.orthographicSize = GetOrthogrphicSize();
	}

	private float GetFOVSize()
	{
		return _camera.fieldOfView * ((float)Screen.height / Screen.width) / ASPECT_RATION;
	}

	private float GetOrthogrphicSize()
	{
		return _camera.orthographicSize * ((float)Screen.height / Screen.width) / ASPECT_RATION;
	}
}

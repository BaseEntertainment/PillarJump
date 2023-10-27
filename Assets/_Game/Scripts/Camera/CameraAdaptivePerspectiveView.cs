using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraAdaptivePerspectiveView : MonoBehaviour
{
	private Camera _camera;

	private const float ASPECT_RATION = 16.0f / 9.0f;

	private void Awake()
	{
		_camera = GetComponent<Camera>();

		_camera.orthographicSize = GetOrthogrphicSize();
	}

	private float GetOrthogrphicSize()
	{
		return _camera.fieldOfView * ((float)Screen.height / Screen.width) / ASPECT_RATION;
	}
}

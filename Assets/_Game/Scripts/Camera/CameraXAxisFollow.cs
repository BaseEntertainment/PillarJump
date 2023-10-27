using UnityEngine;

public class CameraXAxisFollow : MonoBehaviour
{
	[SerializeField] private Transform _target;
	[SerializeField, Range(-5, 5)] private float _positionOffsetX;

	private Transform _transform;

	private float _velocity;

	[SerializeField] private float _smoothTime = 0.1f;

	private void Awake()
	{
		_transform = transform;
	}

	private void LateUpdate()
	{
		Vector3 targetPosition = new(Mathf.SmoothDamp(_transform.position.x, _target.position.x + _positionOffsetX, ref _velocity, _smoothTime), _transform.position.y, _transform.position.z);

		_transform.position = targetPosition;
	}
}
using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
public class Ball : MonoBehaviour
{
	[SerializeField] private float _moveForce = 15.0f;
	[SerializeField] private float _jumpForce = 8.0f;
	[SerializeField] private float _rotationForce = 100.0f;

	[SerializeField] private float _xVelocityLimit = 4.0f;

	private Rigidbody _rigidBody;

	private float _startedMoveTime;

	private bool _isMoving = false;

	public static event Action<Pillar> JumpedOnPillar;

	private void Awake()
	{
		_rigidBody = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			RestartStartedMoveTime();

			_isMoving = true;
		}

		if (Input.GetMouseButtonUp(0))
		{
			_isMoving = false;
		}
	}

	private void FixedUpdate()
	{
		if (_isMoving)
		{
			Move();
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		SoundSystem.PlayJumpSound();

		if (collision.gameObject.TryGetComponent(out Pillar pillar) == false)
		{
			return;
		}

		Jump();

		_rigidBody.angularVelocity = Vector3.zero;
		_rigidBody.AddTorque(_rotationForce * Vector3.one);

		RestartStartedMoveTime();

		JumpedOnPillar?.Invoke(pillar);
	}

	public void Jump()
	{
		_rigidBody.velocity = _jumpForce * Vector3.up;

		VibrationSystem.LightImpact();
	}

	public void Move()
	{
		float holdTimeDelta = (Time.time - _startedMoveTime) * Time.deltaTime;

		_rigidBody.velocity += _moveForce * holdTimeDelta * Vector3.right;
		_rigidBody.velocity = new Vector3(Mathf.Clamp(_rigidBody.velocity.x, 0, _xVelocityLimit), _rigidBody.velocity.y, 0);

		_rigidBody.AddTorque(holdTimeDelta * _rotationForce * Vector3.back);
	}

	private void RestartStartedMoveTime()
	{
		_startedMoveTime = Time.time;
	}
}

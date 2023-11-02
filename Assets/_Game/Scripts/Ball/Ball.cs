using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
public class Ball : MonoBehaviour
{
	[SerializeField] private float _moveForce = 15.0f;
	[SerializeField] private float _jumpForce = 8.0f;
	[SerializeField] private float _rotationForce = 100.0f;

	[SerializeField] private float _xVelocityLimit = 4.0f;
	[SerializeField] private float _initialYPosition = 5.0f;
	[SerializeField] private string DangerTag = "Danger";

	private Rigidbody _rigidBody;

	private float _startedMoveTime;

	private bool _isMoveHolding = false;
	private bool _canMove = true;

	public static event Action<Pillar> JumpedOnPillar;
	public static event Action EnteredDangerZone;

	private void Awake()
	{
		_rigidBody = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		if (_canMove == false)
		{
			return;
		}

		if (Input.GetMouseButtonDown(0))
		{
			RestartStartedMoveTime();

			_isMoveHolding = true;
		}

		if (Input.GetMouseButtonUp(0))
		{
			_isMoveHolding = false;
		}
	}

	private void FixedUpdate()
	{
		if (_isMoveHolding)
		{
			Move();
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag(DangerTag))
		{
			if (_canMove)
			{
				SoundSystem.PlayCrashSound();
				EnteredDangerZone?.Invoke();
			}

			_canMove = false;
			_isMoveHolding = false;

			gameObject.SetActive(false);

			VibrationSystem.LightImpact();

			return;
		}

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

	public void MoveToContinuePosition()
	{
		_rigidBody.velocity = Vector3.zero;
		MoveBallToNearestPillar();

		_canMove = true;

		gameObject.SetActive(true);
	}

	private void MoveBallToNearestPillar()
	{
		foreach (var pillar in PillarsPool.ActivePillars)
		{
			if (transform.position.x + 1.0f < pillar.transform.position.x)
			{
				transform.position = new Vector2(pillar.transform.position.x, _initialYPosition);
				break;
			}
		}
	}

	private void RestartStartedMoveTime()
	{
		_startedMoveTime = Time.time;
	}
}

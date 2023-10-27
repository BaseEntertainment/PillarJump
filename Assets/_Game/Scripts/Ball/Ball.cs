using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
public class Ball : MonoBehaviour
{
	[SerializeField] private float _jumpForce = 10.0f;
	[SerializeField] private float _moveForce = 10.0f;
	[SerializeField] private float _rotationForce = 10.0f;

	[SerializeField] private float _xVelocityLimit = 5.0f;

	[SerializeField] private AudioSource _collideAudioSource;

	private Rigidbody _rigidBody;

	private float _startedTime;

	private void Awake()
	{
		_rigidBody = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			RestartStartTime();
		}

		if (Input.GetMouseButton(0))
		{
			Move();
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.TryGetComponent(out Pillar pillar) == false)
		{
			return;
		}

		pillar.Disappear();

		_collideAudioSource.volume = Random.Range(0.3f, 0.4f);
		_collideAudioSource.Play();

		Jump();
		RestartStartTime();
	}

	public void Jump()
	{
		_rigidBody.velocity = _jumpForce * Vector3.up;

		_rigidBody.angularVelocity = -_rotationForce * Vector3.one;
	}

	public void Move()
	{
		if (_rigidBody.velocity.x > _xVelocityLimit)
		{
			return;
		}

		var force = _moveForce * (Time.time - _startedTime) * Time.deltaTime;

		_rigidBody.velocity += force * Vector3.one;
	}

	private void RestartStartTime()
	{
		_startedTime = Time.time - 0.3f;
	}
}

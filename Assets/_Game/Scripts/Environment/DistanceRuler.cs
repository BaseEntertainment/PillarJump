using TMPro;
using UnityEngine;

public class DistanceRuler : MonoBehaviour
{
	[SerializeField] private TMP_Text _text;
	[SerializeField, Min(10)] private int _forEachInMeters = 10;
	private int _distanceBeforeMove;
	private int _currentDistance = 0;

	private void Awake()
	{
		_distanceBeforeMove = _forEachInMeters / 3;
	}

	private void OnEnable()
	{
		Ball.JumpedOnPillar += OnJumpedNewPillar;
	}

	private void OnDisable()
	{
		Ball.JumpedOnPillar -= OnJumpedNewPillar;
	}

	private void OnJumpedNewPillar(Pillar pillar)
	{
		float pillarPosition = pillar.transform.position.x;

		if (pillarPosition - _currentDistance >= _distanceBeforeMove)
		{
			_currentDistance += _forEachInMeters;

			SetText(_currentDistance);
		}
	}

	private void SetText(int distance)
	{
		string text = string.Empty;

		if (distance > 1000)
		{
			text += (distance / 1000) + "km ";
			distance %= 1000;
		}

		text += distance + "m";

		_text.text = text;

		_text.transform.position = new Vector2(distance, _text.transform.position.y);
	}
}

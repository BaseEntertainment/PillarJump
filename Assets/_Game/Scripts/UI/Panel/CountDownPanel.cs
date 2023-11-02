using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CountDownPanel : MonoBehaviour
{
	[SerializeField] private TMP_Text _text;
	[SerializeField, Range(1, 10)] private int _startNumber = 3;
	[SerializeField, Range(1, 5)] private float _animationScale = 2.0f;
	[SerializeField] private UnityEvent _completed;

	private float _animationSpeed = 0.5f;
	private int _currentNumber;

	private void OnEnable()
	{
		_currentNumber = _startNumber;

		UpdateUI();

		StartCoroutine(CountDown());
	}

	private void OnDisable()
	{
		_text.transform.DOKill();
	}

	private IEnumerator CountDown()
	{
		while (true)
		{
			if (_currentNumber > 0)
			{
				SoundSystem.PlayCountDownSound();
				yield return new WaitForSecondsRealtime(1);
				_currentNumber--;
				UpdateUI();
			}
			else
			{
				_completed?.Invoke();
				break;
			}
		}
	}

	private void UpdateUI()
	{
		_text.text = _currentNumber.ToString();
		Animate();
	}

	private void Animate()
	{
		_text.transform.localScale = Vector3.one / _animationScale;
		_text.transform.DOScale(Vector3.one * _animationScale, _animationSpeed);
	}
}

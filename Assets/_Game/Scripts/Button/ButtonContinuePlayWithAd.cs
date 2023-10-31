using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonContinuePlayWithAd : MonoBehaviour
{
	[SerializeField] private IronSourceMediation _adMediation;
	[SerializeField] private Slider _slider;
	[SerializeField, Range(0.1f, 10.0f)] private float _animationDuration = 5.0f;

	[SerializeField] private UnityEvent _onComplete;

	private Button _button;

	private void OnEnable()
	{
		Animate();
	}

	private void Animate()
	{
		_slider.value = 1;
		_slider.DOValue(0, _animationDuration).OnComplete(() => _onComplete?.Invoke());
	}
}

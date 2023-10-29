using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonContinuePlayWithAd : MonoBehaviour
{
	[SerializeField] private IronSourceMediation _adMediation;
	[SerializeField] private GameObject _content;
	[SerializeField] private Slider _slider;
	[SerializeField, Range(0.1f, 10.0f)] private float _animationDuration = 5.0f;

	private Button _button;

	private void OnEnable()
	{
		_content.SetActive(true);
		Animate();
	}

	private void Animate()
	{
		_slider.value = 1;
		_slider.DOValue(0, _animationDuration).OnComplete(() => _content.SetActive(false));
	}
}

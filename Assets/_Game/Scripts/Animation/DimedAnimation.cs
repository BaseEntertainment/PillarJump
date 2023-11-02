using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DimedAnimation : MonoBehaviour
{
	[SerializeField, Range(0, 5.0f)] private float _duration = 2.0f;

	private Image _image;
	private float _alfaEndValue;

	private void Awake()
	{
		_image = GetComponent<Image>();

		_alfaEndValue = _image.color.a;
	}

	private void OnEnable()
	{
		Animate();
	}

	private void OnDisable()
	{
		_image.DOKill();
	}

	private void Animate()
	{
		_image.DOFade(0, 0);
		_image.DOFade(_alfaEndValue, _duration);
	}
}

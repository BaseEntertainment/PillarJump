using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DimedAnimation : MonoBehaviour
{
	[SerializeField, Range(1, 255)] private int _alfaEndValue = 150;
	[SerializeField, Range(0, 5.0f)] private float _duration = 1.5f;

	private Image _image;

	private void Awake()
	{
		_image = GetComponent<Image>();
	}

	private void OnEnable()
	{
		Animate();
	}

	private void OnDisable()
	{
		DOTween.KillAll();
	}

	private void Animate()
	{
		_image.DOFade(0, 0);
		_image.DOFade((float)_alfaEndValue / 255, _duration);
	}
}

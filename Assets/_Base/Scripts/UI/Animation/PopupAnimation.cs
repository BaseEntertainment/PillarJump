using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class PopupAnimation : MonoBehaviour
{
	[SerializeField, Range(0, 3)] private float _animationDuration = 0.4f;

	private RectTransform _rectTransform;

	private Vector2 _initialPosition;

	private void Awake()
	{
		_rectTransform = GetComponent<RectTransform>();
		_initialPosition = _rectTransform.anchoredPosition;
	}

	private void OnEnable()
	{
		_rectTransform.transform.localPosition = Vector3.down * (Screen.width + _rectTransform.sizeDelta.y);
		_rectTransform.DOAnchorPos(_initialPosition, _animationDuration).SetEase(Ease.OutBack);
	}
}

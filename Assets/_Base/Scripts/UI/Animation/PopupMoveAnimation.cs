using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class PopupMoveAnimation : MonoBehaviour
{
	[SerializeField, Range(0, 3)] private float _animationDuration = 0.4f;
	[SerializeField] private Direction _appearsFrom = Direction.Down;
	[SerializeField] private bool _withBounce = true;

	private RectTransform _rectTransform;

	private Vector2 _initialPosition;
	private float _disappearPosition;

	private Vector3 _direction;

	private void Awake()
	{
		_rectTransform = GetComponent<RectTransform>();
		_initialPosition = _rectTransform.anchoredPosition;

		SetDirection();
		SetDisappearPosition();
	}

	private void OnEnable()
	{
		_rectTransform.transform.localPosition = _direction * _disappearPosition;
		var tween = _rectTransform.DOAnchorPos(_initialPosition, _animationDuration);

		if (_withBounce)
		{
			tween.SetEase(Ease.OutBack);
		}
	}

	private void SetDirection()
	{
		switch (_appearsFrom)
		{
			default:
			case Direction.Down:
				_direction = Vector3.down;
				break;
			case Direction.Up:
				_direction = Vector3.up;
				break;
			case Direction.Left:
				_direction = Vector3.left;
				break;
			case Direction.Right:
				_direction = Vector3.right;
				break;
		}
	}

	private void SetDisappearPosition()
	{
		switch (_appearsFrom)
		{
			default:
			case Direction.Down:
			case Direction.Up:
				_disappearPosition = Screen.height;
				break;
			case Direction.Left:
			case Direction.Right:
				_disappearPosition = Screen.width;
				break;
		}
	}
}

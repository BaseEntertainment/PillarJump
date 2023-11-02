using DG.Tweening;
using UnityEngine;

public class ScaleAnimation : MonoBehaviour
{
	[SerializeField] private Vector3 _scaleTo = Vector3.one * 1.1f;
	[SerializeField] private float _duration = 1.0f;

	private Vector3 _initialScale;

	private void Awake()
	{
		_initialScale = transform.localScale;
	}

	private void OnEnable()
	{
		transform.localScale = _initialScale;

		Animate();
	}

	private void OnDisable()
	{
		transform.DOKill();
	}

	private void Animate()
	{
		transform.DOScale(_scaleTo, _duration).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
	}
}

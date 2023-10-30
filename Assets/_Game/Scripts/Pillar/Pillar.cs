using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Pillar : MonoBehaviour
{
	public int Index { get; set; }

	[SerializeField, Range(0, 10.0f)] private float _animationDuration = 4.0f;
	[SerializeField, Range(-100f, 100.0f)] private float _animationYEndPosition = -10.0f;

	private void OnDisable()
	{
		transform.DOKill();
	}

	public void Disappear()
	{
		transform.DOMoveY(_animationYEndPosition, _animationDuration);
	}
}

using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Pillar : MonoBehaviour
{
	[SerializeField, Range(0, 10.0f)] private float _disappearTime = 3.0f;
	[SerializeField, Range(0, 100.0f)] private float _disappearYPosition = -10.0f;

	private void OnDisable()
	{
		transform.DOKill();
	}

	public void Disappear()
	{
		transform.DOMoveY(_disappearYPosition, _disappearTime).OnComplete(() => gameObject.SetActive(false));
	}
}

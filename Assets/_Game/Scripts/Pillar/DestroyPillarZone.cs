using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class DestroyPillarZone : MonoBehaviour
{
	private BoxCollider _collider;

	private void Awake()
	{
		_collider = GetComponent<BoxCollider>();

		_collider.isTrigger = true;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.TryGetComponent(out Pillar pillar) == false)
		{
			return;
		}

		PillarsPool.Destroy(pillar);
	}

	private void OnDrawGizmos()
	{
		if (_collider == null)
		{
			return;
		}

		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(transform.position, _collider.size);
	}
}

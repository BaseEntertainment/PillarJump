using UnityEngine;

public class PillarSpawner : MonoBehaviour
{
	[SerializeField] private Pillar _prefab;
	[SerializeField] private int _pillarsCount = 100;
	[SerializeField] private float _xSpacing = 3.0f;
	[SerializeField] private float _yMaxPosition = 0.0f;
	[SerializeField] private float _yMinPosition = 3.0f;

	private float _xPositionTemp = 0;

	private void Start()
	{
		SpawnPillars();
	}

	private void SpawnPillars()
	{
		var pillar = Instantiate(_prefab, transform);
		pillar.transform.position = new Vector3(0, _yMaxPosition, 0);

		for (int i = 1; i < _pillarsCount; i++)
		{
			pillar = Instantiate(_prefab, transform);

			_xPositionTemp += _xSpacing;

			pillar.transform.position = new Vector2(Random.Range(_xPositionTemp - 0.5f, _xPositionTemp + 0.5f), Random.Range(_yMinPosition, _yMaxPosition));
		}
	}
}

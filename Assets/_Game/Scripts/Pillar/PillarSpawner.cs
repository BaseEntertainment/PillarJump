using UnityEngine;

[RequireComponent(typeof(PillarsPool))]
public class PillarSpawner : MonoBehaviour
{
	[SerializeField] private int _initialPillarsCount = 100;
	[SerializeField] private float _xSpacing = 3.0f;
	[SerializeField] private float _xSpacingRange = 0.5f;
	[SerializeField] private float _yMaxPosition = 0.0f;
	[SerializeField] private float _yMinPosition = 3.0f;

	private float _xPositionTemp = 0;

	private int _lastPillarIndex;

	private void OnEnable()
	{
		Ball.JumpedOnPillar += OnJumpedOnPillar;
	}

	private void OnDisable()
	{
		Ball.JumpedOnPillar -= OnJumpedOnPillar;
	}

	private void OnJumpedOnPillar(Pillar pillar)
	{
		pillar.DoMove();

		for (int i = PillarsPool.ActivePillars.Count; i < _initialPillarsCount; i++)
		{
			SpawnPillarOnRandomRange();
		}
	}

	private void Start()
	{
		SpawnInitialPillars();
	}

	private void SpawnInitialPillars()
	{
		SpawnPillar(new Vector2(0, _yMaxPosition)); // Spawn first pillar

		for (int i = 1; i < _initialPillarsCount; i++)
		{
			SpawnPillarOnRandomRange();
		}
	}

	private void SpawnPillarOnRandomRange()
	{
		var pos = new Vector2(Random.Range(_xPositionTemp - _xSpacingRange, _xPositionTemp + _xSpacingRange), Random.Range(_yMinPosition, _yMaxPosition));
		SpawnPillar(pos);
	}

	private void SpawnPillar(Vector2 position)
	{
		var pillar = PillarsPool.Instantiate(position);

		UpdatePillarIndex(pillar);

		_xPositionTemp += _xSpacing;
	}

	private void UpdatePillarIndex(Pillar pillar)
	{
		pillar.Index = _lastPillarIndex;
		_lastPillarIndex++;
	}
}

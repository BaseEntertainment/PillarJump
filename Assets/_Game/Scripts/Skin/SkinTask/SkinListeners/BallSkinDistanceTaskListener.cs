using UnityEngine;

public class BallSkinDistanceTaskListener : BallSkinTaskListener
{
	[SerializeField] private int _distanceToReach = 1000;

	private void OnEnable()
	{
		PlayerScoreSystem.DistanceUpdated += OnDistanceUpdated;
	}

	private void OnDisable()
	{
		PlayerScoreSystem.DistanceUpdated -= OnDistanceUpdated;
	}

	private void OnDistanceUpdated(int distance)
	{
		if (distance >= _distanceToReach)
		{
			OnSkinOpened();
		}
	}

	protected override void OnSkinOpened()
	{
		base.OnSkinOpened();
		Destroy(gameObject);
	}
}

using UnityEngine;

public class BallSkinJumpOverPillarsTaskListener : BallSkinTaskListener
{
	[SerializeField] private int _pillarsCountToReach = 5;

	private int _currentJumpedOverPillarsCount;

	private void OnEnable()
	{
		PlayerScoreSystem.JumpedOverPillars += OnDistanceUpdated;
	}

	private void OnDisable()
	{
		PlayerScoreSystem.JumpedOverPillars -= OnDistanceUpdated;
	}

	private void OnDistanceUpdated(int pillarsCount)
	{
		_currentJumpedOverPillarsCount += pillarsCount;

		if (_currentJumpedOverPillarsCount >= _pillarsCountToReach)
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

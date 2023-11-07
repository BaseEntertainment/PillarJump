using UnityEngine;

public class BallSkinScoreTaskListener : BallSkinTaskListener
{
	[SerializeField] private int _scoreToReach = 20;

	private void OnEnable()
	{
		PlayerScoreSystem.ScoreUpdated += OnScoreUpdated;
	}

	private void OnDisable()
	{
		PlayerScoreSystem.ScoreUpdated -= OnScoreUpdated;
	}

	private void OnScoreUpdated(int score)
	{
		if (score >= _scoreToReach)
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

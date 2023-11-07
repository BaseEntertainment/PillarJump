using OPS.AntiCheat.Field;
using OPS.AntiCheat.Prefs;
using System;
using UnityEngine;

public class PlayerScoreSystem : MonoBehaviour
{
	public ProtectedInt32 CurrentScore { get; private set; }
	public ProtectedInt32 RecordScore { get; private set; }
	public ProtectedBool IsRecordBeated { get; private set; }
	public ProtectedInt32 PassedDistance { get; private set; }

	private const int DEFAULT_SCORE_VALUE = 0;
	private const string PLAYER_PREF_SCORE_BEST = "SCORE_BEST";

	[SerializeField] private GameUI _gameUI;
	[SerializeField, Range(1, 100)] private int _scoreMultiplier = 5;
	[SerializeField, Range(0, 100)] private int _minScoreToShowContinuePanel = 50;

	public static event Action NewRecord;
	public static event Action<int> JumpedOverPillars;
	public static event Action<int> ScoreUpdated;
	public static event Action<int> DistanceUpdated;

	private int _lastPillarIndex;
	private bool _isSecondTry;

	private void OnEnable()
	{
		Ball.JumpedOnPillar += OnJumpedNewPillar;
		Ball.EnteredDangerZone += OnBallEnteredDangerZone;
	}

	private void OnDisable()
	{
		Ball.JumpedOnPillar -= OnJumpedNewPillar;
		Ball.EnteredDangerZone -= OnBallEnteredDangerZone;
	}

	private void Awake()
	{
		LoadBestScoreFromPlayerPrefs();
	}

	private void OnBallEnteredDangerZone()
	{
		SaveRecordScorePlayerPrefs();

		if (_isSecondTry == false && CurrentScore >= _minScoreToShowContinuePanel && AdMediation.Instance.IsRewardedVideoAvailable)
		{
			_isSecondTry = true;

			_gameUI.ShowContinuePanel();
		}
		else
		{
			_gameUI.ShowGameOverPanel();
		}
	}

	private void LoadBestScoreFromPlayerPrefs()
	{
		RecordScore = ProtectedPlayerPrefs.GetInt(PLAYER_PREF_SCORE_BEST, DEFAULT_SCORE_VALUE);
	}

	private void OnJumpedNewPillar(Pillar pillar)
	{
		UpdateCurrentScore(pillar);
		UpdateRecordScore();
		UpdatePassedDistance(pillar);
		UpdateUI();
	}

	private void UpdateCurrentScore(Pillar pillar)
	{
		var difference = pillar.Index - _lastPillarIndex;

		CurrentScore += difference * _scoreMultiplier;
		_lastPillarIndex = pillar.Index;

		ScoreUpdated?.Invoke(CurrentScore);

		if (difference > 1)
		{
			JumpedOverPillars?.Invoke(difference - 1);
		}
	}

	private void UpdateUI()
	{
		_gameUI.UpdateScore(CurrentScore);
	}

	private void UpdateRecordScore()
	{
		if (CurrentScore > RecordScore)
		{
			RecordScore = CurrentScore;

			if (IsRecordBeated == false)
			{
				IsRecordBeated = true;

				NewRecord?.Invoke();

				_gameUI.ShowNotificationPopup("New record!");
			}
		}
	}

	private void UpdatePassedDistance(Pillar pillar)
	{
		PassedDistance = (int)pillar.transform.position.x;

		DistanceUpdated?.Invoke(PassedDistance);
	}

	private void SaveRecordScorePlayerPrefs()
	{
		ProtectedPlayerPrefs.SetInt(PLAYER_PREF_SCORE_BEST, RecordScore);
		ProtectedPlayerPrefs.Save();
	}
}

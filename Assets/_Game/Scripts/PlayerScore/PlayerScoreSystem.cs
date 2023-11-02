using OPS.AntiCheat.Field;
using OPS.AntiCheat.Prefs;
using System;
using TMPro;
using UnityEngine;

public class PlayerScoreSystem : MonoBehaviour
{
	public ProtectedInt32 CurrentScore { get; private set; }
	public ProtectedInt32 RecordScore { get; private set; }
	public ProtectedBool IsRecordBeated { get; private set; }
	public ProtectedInt32 PassedDistance { get; private set; }

	private const int DEFAULT_SCORE_VALUE = 0;
	private const string PLAYER_PREF_SCORE_BEST = "SCORE_BEST";

	private int _lastPillarIndex;

	[SerializeField] private TMP_Text _scoreText;
	[SerializeField, Range(1, 100)] private int _multiplier = 5;

	public static event Action NewRecord;

	private void OnEnable()
	{
		Ball.JumpedOnPillar += OnJumpedNewPillar;
		Ball.EnteredDangerZone += SaveRecordScorePlayerPrefs;
	}

	private void OnDisable()
	{
		Ball.JumpedOnPillar -= OnJumpedNewPillar;
		Ball.EnteredDangerZone -= SaveRecordScorePlayerPrefs;
	}

	private void Awake()
	{
		LoadBestScoreFromPlayerPrefs();
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
		CurrentScore += (pillar.Index - _lastPillarIndex) * _multiplier;
		_lastPillarIndex = pillar.Index;
	}

	private void UpdateUI()
	{
		_scoreText.text = CurrentScore.ToString();
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
			}
		}
	}

	private void UpdatePassedDistance(Pillar pillar)
	{
		PassedDistance = (int)pillar.transform.position.x;
	}

	private void SaveRecordScorePlayerPrefs()
	{
		ProtectedPlayerPrefs.SetInt(PLAYER_PREF_SCORE_BEST, RecordScore);
		ProtectedPlayerPrefs.Save();
	}

	[ContextMenu("Delete Record")]
	private void ResetRecord()
	{
		ProtectedPlayerPrefs.DeleteKey(PLAYER_PREF_SCORE_BEST);
	}
}

using TMPro;
using UnityEngine;

public class PlayerScoreSystem : MonoBehaviour
{
	public int CurrentScore { get; private set; }
	public int RecordScore { get; private set; }
	public bool IsRecordBeated { get; private set; }

	///public static event Action NewRecord;

	private const int DEFAULT_SCORE_VALUE = 0;
	private const string PLAYER_PREF_SCORE_BEST = "SCORE_BEST";

	private int _lastPillarIndex;

	[SerializeField] private TMP_Text _scoreText;
	[SerializeField, Range(1, 100)] private int _multiplier = 5;

	private void OnEnable()
	{
		Ball.JumpedOnPillar += OnJumpedNewPillar;
	}

	private void OnDisable()
	{
		Ball.JumpedOnPillar -= OnJumpedNewPillar;
	}

	private void OnJumpedNewPillar(Pillar pillar)
	{
		CurrentScore += (pillar.Index - _lastPillarIndex) * _multiplier;
		_lastPillarIndex = pillar.Index;

		UpdateUI();
	}

	private void UpdateUI()
	{
		_scoreText.text = CurrentScore.ToString();
	}

	private void Start()
	{
		LoadBestScoreFromPlayerPrefs();
	}

	private void LoadBestScoreFromPlayerPrefs()
	{
		RecordScore = PlayerPrefs.GetInt(PLAYER_PREF_SCORE_BEST, DEFAULT_SCORE_VALUE);
	}

	public void SaveRecordScorePlayerPrefs()
	{
		PlayerPrefs.SetInt(PLAYER_PREF_SCORE_BEST, RecordScore);
		PlayerPrefs.Save();
	}
}

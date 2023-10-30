using UnityEngine;

public class PlayerScoreSystem : MonoBehaviour
{
	public static int CurrentScore { get; private set; }
	public static int RecordScore { get; private set; }
	public static bool IsRecordBeated { get; private set; }

	///public static event Action NewRecord;

	private const int DEFAULT_SCORE_VALUE = 0;
	private const string PLAYER_PREF_SCORE_BEST = "SCORE_BEST";

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

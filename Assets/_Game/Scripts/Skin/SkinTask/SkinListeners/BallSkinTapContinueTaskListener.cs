using OPS.AntiCheat.Prefs;
using UnityEngine;

public class BallSkinTapContinueTaskListener : BallSkinTaskListener, ISavedSkinTaskDataToReach
{
	[SerializeField] private int _tapsCountToReach = 1;

	public int CurrentTapsCount { get; private set; }

	private const string PLAYER_PREFS_CURRENT_TAPS_CONTINUE_COUNT = "CURRENT_CONTINUE_TAPS_COUNT";

	public int CurrentScore => GetScoreFromPlayerPrefs();
	public int ToReachScore => _tapsCountToReach;

	private void Awake()
	{
		LoadData();
	}

	private void OnEnable()
	{
		ContinuePanel.TappedContinue += OnTappedContinue;
	}

	private void OnDisable()
	{
		ContinuePanel.TappedContinue -= OnTappedContinue;
	}

	private void LoadData()
	{
		CurrentTapsCount = GetScoreFromPlayerPrefs();
	}

	private int GetScoreFromPlayerPrefs()
	{
		return ProtectedPlayerPrefs.GetInt(PLAYER_PREFS_CURRENT_TAPS_CONTINUE_COUNT, 0);
	}

	private void SaveData()
	{
		ProtectedPlayerPrefs.SetInt(PLAYER_PREFS_CURRENT_TAPS_CONTINUE_COUNT, CurrentTapsCount);
		ProtectedPlayerPrefs.Save();
	}

	private void OnTappedContinue()
	{
		CurrentTapsCount++;

		if (CurrentTapsCount >= _tapsCountToReach)
		{
			OnSkinOpened();
		}

		SaveData();
	}

	protected override void OnSkinOpened()
	{
		base.OnSkinOpened();
		Destroy(gameObject);
	}
}

using OPS.AntiCheat.Prefs;
using UnityEngine;

public class BallSkinPlayedGamesTaskListener : BallSkinTaskListener, ISavedSkinTaskDataToReach
{
	[SerializeField] private int _playedGamesToReach = 100;

	public int CurrentPlayedGames { get; private set; }

	private const string PLAYER_PREFS_CURRENT_PLAYED_GAMES = "CURRENT_PLAYED_GAMES";

	public int CurrentScore => GetScoreFromPlayerPrefs();
	public int ToReachScore => _playedGamesToReach;

	private void Awake()
	{
		LoadData();
	}

	private void OnEnable()
	{
		TapToPlayPanel.TappedPlay += OnTappedPLay;
	}

	private void OnDisable()
	{
		TapToPlayPanel.TappedPlay -= OnTappedPLay;
	}

	private void LoadData()
	{
		CurrentPlayedGames = GetScoreFromPlayerPrefs();
	}

	private int GetScoreFromPlayerPrefs()
	{
		return ProtectedPlayerPrefs.GetInt(PLAYER_PREFS_CURRENT_PLAYED_GAMES, 0);
	}

	private void SaveData()
	{
		ProtectedPlayerPrefs.SetInt(PLAYER_PREFS_CURRENT_PLAYED_GAMES, CurrentPlayedGames);
		ProtectedPlayerPrefs.Save();
	}

	private void OnTappedPLay()
	{
		CurrentPlayedGames++;

		if (CurrentPlayedGames >= _playedGamesToReach)
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

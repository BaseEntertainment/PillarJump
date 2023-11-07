using Newtonsoft.Json;
using OPS.AntiCheat.Field;
using OPS.AntiCheat.Prefs;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkinSystem : MonoBehaviour
{
	[SerializeField] private SkinData _skinData;
	[SerializeField] private Ball _ball;
	[SerializeField] private GameUI _gameUI;

	public SkinData SkinData => _skinData;

	private const string PLAYER_PREF_SELECTED_BALL_SKIN_ID = "SELECTED_BALL_SKIN_ID";
	private const string PLAYER_PREF_OPENED_BALL_SKIN_IDS = "OPENED_BALL_SKIN_IDS";

	private const int DEFAULT_SKIN_ID = 0;

	public ProtectedInt32 SelectedBallSkinID { get; private set; }

	public IReadOnlyList<int> OpenedBallSkinIDs => _openedBallSkinIDs;
	private List<int> _openedBallSkinIDs = new() { DEFAULT_SKIN_ID };

	private void Awake()
	{
		LoadDataFromPlayerPrefs();

		AddBallSkinListeners();

		UpdateBallSkin();
	}

	private void OnEnable()
	{
		BallSkinTaskListener.SkinOpened += OnSkinOpened;
	}

	private void OnDisable()
	{
		BallSkinTaskListener.SkinOpened -= OnSkinOpened;
	}

	private void LoadDataFromPlayerPrefs()
	{
		LoadSelectedBallSkinID();
		LoadOpenedBallSkinIDs();
	}

	public void SelectBallSkin(int skinID)
	{
		SelectedBallSkinID = skinID;
		SaveSelectedBallSkinID();

		UpdateBallSkin();
	}

	private void UpdateBallSkin()
	{
		foreach (Transform child in _ball.transform)
		{
			Destroy(child.gameObject);
		}

		var skin = _skinData.Balls.FirstOrDefault(x => x.ID == SelectedBallSkinID);

		Instantiate(skin.Prefab, _ball.transform);
	}

	private void AddBallSkinListeners()
	{
		foreach (var skin in _skinData.Balls)
		{
			if (_openedBallSkinIDs.Contains(skin.ID))
			{
				continue;
			}

			if (skin.TaskToOpenBallSkinListener == null)
			{
				continue;
			}

			Instantiate(skin.TaskToOpenBallSkinListener, transform);
		}
	}

	private void OnSkinOpened(int skinID)
	{
		_openedBallSkinIDs.Add(skinID);

		SaveOpenedBallSkinIDs();

		var icon = _skinData.Balls.FirstOrDefault(x => x.ID == skinID)?.Image;

		_gameUI.ShowNotificationPopup("New skin!", icon);
	}

	private void LoadSelectedBallSkinID()
	{
		SelectedBallSkinID = ProtectedPlayerPrefs.GetInt(PLAYER_PREF_SELECTED_BALL_SKIN_ID, 0);
	}

	private void LoadOpenedBallSkinIDs()
	{
		var serializedString = ProtectedPlayerPrefs.GetString(PLAYER_PREF_OPENED_BALL_SKIN_IDS, string.Empty);

		if (serializedString == string.Empty)
		{
			return;
		}

		_openedBallSkinIDs = JsonConvert.DeserializeObject<List<int>>(serializedString);
	}

	public void SaveOpenedBallSkinIDs()
	{
		var serializedString = JsonConvert.SerializeObject(_openedBallSkinIDs);

		ProtectedPlayerPrefs.SetString(PLAYER_PREF_OPENED_BALL_SKIN_IDS, serializedString);
		ProtectedPlayerPrefs.Save();
	}

	public void SaveSelectedBallSkinID()
	{
		ProtectedPlayerPrefs.SetInt(PLAYER_PREF_SELECTED_BALL_SKIN_ID, SelectedBallSkinID);
		ProtectedPlayerPrefs.Save();
	}
}

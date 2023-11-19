using System.Linq;
using UnityEngine;

public class SkinsPopup : MonoBehaviour
{
	[SerializeField] private SkinSystem _skinSystem;

	[SerializeField] private SkinUIItem _prefab;

	[SerializeField] private Transform _parent;

	[SerializeField] private SkinTaskPopup _skinTask;

	private SkinUIItem _selectedBallSkin;

	private void Start()
	{
		UpdateUI();
	}

	private void OnDisable()
	{
		CheckAndUpdateSelectedSkin();
	}

	private void CheckAndUpdateSelectedSkin()
	{
		if (_selectedBallSkin.ID == _skinSystem.SelectedBallSkinID)
		{
			return;
		}

		_skinSystem.SelectBallSkin(_selectedBallSkin.ID);
	}

	private void UpdateUI()
	{
		foreach (Transform child in _parent)
		{
			Destroy(child.gameObject);
		}

		foreach (var ballSkin in _skinSystem.SkinData.Balls)
		{
			var item = Instantiate(_prefab, _parent);

			item.Set(ballSkin);

			var isOpened = IsBallSkinOpened(item);
			var isSelected = IsBallSkinSelected(item);

			item.ResetButton(() => OnBallItemClick(item));
			item.SetActivateSelectedCheckMark(isSelected);
			item.SetActivateTaskIcon(isOpened == false);

			if (isSelected)
			{
				_selectedBallSkin = item;
			}
		}
	}

	private void OnBallItemClick(SkinUIItem item)
	{
		var isOpened = IsBallSkinOpened(item);

		if (isOpened)
		{
			SelectSkin(item);
		}
		else
		{
			ShowTask(item);
		}

		VibrationSystem.LightImpact();
	}

	private void SelectSkin(SkinUIItem item)
	{
		_selectedBallSkin.SetActivateSelectedCheckMark(false);
		_selectedBallSkin = item;
		item.SetActivateSelectedCheckMark(true);
	}

	private void ShowTask(SkinUIItem item)
	{
		var skin = _skinSystem.SkinData.Balls.FirstOrDefault(x => x.ID == item.ID);

		var taskText = LocalizationSystem.GetTranslation(skin.TaskToOpenDescription);

		if (skin.TaskToOpenBallSkinListener is ISavedSkinTaskDataToReach additionalInfo)
		{
			taskText += $" ({additionalInfo.CurrentScore}/{additionalInfo.ToReachScore})";
		}

		_skinTask.Show(taskText);
	}

	private bool IsBallSkinOpened(SkinUIItem item)
	{
		return _skinSystem.OpenedBallSkinIDs.Contains(item.ID);
	}

	private bool IsBallSkinSelected(SkinUIItem item)
	{
		return _skinSystem.SelectedBallSkinID == item.ID;
	}
}
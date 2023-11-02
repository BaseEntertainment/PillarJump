using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
	[SerializeField] private TMP_Text _currentScore;
	[SerializeField] private TMP_Text _recordScore;
	[SerializeField] private TMP_Text _distance;
	[SerializeField] private Image _newLabel;

	[SerializeField] private PlayerScoreSystem _playerScore;

	private void OnEnable()
	{
		UpdateUI();
	}

	private void UpdateUI()
	{
		_currentScore.text = _playerScore.CurrentScore.ToString();
		_recordScore.text = _playerScore.RecordScore.ToString();
		_distance.text = _playerScore.PassedDistance + "m";

		_newLabel.gameObject.SetActive(_playerScore.IsRecordBeated);
	}
}

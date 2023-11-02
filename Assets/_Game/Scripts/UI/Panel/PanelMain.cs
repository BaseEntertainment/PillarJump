using TMPro;
using UnityEngine;

public class PanelMain : MonoBehaviour
{
	[SerializeField] private PlayerScoreSystem _scoreSystem;
	[SerializeField] private TMP_Text _bestScore;

	private void Start()
	{
		UpdateBestScore();
	}

	private void UpdateBestScore()
	{
		_bestScore.text = _scoreSystem.RecordScore.ToString();
	}
}

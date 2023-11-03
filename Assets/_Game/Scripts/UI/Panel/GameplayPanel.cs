using TMPro;
using UnityEngine;

public class GameplayPanel : MonoBehaviour
{
	[SerializeField] private TMP_Text _scoreText;

	public void UpdateScore(int score)
	{
		_scoreText.text = score.ToString();
	}
}

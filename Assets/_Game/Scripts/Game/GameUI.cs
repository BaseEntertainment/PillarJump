using UnityEngine;

public class GameUI : MonoBehaviour
{
	[Header("Panels"), Space(5)]
	[SerializeField] private PanelMain _mainPanel;
	[SerializeField] private GameOverPanel _gameOverPanel;
	[SerializeField] private ContinuePanel _continuePanel;
	[SerializeField] private CountDownPanel _countDownPanel;

	[Header("PLayer Score"), Space(5)]
	[SerializeField] private PlayerScoreSystem _playerScore;
	[SerializeField, Range(0, 100)] private int _minScoreToShowContinuePanel = 50;

	private bool _isSecondTry;

	private void OnEnable()
	{
		Ball.EnteredDangerZone += OnBallEnteredDangerZone;
	}

	private void OnDisable()
	{
		Ball.EnteredDangerZone -= OnBallEnteredDangerZone;
	}

	private void OnBallEnteredDangerZone()
	{
		if (_isSecondTry == false && _playerScore.CurrentScore >= _minScoreToShowContinuePanel)
		{
			_continuePanel.gameObject.SetActive(true);
			_isSecondTry = true;
		}
		else
		{
			_gameOverPanel.gameObject.SetActive(true);
		}
	}
}

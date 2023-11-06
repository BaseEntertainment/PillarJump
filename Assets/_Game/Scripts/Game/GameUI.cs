using UnityEngine;

public class GameUI : MonoBehaviour
{
	[Header("Panels"), Space(5)]
	[SerializeField] private PanelMain _mainPanel;
	[SerializeField] private GameplayPanel _gameplayPanel;
	[SerializeField] private GameOverPanel _gameOverPanel;
	[SerializeField] private ContinuePanel _continuePanel;
	[SerializeField] private CountDownPanel _countDownPanel;

	[Header("Player Score"), Space(5)]
	[SerializeField] private PlayerScoreSystem _playerScore;

	[Header("Environment"), Space(5)]
	[SerializeField] private MoveTutorial _moveTutorial;

	public void SwitchToGameplay()
	{
		SetActivateMainPanel(false);

		SetActivateMoveTutorial(true);
		SetActivateGameplayPanel(true);
	}

	public void UpdateScore(int score) => _gameplayPanel.UpdateScore(score);

	public void ShowContinuePanel() => _continuePanel.gameObject.SetActive(true);

	public void ShowCountDownPanel()
	{
		SetActivateGameplayPanel(true);
		SetActivateGameOverPanel(false);
		_continuePanel.gameObject.SetActive(false);
		_countDownPanel.gameObject.SetActive(true);
	}

	public void ShowGameOverPanel()
	{
		SetActivateGameplayPanel(false);
		SetActivateGameOverPanel(true);
	}

	public void SetActivateGameplayPanel(bool active) => _gameplayPanel.gameObject.SetActive(active);

	public void SetActivateMoveTutorial(bool active) => _moveTutorial.gameObject.SetActive(active);

	public void SetActivateMainPanel(bool active) => _mainPanel.gameObject.SetActive(active);

	public void SetActivateGameOverPanel(bool active) => _gameOverPanel.gameObject.SetActive(active);
}
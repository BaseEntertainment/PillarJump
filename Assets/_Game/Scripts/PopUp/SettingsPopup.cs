using UnityEngine;

public class SettingsPopup : Popup
{
	[SerializeField] private SwitchToggle _musicSwitch;
	[SerializeField] private SwitchToggle _soundFXSwitch;
	[SerializeField] private SwitchToggle _vibrationSwitch;

	private void Start()
	{
		ResetToggles();
	}

	private void ResetToggles()
	{
		_musicSwitch.Toggle.isOn = GameSettings.BackgroundMusicEnabled;
		_musicSwitch.Toggle.onValueChanged.AddListener((b) => SwitchMusic());

		_soundFXSwitch.Toggle.isOn = GameSettings.SoundFXEnabled;
		_soundFXSwitch.Toggle.onValueChanged.AddListener((b) => SwitchSoundFX());

		_vibrationSwitch.Toggle.isOn = GameSettings.VibrationEnabled;
		_vibrationSwitch.Toggle.onValueChanged.AddListener((b) => SwitchVibration());
	}

	private void SwitchMusic()
	{
		GameSettings.SwitchBackgroundMusic();
	}

	private void SwitchSoundFX()
	{
		GameSettings.SwitchSoundFX();
	}

	private void SwitchVibration()
	{
		GameSettings.SwitchVibration();
	}
}

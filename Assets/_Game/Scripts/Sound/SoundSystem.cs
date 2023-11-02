using UnityEngine;

public class SoundSystem : MonoBehaviour
{
	[Header("Background"), Space(5)]
	[SerializeField] private AudioSource _backgroundMusic;

	[Header("Environment"), Space(5)]
	[SerializeField] private AudioSource _windSound;

	[Header("Ball"), Space(5)]
	[SerializeField] private AudioSource _jumpSound;
	[SerializeField] private Vector2 _jumpVolumeRange;

	[SerializeField, Space(5)] private AudioSource _crashSound;

	[Header("UI"), Space(5)]
	[SerializeField] private AudioSource _clickSound;
	[SerializeField] private AudioSource _countDown;

	private static SoundSystem _instance;

	private void Awake()
	{
		if (_instance == null)
		{
			_instance = this;
		}
	}

	private void OnEnable()
	{
		GameSettings.SettingsChanged += UpdateBackgroundMusicState;
		GameSettings.SettingsChanged += UpdateWindSoundState;
	}

	private void OnDisable()
	{
		GameSettings.SettingsChanged -= UpdateBackgroundMusicState;
		GameSettings.SettingsChanged -= UpdateWindSoundState;
	}

	private void Start()
	{
		UpdateBackgroundMusicState();
		UpdateWindSoundState();
	}

	public static void PlayJumpSound()
	{
		if (_instance == null)
		{
			return;
		}

		if (GameSettings.SoundFXEnabled == false)
		{
			return;
		}

		if (_instance._jumpSound == null)
		{
			return;
		}

		_instance._jumpSound.volume = Random.Range(_instance._jumpVolumeRange.x, _instance._jumpVolumeRange.y);
		_instance._jumpSound.Play();
	}

	public static void PlayClickSound()
	{
		if (_instance == null)
		{
			return;
		}

		if (GameSettings.SoundFXEnabled == false)
		{
			return;
		}

		if (_instance._clickSound == null)
		{
			return;
		}

		_instance._clickSound.Play();
	}

	public static void PlayCountDownSound()
	{
		if (_instance == null)
		{
			return;
		}

		if (GameSettings.SoundFXEnabled == false)
		{
			return;
		}

		if (_instance._countDown == null)
		{
			return;
		}

		_instance._countDown.Play();
	}

	public static void PlayCrashSound()
	{
		if (_instance == null)
		{
			return;
		}

		if (GameSettings.SoundFXEnabled == false)
		{
			return;
		}

		if (_instance._crashSound == null)
		{
			return;
		}

		_instance._crashSound.Play();
	}

	private void UpdateBackgroundMusicState()
	{
		_backgroundMusic.gameObject.SetActive(GameSettings.BackgroundMusicEnabled);
	}

	private void UpdateWindSoundState()
	{
		_windSound.gameObject.SetActive(GameSettings.SoundFXEnabled);
	}
}

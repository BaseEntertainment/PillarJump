using UnityEngine;

public class SoundSystem : MonoBehaviour
{
	[Header("Background"), Space(5)]
	[SerializeField] private AudioSource _backgroundMusic;

	[Header("Environment"), Space(5)]
	[SerializeField] private AudioSource _windSound;

	[Header("Ball"), Space(5)]
	[SerializeField] private AudioSource _bounceSound;
	[SerializeField] private Vector2 _bounceVolumeRange = new Vector2(0.2f, 0.3f);

	[SerializeField, Space(5)] private AudioSource _crashSound;

	[Header("UI"), Space(5)]
	[SerializeField] private AudioSource _clickSound;
	[SerializeField] private AudioSource _notification;
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

	public static void PlayBounceSound()
	{
		if (_instance == null)
		{
			return;
		}

		if (GameSettings.SoundFXEnabled == false)
		{
			return;
		}

		if (_instance._bounceSound == null)
		{
			return;
		}

		_instance._bounceSound.volume = Random.Range(_instance._bounceVolumeRange.x, _instance._bounceVolumeRange.y);
		_instance._bounceSound.Play();
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

	public static void PlayNotificationSound()
	{
		if (_instance == null)
		{
			return;
		}

		if (GameSettings.SoundFXEnabled == false)
		{
			return;
		}

		if (_instance._notification == null)
		{
			return;
		}

		_instance._notification.Play();
	}

	public static void StopBackgroundMusic()
	{
		if (_instance == null)
		{
			return;
		}

		_instance.SetActivateBackgroundMusic(false);
	}

	public static void PlayBackgroundMusic()
	{
		if (_instance == null)
		{
			return;
		}

		if (GameSettings.BackgroundMusicEnabled == false)
		{
			return;
		}

		_instance.SetActivateBackgroundMusic(true);
	}

	private void UpdateBackgroundMusicState()
	{
		SetActivateBackgroundMusic(GameSettings.BackgroundMusicEnabled);
	}

	private void SetActivateBackgroundMusic(bool active)
	{
		_backgroundMusic.gameObject.SetActive(active);
	}

	private void UpdateWindSoundState()
	{
		_windSound.gameObject.SetActive(GameSettings.SoundFXEnabled);
	}
}

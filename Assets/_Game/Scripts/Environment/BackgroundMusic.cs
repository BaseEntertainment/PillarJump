using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusic : MonoBehaviour
{
	[SerializeField] private bool _smothStart = true;
	[SerializeField, Range(0, 10)] private float _volumeTransitionSpeed = 2.0f;

	private AudioSource _audioSource;

	private float _initialVolume;

	private void Awake()
	{
		_audioSource = GetComponent<AudioSource>();

		_initialVolume = _audioSource.volume;
	}

	private void OnEnable()
	{
		if (_smothStart == false)
		{
			return;
		}

		SmoothStartAudio();
	}

	private void UpdateVolume(float volume)
	{
		_audioSource.volume = volume;
	}

	private void SmoothStartAudio()
	{
		float valueToTween = 0;

		DOTween.To(() => valueToTween, x => valueToTween = x, _initialVolume, _volumeTransitionSpeed).OnUpdate(() => UpdateVolume(valueToTween));
	}
}

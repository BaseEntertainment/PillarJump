using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusic : MonoBehaviour
{
	[SerializeField, Range(0, 2)] private float _volumeTransitionSpeed = 2.0f;

	private AudioSource _audioSource;

	private float _initialVolume;

	private void Awake()
	{
		_audioSource = GetComponent<AudioSource>();

		_initialVolume = _audioSource.volume;
	}

	private void OnEnable()
	{
		float valueToTween = 0;

		DOTween.To(() => valueToTween, x => valueToTween = x, _initialVolume, _volumeTransitionSpeed).OnUpdate(() => UpdateVolume(valueToTween));
	}

	private void UpdateVolume(float volume)
	{
		_audioSource.volume = volume;
	}
}

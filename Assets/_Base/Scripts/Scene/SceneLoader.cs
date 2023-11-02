using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	[SerializeField] private Animator _transitionAnimator;
	[SerializeField] private string _transitionTriggerName = "Start";

	[SerializeField, Range(0, 5.0f)] private float _loadSceneDelay = 1.0f;


	public static event Action LoadingStarted;

	public static event Action LoadingFinished;

	public void LoadNextScene()
	{
		int allScenesMaxIndex = SceneManager.sceneCount;
		int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

		if (nextSceneIndex > allScenesMaxIndex)
		{
			Debug.LogError("SceneLoader: There are no next scene.");
			return;
		}

		StartLoading(nextSceneIndex);
	}

	public void LoadPreviousScene()
	{
		int previousSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;

		if (previousSceneIndex < 0)
		{
			Debug.LogError("SceneLoader: There are no previous scene.");
		}

		StartLoading(previousSceneIndex);
	}

	public void RestartScene()
	{
		StartLoading(SceneManager.GetActiveScene().buildIndex);
	}

	private void StartLoading(int scenebuildIndex)
	{
		StartCoroutine(LoadSceneAsync(scenebuildIndex));
	}

	private void OnStartLoading()
	{
		_transitionAnimator.SetTrigger(_transitionTriggerName);

		LoadingStarted?.Invoke();
	}
	private void OnFinishLoading()
	{
		LoadingFinished?.Invoke();
	}

	private IEnumerator LoadSceneAsync(int sceneBuildIndex)
	{
		OnStartLoading();

		var asyncLoad = SceneManager.LoadSceneAsync(sceneBuildIndex);
		asyncLoad.allowSceneActivation = false;

		while (!asyncLoad.isDone)
		{
			if (asyncLoad.progress >= 0.9f)
			{
				yield return new WaitForSecondsRealtime(_loadSceneDelay);

				asyncLoad.allowSceneActivation = true;
			}

			yield return null;
		}

		OnFinishLoading();
	}
}
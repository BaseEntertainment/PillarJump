using UnityEngine;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
	void Start()
	{

	}


	public void ReloadScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	public float delay = 0.0f;
	public int buildIndex = 0;

	private void LoadScene()
	{
		SceneManager.LoadScene(buildIndex);
	}

	private void Awake()
	{
		if (delay <= 0.0f)
		{
			LoadScene();
		}
		else
		{
			StartCoroutine(DoIt());
		}
	}

	private IEnumerator DoIt()
	{
		yield return new WaitForSeconds(delay);
		LoadScene();
	}
}

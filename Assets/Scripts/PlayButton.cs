using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
	public SceneLoader sceneLoader;

	public void OnClick()
	{
		sceneLoader.buildIndex = 1;
		sceneLoader.gameObject.SetActive(true);
	}
}

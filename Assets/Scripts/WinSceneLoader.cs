using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinSceneLoader : MonoBehaviour
{
	public SceneLoader sceneLoader;

	private void OnTriggerEnter(Collider other)
	{
		var player = other.GetComponentInParent<PlayerCharacterController>();
		if (player)
		{
			sceneLoader.gameObject.SetActive(true);
		}
	}
}

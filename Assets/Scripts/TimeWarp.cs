using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeWarp : MonoBehaviour
{
	public float increaseRate = 2.0f;

	private void Update()
	{
#if UNITY_EDITOR
		if (Input.GetKey(KeyCode.T))
		{
			Time.timeScale *= (1.0f + Time.unscaledDeltaTime);
			Debug.Log(Time.timeScale);
		}
		else if (Input.GetKeyUp(KeyCode.T))
		{
			Time.timeScale = 1.0f;
		}
#endif

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			QuitButton.Quit();
		}
	}
}

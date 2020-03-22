using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <remarks>Put this on parent of `visuals`.</remarks>
/// <author>Elijah Shadbolt</author>
/// 
public class Caught : MonoBehaviour
{
	public GameObject visuals;
	public bool hasTriggered { get; private set; } = false;

	private void Awake()
	{
		visuals.SetActive(false);
	}

	public void Show()
	{
		if (hasTriggered) { return; }
		hasTriggered = true;
		visuals.SetActive(true);
		StartCoroutine(RestartCo());
	}

	private IEnumerator RestartCo()
	{
		yield return new WaitForSeconds(3);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}

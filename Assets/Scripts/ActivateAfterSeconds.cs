using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAfterSeconds : MonoBehaviour
{
	public float delay = 1.0f;

	public GameObject thingToActivate;

	private void Start()
	{
		if (delay <= 0.0f)
		{
			thingToActivate.SetActive(true);
		}
		else
		{
			StartCoroutine(DoIt());
		}
	}

	private IEnumerator DoIt()
	{
		yield return new WaitForSeconds(delay);
		thingToActivate.SetActive(true);
	}
}

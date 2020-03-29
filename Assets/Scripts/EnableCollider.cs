using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableCollider : MonoBehaviour
{
	public GameObject enableThis;
	public GameObject disableThis;

	private void OnTriggerEnter(Collider other)
	{
		if (disableThis) { disableThis.SetActive(false); }
		if (enableThis) { enableThis.SetActive(true); }
	}

	private void OnTriggerExit(Collider other)
	{
		if (disableThis) { disableThis.SetActive(true); }
		if (enableThis) { enableThis.SetActive(false); }
	}
}

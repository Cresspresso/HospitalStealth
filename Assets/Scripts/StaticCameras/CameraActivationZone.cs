using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attach to a Trigger Collider.
/// </summary>
/// <author>Elijah Shadbolt</author>
public class CameraActivationZone : MonoBehaviour
{
	public SpecificCamera cam;

	// for enabled checkbox in inspector
	private void Start()
	{
		
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!enabled) { return; }
		CameraManager.main.current = cam;
	}
}

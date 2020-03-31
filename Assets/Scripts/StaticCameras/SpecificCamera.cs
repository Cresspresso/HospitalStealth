using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <author>Elijah Shadbolt</author>
public class SpecificCamera : MonoBehaviour
{
	public Transform parentForCamera;

	private Transform GetCameraTransform() => Camera.main.transform;

	/// <author>Elijah Shadbolt</author>
	public void OnActivate()
	{
		if (!parentForCamera) { parentForCamera = transform; }
		var t = GetCameraTransform();
		t.SetParent(parentForCamera);
		t.localPosition = Vector3.zero;
		t.localRotation = Quaternion.identity;
		gameObject.SetActive(true);
	}

	/// <author>Elijah Shadbolt</author>
	public void OnDeactivate()
	{
		var t = GetCameraTransform();
		t.SetParent(transform.parent);
		gameObject.SetActive(false);
	}
}

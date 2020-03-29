using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <author>Elijah Shadbolt</author>
public class StaticCameraLookAt : MonoBehaviour
{
	public Transform roomCentre;
	public Transform target;
	public Transform computedPoint;
	public float lerp = 0.5f;

	private void Awake()
	{
		if (!computedPoint)
		{
			var go = new GameObject("ComputedPoint for StaticCameraLookAt");
			computedPoint = go.transform;
		}

		if (!target)
		{
			target = FindObjectOfType<playerController>().transform;
		}
	}

	private void LateUpdate()
	{
		computedPoint.position = Vector3.Lerp(roomCentre.position, target.position, lerp);
		transform.LookAt(computedPoint);
	}
}

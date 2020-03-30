using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <author>Elijah Shadbolt</author>
public class StaticCameraLookAt : MonoBehaviour
{
	/// <author>Elijah Shadbolt</author>
	public Transform roomCentre;
	public Transform target;
	public Transform computedPoint;
	public float lerp = 0.5f;

	/// <author>Elijah Shadbolt</author>
	private Vector3 ComputeTargetPoint()
	{
#if UNITY_EDITOR
		if (!UnityEditor.EditorApplication.isPlaying)
		{
			return roomCentre.position;
		}
#endif
		return Vector3.Lerp(roomCentre.position, target.position, lerp);
	}

	/// <author>Elijah Shadbolt</author>
	private void LateUpdate()
	{
		if (!roomCentre)
		{
			roomCentre = transform.parent;
		}

		if (!target)
		{
			var player = FindObjectOfType<playerController>();
			if (player)
			{
				target = player.transform;
			}
		}

		if (roomCentre && target)
		{
			var focus = ComputeTargetPoint();
			if (computedPoint) { computedPoint.position = focus; }
			transform.rotation = Quaternion.LookRotation(focus - transform.position);
		}
	}
}

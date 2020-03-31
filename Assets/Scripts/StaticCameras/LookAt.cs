using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <author>Elijah Shadbolt</author>
public class LookAt : MonoBehaviour
{
	/// <author>Elijah Shadbolt</author>
	public Transform target;
	public Transform roomCentre;

	[Tooltip("0 for centre, 1 for target")]
	public float lerp = 0.5f;

	/// <author>Elijah Shadbolt</author>
	private void OnDrawGizmosSelected()
	{
		if (roomCentre)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(roomCentre.position, 0.3f);
		}

		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, 0.3f);
		if (roomCentre) { Gizmos.DrawLine(transform.position, roomCentre.position); }
	}

	/// <author>Elijah Shadbolt</author>
	private Vector3 ComputeTargetPoint()
	{
		if (roomCentre)
		{
#if UNITY_EDITOR
			if (!UnityEditor.EditorApplication.isPlaying)
		{
			return roomCentre.position;
		}
#endif
			return Vector3.Lerp(roomCentre.position, target.position, lerp);
		}
		else
		{
			return target.position;
		}
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
			var player = FindObjectOfType<PlayerCharacterController>();
			if (player)
			{
				target = player.transform;
			}
		}

		if (roomCentre && target)
		{
			var point = ComputeTargetPoint();
			transform.rotation = Quaternion.LookRotation(point - transform.position);
		}
	}
}

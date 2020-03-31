using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <author>Elijah Shadbolt</author>
public class Dolly : MonoBehaviour
{
	/// <author>Elijah Shadbolt</author>
	public Transform target;

	[Tooltip("Point on line that the target will move along.")]
	public Transform nearPointA;
	[Tooltip("Point on line that the target will move along.")]
	public Transform nearPointB;

	[Tooltip("Point on line to move this transform along.")]
	public Transform dollyPointA;
	[Tooltip("Point on line to move this transform along.")]
	public Transform dollyPointB;

	public bool clampToLineSegment = true;

	/// <author>Elijah Shadbolt</author>
	private void OnDrawGizmosSelected()
	{
		if (nearPointA || nearPointB)
		{
			Gizmos.color = Color.red;
			if (nearPointA) { Gizmos.DrawWireSphere(nearPointA.position, 0.3f); }
			if (nearPointB) { Gizmos.DrawWireSphere(nearPointB.position, 0.3f); }
			if (nearPointA && nearPointB) { Gizmos.DrawLine(nearPointA.position, nearPointB.position); }
		}

		if (dollyPointA || dollyPointB)
		{
			Gizmos.color = Color.blue;
			if (dollyPointA) { Gizmos.DrawWireSphere(dollyPointA.position, 0.3f); }
			if (dollyPointB) { Gizmos.DrawWireSphere(dollyPointB.position, 0.3f); }
			if (dollyPointA && dollyPointB) { Gizmos.DrawLine(dollyPointA.position, dollyPointB.position); }
		}
	}

	private void LateUpdate()
	{
		/// <author>Elijah Shadbolt</author>
		if (!target)
		{
			var player = FindObjectOfType<PlayerCharacterController>();
			if (player)
			{
				target = player.transform;
			}
		}

		/// <author>Elijah Shadbolt</author>
		if (target
			&& nearPointA && nearPointB
			&& dollyPointA && dollyPointB)
		{
			var C = nearPointA.position;
			var B = nearPointB.position;
			var A = target.position;
			// project point A onto line segment CB
			Vector3 a = A - C;
			Vector3 b = B - C;
			float m = b.magnitude;
			Vector3 n = b / m; // normal along vector b
			float s = Vector3.Dot(a, n); // length of projected vector r
			float t = s / m; // normalised lerp distance
			if (clampToLineSegment) { t = Mathf.Clamp01(t); }

			// compute final position of this transform
			transform.position = Vector3.LerpUnclamped(
				dollyPointA.position,
				dollyPointB.position,
				t);
		}
	}
}

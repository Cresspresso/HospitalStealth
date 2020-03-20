﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <author>Elijah Shadbolt</author>
public class PatrolPoint : MonoBehaviour
{
	/// <author>Elijah Shadbolt</author>
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(transform.position, 0.3f);
	}
}

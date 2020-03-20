using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <author>Elijah Shadbolt</author>
public class PatrolRoute : MonoBehaviour
{
	/// <author>Elijah Shadbolt</author>
	private PatrolPoint[] m_points;
	public PatrolPoint[] points { get { if (m_points == null) { m_points = GetComponentsInChildren<PatrolPoint>(); } return m_points; } }
}

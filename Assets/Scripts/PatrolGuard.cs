using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <author>Elijah Shadbolt</author>
public class PatrolGuard : MonoBehaviour
{
	/// <author>Elijah Shadbolt</author>
	private NavMeshAgent m_agent;
	public NavMeshAgent agent { get { if (!m_agent) {  m_agent = GetComponent<NavMeshAgent>(); } return m_agent; } }

	/// <author>Elijah Shadbolt</author>
	public Transform feetLocation;
	public PatrolRoute route;
	private int currentRoutePointIndex = 0;

	/// <author>Elijah Shadbolt</author>
	private void SetPointIndex(int i)
	{
		currentRoutePointIndex = i;
		agent.destination = route.points[i].transform.position;
	}

	/// <author>Elijah Shadbolt</author>
	private void Start()
	{
		Debug.Assert(feetLocation, $"{nameof(feetLocation)} is null", this);
		Debug.Assert(route, $"{nameof(route)} is null", this);

		SetPointIndex(currentRoutePointIndex);
	}

	/// <author>Elijah Shadbolt</author>
	private void Update()
	{
		var range = 1.0f;
		if (Vector3.Distance(agent.destination, feetLocation.position) < range)
		{
			SetPointIndex((currentRoutePointIndex + 1) % route.points.Length);
		}
	}
}

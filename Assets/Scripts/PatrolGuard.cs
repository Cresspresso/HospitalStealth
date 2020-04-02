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

	[SerializeField]
	private PatrolRoute m_route;
	public PatrolRoute route { get { if (!m_route) { m_route = GetComponentInParent<PatrolRoute>(); } return m_route; } }

	private int currentRoutePointIndex = 0;
	private bool isWaiting = false;
	public float arrivedDistance = 1.0f;

	public NurseAnimController nurse;

	/// <author>Elijah Shadbolt</author>
	private void SetPointIndex(int i)
	{
		currentRoutePointIndex = i;
		agent.destination = route.points[i].transform.position;
		isWaiting = false;
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
		if (nurse)
		{
			if (nurse.isWalking)
			{
				if (agent.remainingDistance < 0.01f)
				{
					nurse.isWalking = false;
				}
			}
			else
			{
				if (agent.remainingDistance > 0.01f)
				{
					nurse.isWalking = true;
				}
			}
		}

		if (isWaiting) { return; }
		if (agent.remainingDistance <= arrivedDistance)
		{
			var waitTime = route.points[currentRoutePointIndex].waitTime;
			var i = (currentRoutePointIndex + 1) % route.points.Length;
			if (waitTime > 0.0f)
			{
				isWaiting = true;
				StartCoroutine(WaitSet(waitTime, i));
			}
			else
			{
				SetPointIndex(i);
			}
		}
	}
	 
	private IEnumerator WaitSet(float t, int i)
	{
		yield return new WaitUntil(() => agent.remainingDistance < 0.01f);
		yield return new WaitForSeconds(t);
		SetPointIndex(i);
	}
}

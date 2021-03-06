﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Detects and attacks a player.
/// DEPRECATED
/// </summary>
/// <author>Elijah Shadbolt</author>
public class GuardAttack : MonoBehaviour
{
	/// <author>Elijah Shadbolt</author>
	private playerController m_targetPlayer;
	public playerController targetPlayer { get { if (!m_targetPlayer) { m_targetPlayer = FindObjectOfType<playerController>(); } return m_targetPlayer; } }

	/// <author>Elijah Shadbolt</author>
	private PatrolGuard m_patrol;
	public PatrolGuard patrol { get { if (!m_patrol) { m_patrol = GetComponent<PatrolGuard>(); } return m_patrol; } }

	public enum StateType
	{
		Patrolling,
		Alerted,
		Pursuing,
		Confused,
	}
	public StateType state { get; private set; }
	private float duration = 2.0f;

	private void Awake()
	{
		state = StateType.Patrolling;
	}

	private void Update()
	{
		if (state == StateType.Confused
			|| state == StateType.Alerted)
		{
			return;
		}

		/// <author>Elijah Shadbolt</author>
		/// 
		Vector3 vecToPlayer = targetPlayer.transform.position - transform.position;
		float sqrRange = vecToPlayer.sqrMagnitude;
		float maxRange = 30.0f;
		float a = Vector3.Angle(vecToPlayer, transform.forward);
		if (sqrRange < maxRange * maxRange && a < 30.0f)
		{
			if (state == StateType.Patrolling)
			{
				StartCoroutine(AlertedCo());
			}
			else
			{
				state = StateType.Pursuing;
				// detected a target.
				patrol.enabled = false;
				patrol.agent.destination = targetPlayer.transform.position;

				/// <author>Elijah Shadbolt</author>
				/// 
				float minRange = 1.2f;
				if (sqrRange < minRange * minRange)
				{
					targetPlayer.isInputEnabled = false;

					this.enabled = false;
					patrol.enabled = false;
					patrol.agent.enabled = false;

					var c = FindObjectOfType<Caught>();
					if (c)
					{
						c.Show();
					}
				}
			}
		}
		else
		{
			if (state == StateType.Pursuing)
			{
				StartCoroutine(ConfusedCo());
			}
			else
			{
				state = StateType.Patrolling;
				patrol.enabled = true;
			}
		}
	}

	private IEnumerator AlertedCo()
	{
		state = StateType.Alerted;
		patrol.enabled = false;
		patrol.agent.destination = patrol.agent.transform.position;
		yield return new WaitForSeconds(duration);
		state = StateType.Pursuing;
	}

	private IEnumerator ConfusedCo()
	{
		state = StateType.Confused;
		patrol.enabled = false;
		patrol.agent.destination = patrol.agent.transform.position;
		yield return new WaitForSeconds(duration);
		state = StateType.Patrolling;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Detects and attacks a player.
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

	private void Update()
	{
		/// <author>Elijah Shadbolt</author>
		/// 
		Vector3 vecToPlayer = targetPlayer.transform.position - transform.position;
		float sqrRange = vecToPlayer.sqrMagnitude;
		float maxRange = 30.0f;
		float a = Vector3.Angle(vecToPlayer, transform.forward);
		if (sqrRange < maxRange * maxRange && a < 30.0f)
		{
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
		else
		{
			patrol.enabled = true;
		}
	}
}

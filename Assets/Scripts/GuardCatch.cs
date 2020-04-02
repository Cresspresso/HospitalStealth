using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <author>Elijah Shadbolt</author>
/// 
public class GuardCatch : MonoBehaviour
{
	public float radius = 1.0f;

	private void Update()
	{
		/// <author>Elijah Shadbolt</author>
		/// 
		var player = FindObjectOfType<PlayerCharacterController>();
		if (player)
		{
			var d = Vector3.Distance(player.transform.position, transform.position);
			if (d < radius)
			{
				player.isInputEnabled = false;

				var pg = GetComponent<PatrolGuard>();
				{
					if (pg)
					{
						pg.enabled = false;
					}

					if (pg.nurse)
					{
						pg.nurse.Grab();
						//pg.nurse.isWalking = false;
					}
				}

				var agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
				if (agent)
				{
					agent.enabled = false;
				}

				var c = FindObjectOfType<Caught>();
				if (c)
				{
					c.Show();
				}
				else
				{
					Debug.LogError($"{nameof(Caught)} not found in scene");
				}
			}
		}
		else
		{
			Debug.LogError($"{nameof(playerController)} not found in scene");
		}
	}
}

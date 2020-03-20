using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.VFX;

/// <author>Elijah Shadbolt</author>
public class BlinkTeleport : MonoBehaviour
{
	public bool isPreparing { get; private set; } = false;

	/// <author>Elijah Shadbolt</author>
	public float maxDistance = 50.0f;
	public float playerCharacterRadius = 0.5f;
	public LayerMask hitMask = ~0;
	public playerController player;
	public VisualEffect effect;

	private bool DoTheCast(out Ray ray, out RaycastHit hit)
	{
		/// <author>Elijah Shadbolt</author>
		ray = new Ray(transform.position, transform.forward);
		var allHits = Physics.SphereCastAll(ray, playerCharacterRadius, maxDistance, hitMask, QueryTriggerInteraction.Ignore);
		var hits =
			from h in allHits
			let player = h.collider.GetComponentInParent<playerController>()
			where !player
			orderby h.distance
			select h;
		if (hits.Any())
		{
			hit = hits.First();
			return true;
		}
		else
		{
			hit = new RaycastHit();
			return false;
		}
	}

	private Vector3 CalcDestination()
	{
		Ray ray;
		if (DoTheCast(out ray, out RaycastHit hit))
		{
			return ray.origin + Vector3.Project(hit.point - ray.origin, ray.direction);
		}
		else
		{
			return ray.GetPoint(maxDistance);
		}
	}

	private void StopPreparing()
	{
		isPreparing = false;
		effect.SendEvent("OnStop");
	}

	private void Update()
	{
		/// <author>Elijah Shadbolt</author>
		if (Input.GetKeyDown(KeyCode.E))
		{
			if (isPreparing)
			{
				StopPreparing();
			}
			else
			{
				isPreparing = true;
				effect.SendEvent("OnPlay");
			}
		}

		/// <author>Elijah Shadbolt</author>
		if (isPreparing)
		{
			if (Input.GetButtonDown("Fire1"))
			{
				// Use the power.
				/// <author>Elijah Shadbolt</author>
				StopPreparing();

				// Teleport the player.
				player.controller.enabled = false;
				player.transform.position = CalcDestination();
				player.controller.enabled = true;
			}
			else if (Input.GetButtonDown("Fire2"))
			{
				// Cancel the power.
				/// <author>Elijah Shadbolt</author>
				StopPreparing();
			}
		}
	}

	private void LateUpdate()
	{
		/// <author>Elijah Shadbolt</author>
		if (isPreparing)
		{
			if (effect)
			{
				effect.transform.position = CalcDestination();
			}
		}
	}
}

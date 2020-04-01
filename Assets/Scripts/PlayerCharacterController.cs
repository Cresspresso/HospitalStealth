using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A character controller to work with the static camera system.
/// Up/Down (W/S) to move forwards or backwards relative to this character.
/// Left/Right (A/D) to rotate the character clockwise/anticlockwise when viewed from above.
/// </summary>
/// <author>Elijah Shadbolt</author>
public class PlayerCharacterController : MonoBehaviour
{
	/// <author>Elijah Shadbolt</author>
	private CharacterController m_cc;
	public CharacterController cc { get { if (!m_cc) { m_cc = GetComponent<CharacterController>(); } return m_cc; } }

	/// <author>Elijah Shadbolt</author>
	///
	private Vector3 m_velocity = Vector3.zero;
	public Vector3 velocity {
		get => m_velocity;
		set
		{
			m_velocity = value;
		}
	}

	public bool isTouchingGround { get; private set; } = false;

	/// <author>Elijah Shadbolt</author>
	public float rotateSpeed = 100.0f;
	public float moveForwardSpeed = 3.0f;
	public float moveBackwardSpeed = 3.0f;

	public float currentAngularSpeed { get; private set; } = 0;
	public float currentSpeed { get; private set; } = 0;



	private void FixedUpdate()
	{
		/// <author>Elijah Shadbolt</author>
		var dt = Time.fixedDeltaTime;
		var gravity = Physics.gravity;
		float horizontalAmount = Input.GetAxis("Horizontal");
		float verticalAmount = Input.GetAxis("Vertical");
		float small = 0.001f;

		// angular motion
		/// <author>Elijah Shadbolt</author>
		var forward = Vector3.ProjectOnPlane(transform.forward, gravity).normalized;
		//var right = Vector3.ProjectOnPlane(transform.right, gravity).normalized;
		var rotation = Quaternion.LookRotation(forward, -gravity);

		float angularSpeed = horizontalAmount * rotateSpeed;
		if (verticalAmount < 0) { angularSpeed = -angularSpeed; }
		var localAngularVelocity = new Vector3(0, angularSpeed, 0);
		var localAngularMotion = localAngularVelocity * dt;
		this.currentAngularSpeed = angularSpeed;

		// linear motion
		/// <author>Elijah Shadbolt</author>
		var velocity = this.velocity;

		velocity += Physics.gravity * dt;
		velocity = Vector3.Project(velocity, Physics.gravity);

		float verticalSpeed = verticalAmount > small
			? moveForwardSpeed * verticalAmount
			: (verticalAmount < -small
			? moveBackwardSpeed * verticalAmount
			: 0.0f);
		this.currentSpeed = verticalSpeed;
		velocity += forward * verticalSpeed;

		var motion = velocity * dt;

		// apply the linear motion
		var flags = cc.Move(motion);
		isTouchingGround = (flags & CollisionFlags.Below) != 0;
		velocity = cc.velocity;

		if (isTouchingGround)
		{
			if (velocity.y > 0)
			{
				velocity.y = 0;
			}
		}

		this.velocity = velocity;

		// apply the rotational motion
		/// <author>Elijah Shadbolt</author>
		transform.rotation = rotation;
		transform.Rotate(localAngularMotion, Space.Self);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterAnimator : MonoBehaviour
{
	public float speedMultiplier = 1.0f;

	private enum State { Idle, Walking, }
	private State state;

	private Animator m_anim;
	public Animator anim { get { if (!m_anim) m_anim = FindObjectOfType<Animator>(); return m_anim; } }

	[SerializeField]
	private PlayerCharacterController m_pcc;
	public PlayerCharacterController pcc { get { if (!m_pcc) { m_pcc = GetComponentInParent<PlayerCharacterController>(); } return m_pcc; } }


	private void UpdateSpeed()
	{
		var v = Input.GetAxisRaw("Vertical");
		var h = Input.GetAxisRaw("Horizontal");
		var s = Mathf.Clamp(v + 0.3f * Mathf.Abs(h), -1, 1);
		var a = pcc ? pcc.moveForwardSpeed : 1;
		var b = pcc ? pcc.moveBackwardSpeed : 1;
		var t = v > 0 ? a : (v < 0 ? b : 0);
		anim.SetFloat("Speed", s * t * speedMultiplier);
		//var s = pcc.currentSpeed;
		//anim.SetFloat("Speed", s > 0 ? 1 : (s < 0 ? -1 : 0));
	}

	private void Update()
	{
		switch (state)
		{
			default:
			case State.Idle:
				{
					if (!(0 == Input.GetAxisRaw("Vertical")
						&& 0 == Input.GetAxisRaw("Horizontal")))
					{
						state = State.Walking;
						anim.SetTrigger("Walking");
						UpdateSpeed();
					}
				} break;
			case State.Walking:
				{
					UpdateSpeed();

					if (0 == Input.GetAxisRaw("Vertical")
						&& 0 == Input.GetAxisRaw("Horizontal"))
					{
						state = State.Idle;
						anim.SetTrigger("Idle");
					}
				} break;
		}
	}
}

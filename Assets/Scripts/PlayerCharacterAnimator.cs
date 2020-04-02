using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterAnimator : MonoBehaviour
{
	public float speedMultiplier = 1.0f;

	private enum State { Idle, Walking, }
	private State state;

	private Animator m_anim;
	public Animator anim { get { if (!m_anim) m_anim = GetComponent<Animator>(); return m_anim; } }

	[SerializeField]
	private PlayerCharacterController m_pcc;
	public PlayerCharacterController pcc { get { if (!m_pcc) { m_pcc = GetComponentInParent<PlayerCharacterController>(); } return m_pcc; } }


	private void UpdateSpeed(float v, float h)
	{
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
		float v = pcc.isInputEnabled ? Input.GetAxisRaw("Vertical") : 0;
		float h = pcc.isInputEnabled ? Input.GetAxisRaw("Horizontal") : 0;
		switch (state)
		{
			default:
			case State.Idle:
				{
					if (!(0 == v && 0 == h))
					{
						state = State.Walking;
						anim.SetTrigger("Walking");
						UpdateSpeed(v, h);
					}
				} break;
			case State.Walking:
				{
					UpdateSpeed(v, h);

					if (0 == v && 0 == h)
					{
						state = State.Idle;
						anim.SetTrigger("Idle");
					}
				} break;
		}
	}
}

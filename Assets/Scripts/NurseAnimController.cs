using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NurseAnimController : MonoBehaviour
{
	private Animator m_anim;
	public Animator anim { get { if (!m_anim) { m_anim = GetComponent<Animator>(); } return m_anim; } }

	public bool isWalking {
		get => anim.GetBool("IsWalking");
		set => anim.SetBool("IsWalking", value);
	}

	public void Grab() => anim.SetTrigger("Grab");
}

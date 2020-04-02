using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
	public float delay = 1.0f;

	private void Awake()
	{
		Destroy(gameObject, delay);
	}
}

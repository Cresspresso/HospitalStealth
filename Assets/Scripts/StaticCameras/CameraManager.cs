using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attach to the Main Camera.
/// </summary>
/// <author>Elijah Shadbolt</author>
public class CameraManager : MonoBehaviour
{
	/// <author>Elijah Shadbolt</author>
	[SerializeField]
	private SpecificCamera m_current;
	public SpecificCamera current {
		get => m_current;
		set
		{
			if (m_current) { m_current.OnDeactivate(); }
			m_current = value;
			value.OnActivate();
		}
	}

	public static CameraManager main => Camera.main.GetComponent<CameraManager>();

	private void Start()
	{
		/// <author>Elijah Shadbolt</author>
		foreach (var cam in FindObjectsOfType<SpecificCamera>())
		{
			cam.OnDeactivate();
		}
		m_current.OnActivate();
	}
}

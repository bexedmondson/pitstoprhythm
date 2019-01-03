using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TappableObject : MonoBehaviour 
{
	[SerializeField]
	public TapObjectType m_type;

	public Animator m_animator;

	private void Awake()
	{
		m_animator = this.gameObject.GetComponent<Animator>();
	}
}

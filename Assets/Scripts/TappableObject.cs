using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TappableObject : MonoBehaviour, IPointerClickHandler
{
	[SerializeField]
	public TapObjectType m_tapObjectType;

	public Animator m_animator;

	public void OnPointerClick(PointerEventData eventData)
	{
		EventManager.TriggerEvent(EventManager.TappableObjectTap, m_tapObjectType);
	}

	private void Awake()
	{
		m_animator = this.gameObject.GetComponent<Animator>();
	}
}

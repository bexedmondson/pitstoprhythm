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
		switch (m_tapObjectType)
		{
            case TapObjectType.WheelBottomLeft:
                EventManager.TriggerEvent(EventManager.WheelBLTap);
				break;
			case TapObjectType.WheelBottomRight:
                EventManager.TriggerEvent(EventManager.WheelBRTap);
				break;
			case TapObjectType.WheelTopLeft:
                EventManager.TriggerEvent(EventManager.WheelTLTap);
				break;
			case TapObjectType.WheelTopRight:
                EventManager.TriggerEvent(EventManager.WheelTRTap);
                break;
		}
	}

	private void Awake()
	{
		m_animator = this.gameObject.GetComponent<Animator>();
	}
}

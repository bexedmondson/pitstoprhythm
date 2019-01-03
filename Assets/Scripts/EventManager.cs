using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class TapObjectEvent : UnityEvent<TapObjectType>
{
}

public class EventManager : MonoBehaviour
{
	//public static string SwipeUp = "SwipeUp";
	//public static string SwipeDown = "SwipeDown";
	public static string TappableObjectTap = "WheelTap";


	private Dictionary<string, TapObjectEvent> eventDictionary;

    private static EventManager eventManager;

    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManager script on a GameObject in your scene.");
                }
                else
                {
                    eventManager.Init();
                }
            }

            return eventManager;
        }
    }

    void Init()
    {
        if (eventDictionary == null)
        {
			eventDictionary = new Dictionary<string, TapObjectEvent>();
        }
    }

	public static void StartListening(string eventName, UnityAction<TapObjectType> listener)
    {
		TapObjectEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
			thisEvent = new TapObjectEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

	public static void StopListening(string eventName, UnityAction<TapObjectType> listener)
    {
        if (eventManager == null) return;
		TapObjectEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    /*public static void TriggerEvent(string eventName)
    {
		TapObjectEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }*/

	public static void TriggerEvent(string eventName, TapObjectType tapObjectType)
	{
		TapObjectEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
			thisEvent.Invoke(tapObjectType);
        }
	}
}
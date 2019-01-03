using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour 
{
	public Animator m_carAnimator;

	public List<TappableObject> tappableObjects;

	private void Awake()
    {
        EventManager.StartListening(EventManager.SwipeDown, CarEnter);
    }

	void CarEnter()
	{
		m_carAnimator.SetTrigger("Enter");
	}

	public void AnimateForNote(NoteData note)
	{
		for (int i = 0; i < tappableObjects.Count; i++)
		{
			if (note.tapObject == tappableObjects[i].m_type)
				tappableObjects[i].m_animator.SetTrigger("LeadUp");
		}
	}
}

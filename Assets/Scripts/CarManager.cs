using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour 
{
	public Animator m_carAnimator;

	public List<TappableObject> tappableObjects;

	public void CarEnter()
	{
		m_carAnimator.SetTrigger("Enter");
	}

	public void AnimateForNote(NoteData note)
	{
		for (int i = 0; i < tappableObjects.Count; i++)
		{
			if (note.tapObject == tappableObjects[i].m_tapObjectType)
				tappableObjects[i].m_animator.SetTrigger("LeadUp");
		}
	}
}

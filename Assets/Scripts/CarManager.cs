using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour 
{
	public Animator m_carAnimator;

	public List<TappableObject> tappableObjects;
    
	public AudioClip driveIn;

	public AudioClip driveOut;

	public void CarEnter()
	{
		m_carAnimator.SetTrigger("Enter");

		GetComponent<AudioSource>().PlayOneShot(driveIn);
	}

	public void CarExit()
    {
        m_carAnimator.SetTrigger("Exit");

		GetComponent<AudioSource>().PlayOneShot(driveOut);
    }

	public void LeadupAnimForNote(NoteData note)
	{
		GetObjectForNoteObjectType(note.tapObject).m_animator.SetTrigger("LeadUp");
	}

	public void PerfectAnimForNote(NoteData note)
	{
		GetObjectForNoteObjectType(note.tapObject).m_animator.SetTrigger("Perfect");
	}

	public void EarlyAnimForNote(NoteData note)
    {
        GetObjectForNoteObjectType(note.tapObject).m_animator.SetTrigger("Early");
    }

	public void LateAnimForNote(NoteData note)
    {
        GetObjectForNoteObjectType(note.tapObject).m_animator.SetTrigger("Late");
    }

	public void MissAnimForNote(NoteData note)
    {
        GetObjectForNoteObjectType(note.tapObject).m_animator.SetTrigger("Miss");
    }

	private TappableObject GetObjectForNoteObjectType(TapObjectType tapObjectType)
	{
        for (int i = 0; i < tappableObjects.Count; i++)
        {
            if (tapObjectType == tappableObjects[i].m_tapObjectType)
            	return tappableObjects[i];
        }

		Debug.LogError("Couldn't find an object in the scene with type " + tapObjectType.ToString() + "!!!");
		return null;
	}
}

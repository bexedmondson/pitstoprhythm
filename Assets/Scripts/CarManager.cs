using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour 
{
	public Animator m_carAnimator;

	public List<TappableObject> tappableObjects;
    
	public List<AudioClip> successWheelEffects;

	public AudioClip failWheelEffect;

	public AudioClip driveOutSound;

	private AudioSource audioSource;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}

	public void Reset()
	{
		m_carAnimator.SetTrigger("Reset");
	}

	public void CarEnter()
	{
		m_carAnimator.SetTrigger("Enter");
	}

	public void CarExit()
    {
        m_carAnimator.SetTrigger("Exit");

		audioSource.PlayOneShot(driveOutSound);
    }

	public void LeadupAnimForNote(NoteData note)
	{
		GetObjectForNoteObjectType(note.tapObject).m_animator.SetTrigger("LeadUp");
	}

	public void PerfectAnimForNote(NoteData note)
	{
		GetObjectForNoteObjectType(note.tapObject).m_animator.SetTrigger("Perfect");
		PlayRandomSuccessSound();
	}

	public void EarlyAnimForNote(NoteData note)
    {
        GetObjectForNoteObjectType(note.tapObject).m_animator.SetTrigger("Early");
		PlayRandomSuccessSound();
    }

	public void LateAnimForNote(NoteData note)
    {
        GetObjectForNoteObjectType(note.tapObject).m_animator.SetTrigger("Late");
		PlayRandomSuccessSound();
    }

	public void MissAnimForNote(NoteData note)
    {
        GetObjectForNoteObjectType(note.tapObject).m_animator.SetTrigger("Miss");
		audioSource.PlayOneShot(failWheelEffect);
    }

	private void PlayRandomSuccessSound()
	{
		audioSource.PlayOneShot(successWheelEffects[Random.Range(0, successWheelEffects.Count)]);
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

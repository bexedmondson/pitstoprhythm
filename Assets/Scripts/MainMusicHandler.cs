using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMusicHandler : MonoBehaviour 
{
	public AudioClip mainTrack;

	private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

	void Start () 
	{
		EventManager.StartListening(EventManager.RestartTrack, RestartMusic);
	}

	private void RestartMusic(TapObjectType type)
	{
		//ignore type, that's just because of the annoying way event handler is set up
		audioSource.Play();
	}
}

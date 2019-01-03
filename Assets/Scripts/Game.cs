using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour 
{   
	private static Game game;

	public static Game instance
    {
        get
        {
            if (!game)
            {
				game = FindObjectOfType(typeof(Game)) as Game;

                if (!game)
                {
					Debug.LogError("There needs to be one active Game script on a GameObject in your scene.");
                }
            }

            return game;
        }
    }


    public UIManager m_uiManager;

	public SongData m_songData;
    
	private Queue<NoteData> unplayedNotes;

	private List<NoteData> playingNotes;

    private float m_secondsSinceSongStart = 0f;

	private bool m_songStarted = false;
    
	private const float k_noteIntroLength = 0.5f;
	private const float k_noteMaxLatenessLength = 0.5f;

	void Start () 
	{
		m_uiManager.StartCoroutine("PlayCountdown");
	}

	void FixedUpdate () 
	{
		if (m_songStarted)
		{
			//check to see if we need to start playing any notes
			NoteData nextNote = unplayedNotes.Peek();
            
			if (nextNote.time < (m_secondsSinceSongStart + k_noteIntroLength))
			{
				nextNote = unplayedNotes.Dequeue();
				playingNotes.Add(nextNote);
			}

			//check to see if any playing notes are done
			foreach (NoteData note in playingNotes)
			{
				if (note.time < (m_secondsSinceSongStart - k_noteMaxLatenessLength))
				{
					playingNotes.Remove(note);
                    //late! animate here
				}
			}

			m_secondsSinceSongStart += Time.deltaTime;
		}
	}

	public void StartSong()
	{
		//load song
		foreach (NoteData noteData in m_songData.notes)
		{
			unplayedNotes.Enqueue(noteData);
		}
		
		m_songStarted = true;
	}

	private void EndGame()
	{
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
	}
}

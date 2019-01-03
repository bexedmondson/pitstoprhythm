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

	public CarManager m_carManager;

	public SongData m_songData;

	private Queue<NoteData> unplayedNotes = new Queue<NoteData> { };

	private List<NoteData> playingNotes = new List<NoteData> { };

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
			int numOfNotesToPlay = unplayedNotes.Count;
			int numOfNotesPlaying = playingNotes.Count;

			if (numOfNotesToPlay == 0 && numOfNotesPlaying == 0)
			{
				StartCoroutine("EndSong");
				m_songStarted = false;
			}
            
            //check to see if any playing notes are done
            if (playingNotes.Count != 0)
            {
				List<NoteData> notesToBeRemoved = new List<NoteData> { };

                foreach (NoteData note in playingNotes)
                {
                    if (note.time < (m_secondsSinceSongStart - k_noteMaxLatenessLength))
                    {
						notesToBeRemoved.Add(note);
                    }
                }

				foreach (NoteData note in notesToBeRemoved)
				{
					playingNotes.Remove(note);
					//miss! animate here
				}
            }

			//check to see if we need to start playing any notes
			if (numOfNotesToPlay != 0)
			{
				NoteData nextNote = unplayedNotes.Peek();

				if (nextNote.time <= (m_secondsSinceSongStart + k_noteIntroLength))
				{
					nextNote = unplayedNotes.Dequeue();
					playingNotes.Add(nextNote);

					m_carManager.AnimateForNote(nextNote);
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

		m_carManager.CarEnter();
	}

	IEnumerator EndSong()
	{
		yield return new WaitForSeconds(3);
		EndGame();
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

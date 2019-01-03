using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

	public ScoreManager m_scoreManager;

	public SongData m_songData;

	//these windows are cumulative - the total max time you can be late on a note is the perfect window time + the normal window time
	public float m_earlyWindow;

    public float m_perfectEarlyWindow;

	public float m_perfectLateWindow;

	public float m_lateWindow;

	private Queue<NoteData> m_unplayedNotes = new Queue<NoteData> { };

	private List<NoteData> m_playingNotes = new List<NoteData> { };
    
    private float m_secondsSinceSongStart = 0f;

	private bool m_songStarted = false;
    
	private const float k_noteIntroLength = 0.5f;

	private void Awake()
	{
		EventManager.StartListening(EventManager.TappableObjectTap, OnTappableObjectTap);
	}

	void Start () 
	{
		m_uiManager.StartCoroutine("PlayCountdown");
	}

	void FixedUpdate () 
	{
		if (m_songStarted)
		{
			int numOfNotesToPlay = m_unplayedNotes.Count;
			int numOfNotesPlaying = m_playingNotes.Count;

			if (numOfNotesToPlay == 0 && numOfNotesPlaying == 0)
			{
				StartCoroutine("EndSong");
				m_songStarted = false;
			}
            
            //check to see if any playing notes are done
            if (m_playingNotes.Count != 0)
            {
				List<NoteData> notesToBeRemoved = new List<NoteData> { };

                foreach (NoteData note in m_playingNotes)
                {
					if (note.time < (m_secondsSinceSongStart - (m_perfectLateWindow + m_lateWindow)))
                    {
						notesToBeRemoved.Add(note);
                    }
                }

				foreach (NoteData note in notesToBeRemoved)
				{
					m_playingNotes.Remove(note);
					m_carManager.MissAnimForNote(note);
				}
            }

			//check to see if we need to start playing any notes
			if (numOfNotesToPlay != 0)
			{
				NoteData nextNote = m_unplayedNotes.Peek();

				if (nextNote.time <= (m_secondsSinceSongStart + k_noteIntroLength))
				{
					nextNote = m_unplayedNotes.Dequeue();
					m_playingNotes.Add(nextNote);

					m_carManager.LeadupAnimForNote(nextNote);
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
			m_unplayedNotes.Enqueue(noteData);
		}
		
		m_songStarted = true;

		m_carManager.CarEnter();
	}
       
	private void OnTappableObjectTap(TapObjectType tapObjectType)
    {      
		NoteData note = m_playingNotes.FirstOrDefault(x => x.tapObject == tapObjectType);

		if (note == null)
			return;

		if (m_secondsSinceSongStart <= note.time + m_perfectLateWindow && m_secondsSinceSongStart >= note.time - m_perfectEarlyWindow)
		{
			m_carManager.PerfectAnimForNote(note);
			m_playingNotes.Remove(note);
		}
		else if (m_secondsSinceSongStart > note.time && m_secondsSinceSongStart <= note.time + m_lateWindow)
		{
			m_carManager.LateAnimForNote(note);
			m_playingNotes.Remove(note);
		}
		else if (m_secondsSinceSongStart < note.time && m_secondsSinceSongStart >= note.time - m_lateWindow)
		{
			m_carManager.EarlyAnimForNote(note);
			m_playingNotes.Remove(note);
		}      
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

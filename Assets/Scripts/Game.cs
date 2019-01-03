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

	public SongData m_songData;

	public float m_earlyWindow;

    public float m_perfectEarlyWindow;

	public float m_perfectLateWindow;

	public float m_lateWindow;

	private Queue<NoteData> unplayedNotes = new Queue<NoteData> { };

	private List<NoteData> playingNotes = new List<NoteData> { };

    private float m_secondsSinceSongStart = 0f;

	private bool m_songStarted = false;
    
	private const float k_noteIntroLength = 0.5f;
	private const float k_noteMaxLatenessLength = 0.5f;

	private void Awake()
	{
		EventManager.StartListening(EventManager.WheelBLTap, OnWheelBLTap);
		EventManager.StartListening(EventManager.WheelBRTap, OnWheelBRTap);
		EventManager.StartListening(EventManager.WheelTLTap, OnWheelTLTap);
		EventManager.StartListening(EventManager.WheelTRTap, OnWheelTRTap);
	}

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
    
    //this code duplication is gross but it's a game jam so i'm leaving it for now!
	private void OnWheelBLTap()
    {
		NoteData note = playingNotes.FirstOrDefault(x => x.tapObject == TapObjectType.WheelBottomLeft);

		if (note == null)
			return;
        

	}

	private void OnWheelBRTap()
    {
    }

	private void OnWheelTLTap()
    {
    }

	private void OnWheelTRTap()
    {
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

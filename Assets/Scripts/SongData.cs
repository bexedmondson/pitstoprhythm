using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Song")]
public class SongData : ScriptableObject 
{   
	public string songName;

    [SerializeField]
	public List<NoteData> notes;
}

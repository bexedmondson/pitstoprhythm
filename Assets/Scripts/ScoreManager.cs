using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	public int perfects;
	public int earlies;
	public int lates;
	public int misses;

	public int GetPercentageScore()
	{
		float scoreTotal = perfects + (earlies / 2) + (lates / 2);
		float totalNotes = Game.instance.m_songData.notes.Count;

		float score = scoreTotal / totalNotes * 100;

		int roundedScore = (int)score;

		return roundedScore;
	}

	public void ClearScore()
	{
		perfects = earlies = lates = misses = 0;
	}
}

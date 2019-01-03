using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour 
{
	public Image m_countdownBG;
	public Text m_countdownText;

	public GameObject m_scorePopup;
	public Text m_perfects;
	public Text m_earlies;
	public Text m_lates;
	public Text m_misses;
	public Text m_score;

	public IEnumerator PlayCountdown()
	{
		m_countdownBG.gameObject.SetActive(true);

		for (int i = 5; i > 0; i--)
		{
			m_countdownText.text = i.ToString();
            
			yield return new WaitForSeconds(0.95f); //the audio is weird so this is a hack to get the visuals a bit closer
		}

		m_countdownBG.gameObject.SetActive(false);

		Game.instance.StartSong();
	}

	public void ShowScore()
	{
		ScoreManager scoreManager = Game.instance.m_scoreManager;

		m_perfects.text = "Perfects: " + scoreManager.perfects.ToString();
		m_earlies.text = "Earlies: " + scoreManager.earlies.ToString();
		m_lates.text = "Lates: " + scoreManager.lates.ToString();
		m_misses.text = "Misses: " + scoreManager.misses.ToString();

		int score = scoreManager.GetPercentageScore();

		string scoreString = score.ToString() + "%";

		if (score >= 80)
			scoreString += " :D";
		else if (score >= 60)
			scoreString += " :)";
		else if (score >= 40)
			scoreString += " :|";
		else if (score >= 20)
			scoreString += " :/";
		else
			scoreString += " >:( did you even try?";

		m_score.text = scoreString;

		m_scorePopup.SetActive(true);
	}

	public void OnEndGameButton()
	{
		m_scorePopup.SetActive(false);
		Game.instance.EndGame();
	}
}

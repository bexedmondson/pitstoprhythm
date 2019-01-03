﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour 
{
	public Image m_countdownBG;
	public Text m_countdownText;

	public IEnumerator PlayCountdown()
	{
		m_countdownBG.gameObject.SetActive(true);

		for (int i = 5; i > 0; i--)
		{
			m_countdownText.text = i.ToString();
            
			yield return new WaitForSeconds(1);
		}

		m_countdownBG.gameObject.SetActive(false);

		Game.instance.StartSong();
	}
}

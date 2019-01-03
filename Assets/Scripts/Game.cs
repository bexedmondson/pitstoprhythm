﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour 
{
	public UIManager m_uiManager;


	public float m_audioLength = 10f;

	private float m_timeSinceStart = 0f;


	void Start () 
	{
		
	}

	void FixedUpdate () 
	{      
		if (m_timeSinceStart < m_audioLength)
		{
			//do stuff
			m_timeSinceStart += Time.deltaTime;
		}
		else
		{
			EndGame();
		}
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

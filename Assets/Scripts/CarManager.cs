﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour 
{
	public Animator m_carAnimator;

	public List<TappableObject> tappableObjects;

	private void Awake()
    {
        EventManager.StartListening(EventManager.SwipeDown, CarEnter);
    }

	void CarEnter()
	{
		m_carAnimator.SetTrigger("Enter");
	}
}
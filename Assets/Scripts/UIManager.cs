using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour 
{
	public Image m_downArrow;

	private void Awake()
	{
		EventManager.StartListening(EventManager.SwipeDown, SwipeDownDone);
		Debug.Log("subscribing!");
	}

	void Start () 
	{
		m_downArrow.gameObject.SetActive(true);
	}

    
	void SwipeDownDone()
	{
		m_downArrow.gameObject.SetActive(false);
	}
}

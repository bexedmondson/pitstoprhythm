using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAnimationEventHandler : MonoBehaviour 
{   
    public void CarHasExited()
    {
        Game.instance.StartCoroutine("EndSong");
    }
}

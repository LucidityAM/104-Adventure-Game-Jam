using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{

    public bool Started;

    public void Start()
    {
        if (MoveNumber.musicStarted == false)
        {
            DontDestroyOnLoad(this.gameObject);
            MoveNumber.musicStarted = true;
            Started = true;
        }

        if(Started == false)
        {
            gameObject.GetComponent<AudioSource>().enabled = false;
        }
    }
}

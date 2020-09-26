using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue
{
    [TextArea(3, 10)]
    //strings for names and sentences, options and everything pretty much
    public string[] sentences;
    public Sprite[] names;

    //different expression sprites and backgrounds
    public string[] sprites;
    public Sprite background;

    //checking if its gonna send you to another scene and stuff
    public bool sceneTransition;
    public string sceneName;
    public bool prompt;
    public Question question;
}

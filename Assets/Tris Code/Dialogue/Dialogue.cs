using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue
{
    [TextArea(3, 10)]
    //strings for names and sentences
    public string[] sentences;
    public string[] names;

    //different expression sprites and backgrounds
    public Sprite[] sprites;
    public Sprite background;

    //checking if its gonna send you to another scene and stuff
    public bool sceneTransition;
}

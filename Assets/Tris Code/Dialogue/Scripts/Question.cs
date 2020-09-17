using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Question
{
    //three different options. these will also be the way to determine if you said the right answer or not
    public string correctAnswer;
    public string option1;
    public string option2;
    public string option3;

    public Dialogue path1;
    public Dialogue path2;
    public Dialogue path3;
}

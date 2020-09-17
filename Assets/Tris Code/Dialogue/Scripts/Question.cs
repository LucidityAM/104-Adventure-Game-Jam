using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question
{
    [TextArea(3, 10)]
    public int correctAnswer;
    public string option1;
    public string option2;
    public string option3;

    public Dialogue path1;
    public Dialogue path2;
    public Dialogue path3;
}

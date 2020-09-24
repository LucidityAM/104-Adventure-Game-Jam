using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuestionManager : MonoBehaviour
{
    #region Variables

    //So you know. In this class if i refer to variable with a number. the number corresponds to the dialogue choice. 1 being top, 2 being middle, 3 being bottom.
    public Text text1;
    public Text text2;
    public Text text3;

    //Grabbing game objects and varibales of the buttons
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    private Button button1b;
    private Button button2b;
    private Button button3b;
    private Animator button1Anim;
    private Animator button2Anim;
    private Animator button3Anim;

    //Grabbing GameObjects and variables of the epic slide in option sprite
    public GameObject promptSprite;
    public GameObject promptEmmie;
    public GameObject promptLine;
    private Animator promptSpriteAnim;
    private Animator promptEmmieAnim;
    private Animator promptLineAnim;

    //Shader square to make everyting other than your options dark
    public GameObject shade;
    private Animator shadeAnim;


    //private variables that will be taken from Question
    private string option1;
    private string option2;
    private string option3;
    private Dialogue path1;
    private Dialogue path2;
    private Dialogue path3;
    private string correctAnswer;

    public static QuestionManager Instance;

    #endregion
    void Start()
    {
        //resetting privates from question
        option1 = null;
        option2 = null;
        option3 = null;
        path1 = null;
        path2 = null;
        path3 = null;

        //resetting text in options
        text1.text = "";
        text2.text = "";
        text3.text = "";

        //button buttons
        button1b = button1.GetComponent<Button>();
        button2b = button2.GetComponent<Button>();
        button3b = button3.GetComponent<Button>();
        //button anims
        button1Anim = button1.GetComponent<Animator>();
        button2Anim = button2.GetComponent<Animator>();
        button3Anim = button3.GetComponent<Animator>();
        //promptSprite variables
        promptSpriteAnim = promptSprite.GetComponent<Animator>();
        promptEmmieAnim = promptEmmie.GetComponent<Animator>();
        promptLineAnim = promptLine.GetComponent<Animator>();
        shadeAnim = shade.GetComponent<Animator>();

        //turning off all variables that need to be turned off
        //buttons
        button1.SetActive(false);
        button2.SetActive(false);
        button3.SetActive(false);
        //prompt and shade
        promptSprite.SetActive(false);
        promptEmmie.SetActive(false);
        promptLine.SetActive(false);
        shade.SetActive(false);


    }

    public IEnumerator StartQuestion(Question question)
    {
        //setting privates from Question
        option1 = question.option1;
        option2 = question.option2;
        option3 = question.option3;
        path1 = question.path1;
        path2 = question.path2;
        path3 = question.path3;
        correctAnswer = question.correctAnswer;
        //turning all gameobjects on
        shade.SetActive(true);
        promptSprite.SetActive(true);
        promptLine.SetActive(true);
        promptEmmie.SetActive(true);

        //turn all animators to open yield return new waitforseconds are used for stylistic timing
        shadeAnim.SetBool("isOpen", true);
        yield return new WaitForSeconds(0.2f);
        promptSprite.SetActive(true);
        promptSpriteAnim.SetBool("isOpen", true);
        promptLineAnim.SetBool("isOpen", true);
        yield return new WaitForSeconds(0.4f);
        promptEmmieAnim.SetBool("isOpen", true);
        StartCoroutine("DisplayOptions");
    }

    public IEnumerator DisplayOptions()
    {
        Debug.Log("true");
        button1.SetActive(true);
        button2.SetActive(true);
        button3.SetActive(true);
        text1.text = option1;
        text2.text = option2;
        text3.text = option3;
        button1Anim.SetBool("isOpen", true);
        button2Anim.SetBool("isOpen", true);
        button3Anim.SetBool("isOpen", true);

        yield return new WaitForSeconds(0.3f);
    }

    //public void DisplayNextEvent()
    //{
    //    if (EventSystem.current.currentSelectedGameObject.name.Contains(correctAnswer))
    //    {

    //    }
    //}

    public void DisplayPath1()
    {
        CloseQuestions();
        StartCoroutine(FindObjectOfType<DialogueManager>().StartDialogue(path1));
    }

    public void DisplayPath2()
    {
        CloseQuestions();
        StartCoroutine(FindObjectOfType<DialogueManager>().StartDialogue(path2));
    }

    public void DisplayPath3()
    {
        CloseQuestions();
        StartCoroutine(FindObjectOfType<DialogueManager>().StartDialogue(path3));
    }

    public void CloseQuestions()
    {
        button1Anim.SetBool("isOpen", false);
        button2Anim.SetBool("isOpen", false);
        button3Anim.SetBool("isOpen", false);
        promptSpriteAnim.SetBool("isOpen", false);
        promptLineAnim.SetBool("isOpen", false);
        promptEmmieAnim.SetBool("isOpen", false);
        shadeAnim.SetBool("isOpen", false);

        button1.SetActive(false);
        button2.SetActive(false);
        button3.SetActive(false);
        promptSprite.SetActive(false);
        promptEmmie.SetActive(false);
        promptLine.SetActive(false);
        shade.SetActive(false);

    }
}

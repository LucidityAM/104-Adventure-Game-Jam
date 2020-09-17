using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private Animator promptSpriteAnim;

    //Shader square to make everyting other than your options dark
    public GameObject shade;
    private Animator shadeAnim;

    public static QuestionManager Instance;

    #endregion
    void Start()
    {
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


        //turning off all variables that need to be turned off
        //buttons
        button1.SetActive(false);
        button2.SetActive(false);
        button3.SetActive(false);
        //prompt and shade
        promptSprite.SetActive(false);
        shade.SetActive(false);


    }

    public IEnumerator StartDialogue()
    {
        //turning all gameobjects on
        shade.SetActive(true);
        promptSprite.SetActive(true);
        button1.SetActive(true);
        button2.SetActive(true);
        button3.SetActive(true);

        //turn all animators to open yield return new waitforseconds are used for stylistic timing
        shadeAnim.SetBool("isOpen", true);
        yield return new WaitForSeconds(0.2f);
        promptSprite.SetActive(true);
        promptSpriteAnim.SetBool("isOpen", true);
        yield return new WaitForSeconds(0.4f);
        button1.SetActive(true);
        button2.SetActive(true);
        button3.SetActive(true);
        button1Anim.SetBool("isOpen", true);
        yield return new WaitForSeconds(0.3f);
        button2Anim.SetBool("isOpen", true);
        yield return new WaitForSeconds(0.3f);
        button3Anim.SetBool("isOpen", true);
    }
}

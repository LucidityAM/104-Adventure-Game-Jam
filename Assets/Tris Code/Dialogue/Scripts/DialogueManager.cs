using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    #region variables
    //Text stuff: Names and Body Text
    public Text dialogueText;

    //Visual Stuff: Sprites, Backgrounds, Box for text
    public GameObject nameLeft;
    public GameObject nameRight;
    public GameObject textBox;
    public GameObject characterLeft;
    public GameObject characterRight;
    public GameObject BG;

    //private variables that i dont want to be flooded in unity. Animators and Images for the Objects above
    private Animator nameLeftAnim;
    private Animator nameRightAnim;
    private Animator textBoxAnim;
    private Animator characterLeftSprite;
    private Animator characterRightSprite;
    private Image nameLeftSprite;
    private Image nameRightSprite;
    private Image BGSprite;

    //Queue for names and sentances.
    private Queue<string> sentences;
    private Queue<Sprite> names;
    private Queue<Sprite> sprites;

    //Player access for preventing movement



    //Making instance of the DialogueManager, so it can be reapplied
    public static DialogueManager Instance;

    //Bools
    private bool isActive; //Checks if dialogue is on or not
    private bool endText; //if on will keep the text off constantly
    private bool sceneTransition; //if on will go to a marked scene once the dialogue is over
    private bool onLeftChar; //checking if the current sentence is the left character speaking
    private bool onRightChar; //checking if the current sentence is the right character speaking 

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //Giving value to all private Animators, visual components, or anything thats not already given values that need to be here
        #region getting components
        nameLeftAnim = nameLeft.GetComponent<Animator>();
        nameRightAnim = nameRight.GetComponent<Animator>();
        textBoxAnim = textBox.GetComponent<Animator>();
        characterLeftSprite = characterLeft.GetComponent<Animator>();
        characterRightSprite = characterRight.GetComponent<Animator>();
        nameLeftSprite = nameLeft.GetComponent<Image>();
        nameRightSprite = nameRight.GetComponent<Image>();
        BGSprite = BG.GetComponent<Image>();
        #endregion 

        //Turning bools and counts to default values
        isActive = false;
        sceneTransition = false;
        onLeftChar = false;
        onRightChar = false;

        //Resetting Queues
        sentences = new Queue<string>();
        names = new Queue<Sprite>();
        sprites = new Queue<Sprite>();

        //Turning off everything
        #region turning off variables
        //characterLeftSprite.SetBool("isOpen", false);
        //characterRightSprite.SetBool("isOpen", false);
        //nameLeftAnim.SetBool("isOpen", false);
        //nameRightAnim.SetBool("isOpen", false);
        //textBoxAnim.SetBool("isOpen", false);

        BG.SetActive(false);
        textBox.SetActive(false);
        characterLeft.SetActive(false);
        characterRight.SetActive(false);
        nameLeft.SetActive(false);
        nameRight.SetActive(false);

        #endregion

    }

    public IEnumerator StartDialogue(Dialogue dialogue)
    {
        //turning off all bool variables so they can be set later
        endText = false; //Since Dialogue is Starting, end text does not need to be on
        isActive = false;
        sceneTransition = false;
        onLeftChar = false;
        onRightChar = false;


        //Setting all the bools that need to be set equal to their counterpart in dialogue
        sceneTransition = dialogue.sceneTransition;

        //Player stopping happens HERE


        //setting up queues from dialogue into queue for the whole script
        //NAMES
        names.Clear();
        foreach (Sprite name in dialogue.names)
        {
            names.Enqueue(name);
        }
        //SPRITES
        foreach (Sprite sprite in dialogue.sprites)
        {
            sprites.Enqueue(sprite);
        }
        //SENTENCES
        sentences.Clear();
        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        //Enabling all visual components
        #region enabling components
        BG.SetActive(true);
        textBox.SetActive(true);
        characterLeft.SetActive(true);
        characterRight.SetActive(true);
        nameLeft.SetActive(true);
        nameRight.SetActive(true);
        #endregion


        textBoxAnim.SetBool("isOpen", true);
        yield return new WaitForSeconds(0.4f);
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        //Ends Dialogue if count of sentances reaches 0
        if(sentences.Count <= 0)
        {
            StartCoroutine("EndDialogue");
            return;
        }

        //Making local variables for sentences and names
        string sentence = sentences.Dequeue();
        Sprite name = names.Dequeue();

        StopAllCoroutines();
        StartCoroutine(DisplayNextName(name));
        StartCoroutine(TypeSentence(sentence));

    }

    public IEnumerator DisplayNextName(Sprite name)
    {
        
        Sprite prevNameLeft = nameLeftSprite.sprite;
        Sprite prevNameRight = nameRightSprite.sprite;

        yield return new WaitForSeconds(0.1f);
        
        if (prevNameLeft == null)
        {
            nameLeftSprite.sprite = name;
            nameLeftAnim.SetBool("isOpen", true);
            onLeftChar = true;
            onRightChar = false;
            //enabling character anim
            characterLeftSprite.SetBool("isOpen", true);
            

        } else if(name == prevNameLeft && prevNameLeft != null)
        {
            nameLeftSprite.sprite = name;
            nameLeftAnim.SetBool("isOpen", true);
            onLeftChar = true;
            onRightChar = false;

            //enabling character anim
            characterLeftSprite.SetBool("isOpen", true);
        }
        else
        {
            nameRightAnim.SetBool("isOpen", true);
            nameRightSprite.sprite = name;
            onLeftChar = false;
            onRightChar = true;

            //enabling character anim
            characterRightSprite.SetBool("isOpen", true);
        }

    }

    public IEnumerator TypeSentence(string sentence)
    {


        dialogueText.text = "";
        //typing each word letter for letter
        foreach (char letter in sentence.ToCharArray())
        {
            if (endText == true)
            {

            }
            dialogueText.text += letter;
            yield return null;
        }

    }

    public IEnumerator EndDialogue()
    {
        Debug.Log("ENDED");
        endText = true;
        //Enable Player controls

        //Turns all Animators to off
        yield return new WaitForSeconds(0.4f);
        characterLeftSprite.SetBool("isOpen", false);
        characterRightSprite.SetBool("isOpen", false);
        nameLeftAnim.SetBool("isOpen", false);
        nameRightAnim.SetBool("isOpen", false);
        textBoxAnim.SetBool("isOpen", false);

        yield return new WaitForSeconds(3f);
        //Diables all GameObjects
        BG.SetActive(false);
        textBox.SetActive(false);
        characterLeft.SetActive(false);
        characterRight.SetActive(false);
        nameLeft.SetActive(false);
        nameRight.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

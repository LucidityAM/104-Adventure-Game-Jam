using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditorInternal;

public class DialogueManager : MonoBehaviour
{
    #region variables
    //Text stuff: Names and Body Text
    public Text dialogueText;

    //Visual Stuff: Sprites, Backgrounds, Box for text
    public GameObject nameLeft;
    public GameObject nameRight;
    public GameObject textBox;
    public GameObject chapterBox;
    public GameObject characterLeft;
    public GameObject characterRight;
    public GameObject BG;
    public GameObject BlueLine;
    public GameObject RedLine;

    //private variables that i dont want to be flooded in unity. Animators and Images for the Objects above
    private Animator nameLeftAnim;
    private Animator nameRightAnim;
    private Animator textBoxAnim;
    private Animator chapterBoxAnim;
    private Animator characterLeftSprite;
    private Animator characterRightSprite;
    private Animator BlueLineAnim;
    private Animator RedLineAnim;
    private Image nameLeftSprite;
    private Image nameRightSprite;
    private Image BGSprite;

    //Queue for names and sentances.
    private Queue<string> sentences;
    private Queue<Sprite> names;
    private Queue<string> sprites;

    //Player access for preventing movement


    //private question to trigger a question at the end of dialogue
    private Question question;

    //Entire Question Object
    public GameObject questionObject;

    //Making instance of the DialogueManager, so it can be reapplied
    public static DialogueManager Instance;

    //Bools
    private bool isActive; //Checks if dialogue is on or not
    private bool endText; //if on will keep the text off constantly
    private bool sceneTransition; //if on will go to a marked scene once the dialogue is over
    private string sceneName;
    private bool onLeftChar; //checking if the current sentence is the left character speaking
    private bool onRightChar; //checking if the current sentence is the right character speaking 
    private bool prompt; //if prompt is on. ending the dialogue will lead to a question

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //Giving value to all private Animators, visual components, or anything thats not already given values that need to be here
        #region getting components
        nameLeftAnim = nameLeft.GetComponent<Animator>();
        nameRightAnim = nameRight.GetComponent<Animator>();
        textBoxAnim = textBox.GetComponent<Animator>();
        chapterBoxAnim = chapterBox.GetComponent<Animator>();
        characterLeftSprite = characterLeft.GetComponent<Animator>();
        characterRightSprite = characterRight.GetComponent<Animator>();
        nameLeftSprite = nameLeft.GetComponent<Image>();
        nameRightSprite = nameRight.GetComponent<Image>();
        BGSprite = BG.GetComponent<Image>();
        BlueLineAnim = BlueLine.GetComponent<Animator>();
        RedLineAnim = RedLine.GetComponent<Animator>();
        #endregion

        //Turning bools and counts to default values
        isActive = false;
        sceneTransition = false;
        onLeftChar = false;
        onRightChar = false;
        prompt = false;

        //Resetting question
        question = null;

        //Resetting Queues
        sentences = new Queue<string>();
        names = new Queue<Sprite>();
        sprites = new Queue<string>();

        //Turning off everything
        #region turning off variables
        BG.SetActive(false);
        textBox.SetActive(false);
        chapterBox.SetActive(false);
        characterLeft.SetActive(false);
        characterRight.SetActive(false);
        nameLeft.SetActive(false);
        nameRight.SetActive(false);
        BlueLine.SetActive(false);
        RedLine.SetActive(false);
        #endregion

    }

    public IEnumerator StartDialogue(Dialogue dialogue)
    {
        #region spammy disabling
        //turning off animators here because it works better for some reason
        characterLeftSprite.SetBool("isOpen", false);
        characterRightSprite.SetBool("isOpen", false);
        nameLeftAnim.SetBool("isOpen", false);
        nameRightAnim.SetBool("isOpen", false);
        BlueLineAnim.SetBool("isOpen", false);
        RedLineAnim.SetBool("isOpen", false);

        BG.SetActive(false);
        textBox.SetActive(false);
        chapterBox.SetActive(false);
        characterLeft.SetActive(false);
        characterRight.SetActive(false);
        nameLeft.SetActive(false);
        nameRight.SetActive(false);
        #endregion

        //turning off all bool variables so they can be set later
        endText = false; //Since Dialogue is Starting, end text does not need to be on
        isActive = false;
        sceneTransition = false;
        onLeftChar = false;
        onRightChar = false;
        BGSprite = null;
        nameLeftSprite.sprite = null;
        nameRightSprite.sprite = null;

        //Setting all the bools that need to be set equal to their counterpart in dialogue
        sceneTransition = dialogue.sceneTransition;
        sceneName = dialogue.sceneName;
        prompt = dialogue.prompt;
        question = dialogue.question;

        //Player stopping happens HERE


        //setting up queues from dialogue into queue for the whole script
        //NAMES
        names.Clear();
        foreach (Sprite name in dialogue.names)
        {
            names.Enqueue(name);
        }
        //SPRITES
        sprites.Clear();
        foreach (string sprite in dialogue.sprites)
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
        chapterBox.SetActive(true);
        characterLeft.SetActive(true);
        characterRight.SetActive(true);
        nameLeft.SetActive(true);
        nameRight.SetActive(true);
        BlueLine.SetActive(true);
        RedLine.SetActive(true);
        #endregion

        dialogueText.text = "";
        textBoxAnim.SetBool("isOpen", true);
        yield return new WaitForSeconds(0.3f);
        BlueLineAnim.SetBool("isOpen", true);
        yield return new WaitForSeconds(0.1f);
        RedLineAnim.SetBool("isOpen", true);
        yield return new WaitForSeconds(0.4f);
        chapterBoxAnim.SetBool("isOpen", true);
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        BlueLineAnim.SetBool("isOpen", true);
        RedLineAnim.SetBool("isOpen", true);
        //Ends Dialogue if count of sentances reaches 0
        if (sentences.Count <= 0)
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
        
        if (prevNameLeft == null)
        {
            nameLeftAnim.SetBool("isOpen", true);
            nameLeftSprite.color = new Color(1f, 1f, 1f, 1f);
            nameLeftSprite.sprite = name;
            nameRightSprite.color = new Color(.5f, .5f, .5f, 1f);
            onLeftChar = true;
            onRightChar = false;
            StartCoroutine("VisualizeSpeaker");
        } else if(name == prevNameLeft && prevNameLeft != null)
        {
            nameLeftAnim.SetBool("isOpen", true);
            nameLeftSprite.color = new Color(1f, 1f, 1f, 1f);
            nameLeftSprite.sprite = name;
            nameRightSprite.color = new Color(.5f, .5f, .5f, 1f);
            onLeftChar = true;
            onRightChar = false;
            StartCoroutine("VisualizeSpeaker");
        }
        else if(name == prevNameRight)
        {
            nameRightAnim.SetBool("isOpen", true);
            nameRightSprite.color = new Color(1f, 1f, 1f, 1f);
            nameRightSprite.sprite = name;
            nameLeftSprite.color = new Color(.5f, .5f, .5f, 1f);
            onLeftChar = false;
            onRightChar = true;
            StartCoroutine("VisualizeSpeaker");
        }
        else
        {
            nameRightAnim.SetBool("isOpen", false);
            yield return new WaitForSeconds(0.3f);
            nameRightAnim.SetBool("isOpen", true);
            nameRightSprite.color = new Color(1f, 1f, 1f, 1f);
            nameRightSprite.sprite = name;
            nameLeftSprite.color = new Color(.5f, .5f, .5f, 1f);
            onLeftChar = false;
            onRightChar = true;
            StartCoroutine("VisualizeSpeaker");
        }

    }

    public IEnumerator VisualizeSpeaker()
    {
        if (onLeftChar)
        {
            characterRightSprite.enabled = false;
            characterLeftSprite.enabled = true;
            characterLeftSprite.SetBool("isOpen", false);
            yield return new WaitForSeconds(0.4f);
            characterLeftSprite.Play(sprites.Dequeue());
            characterLeftSprite.SetBool("isOpen", true);
            yield return new WaitForSeconds(0.3f);
            BlueLineAnim.Play("BlueLineActive");
            RedLineAnim.Play("RedLineInactive");
            BlueLineAnim.SetBool("isOpen", true);
            RedLineAnim.SetBool("isOpen", true);

        } else if (onRightChar)
        {
            characterLeftSprite.enabled = false;
            characterRightSprite.enabled = true;
            characterRightSprite.SetBool("isOpen", false);
            yield return new WaitForSeconds(0.4f);
            characterRightSprite.Play(sprites.Dequeue());
            characterRightSprite.SetBool("isOpen", true);
            yield return new WaitForSeconds(0.3f);
            RedLineAnim.Play("RedLineActive");
            BlueLineAnim.Play("BlueLineInactive");
            BlueLineAnim.SetBool("isOpen", true);
            RedLineAnim.SetBool("isOpen", true);
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
        endText = true;
        //Enable Player controls


        //checks if theres a prompt, if theres a prompt, you go there instead of ending
        if (prompt)
        {
            characterLeftSprite.SetBool("isOpen", false);
            characterRightSprite.SetBool("isOpen", false);
            StartCoroutine(FindObjectOfType<QuestionManager>().StartQuestion(question));
        }
        else
        {
            #region gross
            //Turns all Animators to off
            yield return new WaitForSeconds(0.4f);
            characterLeftSprite.SetBool("isOpen", false);
            characterRightSprite.SetBool("isOpen", false);
            yield return new WaitForSeconds(0.3f);
            nameLeftAnim.SetBool("isOpen", false);
            nameRightAnim.SetBool("isOpen", false);
            yield return new WaitForSeconds(0.3f);
            textBoxAnim.SetBool("isOpen", false);
            yield return new WaitForSeconds(0.1f);
            chapterBoxAnim.SetBool("isOpen", false);
            BlueLineAnim.SetBool("isOpen", false);
            //yield return new WaitForSeconds(0.05f);
            RedLineAnim.SetBool("isOpen", false);

            yield return new WaitForSeconds(2f);
            //Diables all GameObjects
            BG.SetActive(false);
            textBox.SetActive(false);
            chapterBox.SetActive(false);
            characterLeft.SetActive(false);
            characterRight.SetActive(false);
            nameLeft.SetActive(false);
            nameRight.SetActive(false);
            BlueLine.SetActive(false);
            RedLine.SetActive(false);
            #endregion

            if(sceneTransition == true)
            {
                FindObjectOfType<LoadingScene>().LoadSceneLevel(sceneName);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}

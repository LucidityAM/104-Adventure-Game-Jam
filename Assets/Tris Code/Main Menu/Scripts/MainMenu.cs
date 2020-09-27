using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuBG;
    private Animator mainMenuBGAnim;

    public GameObject resumeButton;
    public GameObject quitButton;

    private bool isOpened;

    public PlayerController Player;
    // Start is called before the first frame update
    void Start()
    {
        mainMenuBGAnim = mainMenuBG.GetComponent<Animator>();

        mainMenuBG.SetActive(false);
        resumeButton.SetActive(false);
        quitButton.SetActive(false);
        mainMenuBGAnim.SetBool("isOpen", false);
    }

    public void PingMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        { 
            if (!isOpened) { isOpened = true; StartCoroutine("OpenMenu"); }
            else { isOpened = false; StartCoroutine("CloseMenu"); }
        }
    }

    public IEnumerator OpenMenu()
    {
        //turnign off player shit
        Player.enabled = false;
        
        mainMenuBG.SetActive(true);
        mainMenuBGAnim.SetBool("isOpen", true);
        yield return new WaitForSeconds(0.3f);
        resumeButton.SetActive(true);
        quitButton.SetActive(true);
        yield return new WaitForSeconds(0.5f);
    }

    public IEnumerator CloseMenu()
    {
        Player.enabled = true;
        resumeButton.SetActive(false);
        quitButton.SetActive(false);
        mainMenuBGAnim.SetBool("isOpen", false);
        yield return new WaitForSeconds(0.3f);
        mainMenuBG.SetActive(false);
        yield return new WaitForSeconds(0.5f);
    }

    public void StartIEnumerator(string name)
    {
        StartCoroutine(name);
    }

    // Update is called once per frame
    void Update()
    {
        PingMenu();
    }
}

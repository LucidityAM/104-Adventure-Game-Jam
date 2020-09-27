using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class ItemPopup : MonoBehaviour
{
    public GameObject popup;

    public Sprite imageToAppear;

    public PlayerController playerController;
    public MainMenu menu;

    private Animator anim;

    private bool isOpen;
    public void Start()
    {
        anim = popup.GetComponent<Animator>();
        popup.SetActive(false);
        //isOpen = false;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isOpen == false)
            {
                menu.enabled = false;
                popup.SetActive(true);
                anim.SetBool("FadeOut", true);
                popup.GetComponent<Image>().sprite = imageToAppear;
                playerController.enabled = false;
                isOpen = true;
            }
        }
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isOpen)
            {
                anim.SetBool("FadeOut", false);
                StartCoroutine(Enable());
            }
        }
    }

    public IEnumerator Enable()
    {
        yield return new WaitForSeconds(1.5f);
        menu.enabled = true;
        popup.SetActive(false);
        playerController.enabled = true;
        isOpen = false;
    }

}

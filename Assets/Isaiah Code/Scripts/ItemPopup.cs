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

    private Animator anim;

    public void Start()
    {
        anim = popup.GetComponent<Animator>();
        popup.SetActive(false);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            popup.SetActive(true);

            popup.GetComponent<Image>().sprite = imageToAppear;

            playerController.enabled = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            anim.SetTrigger("FadeOut");
            StartCoroutine(Enable());
        }
    }

    public IEnumerator Enable()
    {
        yield return new WaitForSeconds(1.5f);

        popup.SetActive(false);
        playerController.enabled = true;
    }

}

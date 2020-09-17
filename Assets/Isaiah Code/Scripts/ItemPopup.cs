using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class ItemPopup : MonoBehaviour
{
    public GameObject popup;

    public Canvas canvas;

    public PlayerController playerController;

    public Object spawnedImage;

    public Animator anim;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            spawnedImage = Instantiate(popup, canvas.transform);
            playerController.enabled = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Destroy(spawnedImage);
            playerController.enabled = true;
        }
    }

}

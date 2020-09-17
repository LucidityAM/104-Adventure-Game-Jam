using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomSwitch : MonoBehaviour
{
    public string roomName;

    public Animator anim;

    public Rigidbody2D player;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerController>().enabled = false;
            FadeToLevel();
        }
    }
    public void FadeToLevel()
    {
        anim.SetTrigger("FadeOut");
    }
}

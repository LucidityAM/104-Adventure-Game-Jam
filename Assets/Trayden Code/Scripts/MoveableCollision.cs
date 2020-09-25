using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableCollision : MonoBehaviour
{
    public GameObject Player;
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.gameObject.CompareTag("Wall"))
        {
            Player.GetComponent<PlayerController>().StartCoroutine("MoveCooldownHorizontal");
        }
    }
}

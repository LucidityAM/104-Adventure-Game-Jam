using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableCollision : MonoBehaviour
{
    public GameObject Object;
    public GameObject Player;

    public void Start()
    {
        Object = this.gameObject;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Moveable") || collision.gameObject.CompareTag("Stationary"))
        {
            var relativePosition = transform.InverseTransformPoint(collision.transform.position);

            if (relativePosition.y > 0)
            {
                Object.transform.position += new Vector3(0f, .72f, 0f);
            }
            else if (Player.GetComponent<PlayerController>().vertPosition == 0)
            {
                if (relativePosition.x < 0)
                {
                    Object.transform.position += new Vector3(-.72f, 0f, 0f);
                }
                else if (relativePosition.x > 0)
                {
                    Object.transform.position += new Vector3(.72f, 0f, 0f);
                }
            }
            else if (relativePosition.y < 0)
            {
               Object.transform.position += new Vector3(0f, -.72f, 0f);
            }

            if (collision.gameObject.CompareTag("Wall"))
            {
                Player.GetComponent<PlayerController>().StartCoroutine("MoveCooldownHorizontal");

            }
        }
    }
}

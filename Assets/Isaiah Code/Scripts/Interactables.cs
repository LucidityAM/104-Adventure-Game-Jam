using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var relativePosition = transform.InverseTransformPoint(collision.transform.position);

            if (relativePosition.y > 0)
            {
                collision.transform.position += new Vector3(0f, .72f, 0f);
            }
            else if (collision.gameObject.GetComponent<PlayerController>().vertPosition == 0)
            {
                if (relativePosition.x < 0)
                {
                    collision.transform.position += new Vector3(-.72f, 0f, 0f);
                }
                else if (relativePosition.x > 0)
                {
                    collision.transform.position += new Vector3(.72f, 0f, 0f);
                }
            }
            else if (relativePosition.y < 0)
            {
                collision.transform.position += new Vector3(0f, -.72f, 0f);
            }
        }
    }
}

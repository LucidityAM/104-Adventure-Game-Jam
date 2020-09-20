using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint;
    public LayerMask whatStopsMovement;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                if(!Physics2D.OverlapCircle(movePoint.position + new Vector3((Input.GetAxisRaw("Horizontal") * .48f), 0f, 0f), .2f, whatStopsMovement))
                {
                    movePoint.position += new Vector3((Input.GetAxisRaw("Horizontal") *.48f), 0f, 0f);
                }
            } else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, (Input.GetAxisRaw("Vertical") * .48f), 0f), .2f, whatStopsMovement))
                {
                    movePoint.position += new Vector3(0f, (Input.GetAxisRaw("Vertical") * .48f), 0f);
                }
            }

            anim.SetBool("moving", false);
        }else
        {
            anim.SetBool("moving", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Moveable"))
        {
            var relativePosition = transform.InverseTransformPoint(collision.transform.position);

            if (relativePosition.y > 0)
            {
                Debug.Log("up");
                collision.transform.position += new Vector3(0f, .48f, 0f);
            }
            else if (relativePosition.x < 0)
            {
                Debug.Log("left");
                collision.transform.position += new Vector3(-.48f, 0f, 0f);
            }
            else if (relativePosition.x > 0)
            {
                Debug.Log("right");
                collision.transform.position += new Vector3(.48f, 0f, 0f);
            }
            else if (relativePosition.y < 0)
            {
                Debug.Log("down");
                collision.transform.position += new Vector3(0f, -.48f, 0f);
            }
           
        }
    }
}

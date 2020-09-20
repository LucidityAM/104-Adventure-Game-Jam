using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //Entire Dialogue and Question Game Objects, probably adding the overlay too.
    public GameObject dialogueUI;
    public GameObject questionUI;


    // Start is called before the first frame update
    void Start()
    {
        dialogueUI.SetActive(true);
        questionUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

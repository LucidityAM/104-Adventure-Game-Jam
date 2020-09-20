using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCooldown : MonoBehaviour
{
    Button button;
    public float cooldown;
    // Start is called before the first frame update

    private void Start()
    {
        button.interactable = true;
    }
    void Awake()
    {

        button = GetComponent<Button>();

        if(button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    void OnButtonClick()
    {
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        button.interactable = false;

        yield return new WaitForSeconds(cooldown);

        button.interactable = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

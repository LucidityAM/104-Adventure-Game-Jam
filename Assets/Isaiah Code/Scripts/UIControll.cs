using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIControll : MonoBehaviour
{
    public Text moveCount;
    public Image redScreen;
    private bool isDead;

    void Update()
    {
        moveCount.text = MoveNumber.moveCount.ToString();

        switch (MoveNumber.moveCount)
        {
            case 5:
                redScreen.color = new Color32(89, 0, 0, 30);
                break;
            case 4:
                redScreen.color = new Color32(89, 0, 0, 60);
                break;
            case 3:
                redScreen.color = new Color32(89, 0, 0, 90);
                break;
            case 2:
                redScreen.color = new Color32(89, 0, 0, 120);
                break;
            case 1:
                redScreen.color = new Color32(89, 0, 0, 150);
                break;
            case 0:
                redScreen.color = new Color32(89, 0, 0, 255);
                isDead = true;
                break;
            default:
                redScreen.color = new Color32(89, 0, 0, 0);
                break;
        }

        if (isDead || Input.GetKeyDown(KeyCode.R))
        {
            string currentScene;

            currentScene = SceneManager.GetActiveScene().name;

            SceneManager.LoadScene(currentScene);
        }
    }
}

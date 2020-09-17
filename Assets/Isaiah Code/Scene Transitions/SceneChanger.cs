using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public Animator anim;

    private int levelToLoad;


    public void LoadLevel2or3()
    {

    }

    public void FadeToLevel (int levelIndex)
    {
        levelToLoad = levelIndex;
        anim.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }

}

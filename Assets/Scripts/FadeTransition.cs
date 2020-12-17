using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeTransition : MonoBehaviour
{
    public Animator transAnim;
    string nextScene;

    public void SceneFadeOut(string s)
    {
        nextScene = s;
        transAnim.SetTrigger("End");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(nextScene);
    }
}

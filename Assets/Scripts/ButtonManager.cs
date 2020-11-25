using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public void Unpause()
    {
        GlobalControl.Instance.UnpauseGame();
    }

    public void RestartLevel()
    {
        GlobalControl.Instance.RestartLevel();
    }
}

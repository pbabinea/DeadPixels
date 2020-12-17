using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Text sliderText;

    private void Update()
    {
        sliderText.text = "" + GlobalControl.Instance.backlight;
    }
    public void Unpause()
    {
        GlobalControl.Instance.UnpauseGame();
    }

    public void RestartLevel()
    {
        GlobalControl.Instance.RestartLevel();
    }

    public void UpdateBackLight(System.Single val) 
    {
        GlobalControl.Instance.backlight = val;
        GlobalControl.Instance.setBacklight();
    }
}

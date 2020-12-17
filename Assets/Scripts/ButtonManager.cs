using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{

    public Text sliderText;
    public Text hintText;

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

    public void GiveHint()
    {
        if (GlobalControl.Instance.hasMazeButton) hintText.text = "Didn't Sock say he'd be waiting in my room once I got all the buttons?";
        else if (GlobalControl.Instance.hasArtButton) hintText.text = "I wonder if anymore windows have lit up?";
        else if (GlobalControl.Instance.hasBasButton) hintText.text = "It seemed like that house's window lit up after I got the button in the library...";
        else if (GlobalControl.Instance.hasLibButton) hintText.text = "I wonder if anything has changed around town?";
        else hintText.text = "I should go to the library. It should be in the northeast part of town.";
    }
}

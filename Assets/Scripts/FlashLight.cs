using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FlashLight : MonoBehaviour
{

    private bool flashlightOn = false;
    private Light2D flashlight;
    private float battery; //TODO implement

    // Start is called before the first frame update
    void Start()
    {
        flashlight = this.gameObject.GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggle() 
    {
        flashlightOn = !flashlightOn;
        if (flashlightOn)
            flashlight.intensity = 0;
        else flashlight.intensity = 1;
    }

    public void rotate() { }
    void dim() { }
    void flicker() { }
    public bool isFlashlightOn() { return flashlightOn; }
}

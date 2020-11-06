using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FlashLight : MonoBehaviour
{

    private bool flashlightOn = false;
    public Light2D flashlight;
    private float battery; //TODO implement

    // Start is called before the first frame update
    void Start()
    {
        //flashlight = this.gameObject.GetComponent<Light2D>();
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

    public void turn(Vector3 x, Vector3 y) 
    {
        Quaternion q = Quaternion.identity;
        q.SetLookRotation(Vector3.forward, x+y);
        flashlight.transform.SetPositionAndRotation(flashlight.transform.position, q);
    }
    void dim() { }
    void flicker() { }
    public bool isFlashlightOn() { return flashlightOn; }

    private void rotationReset() { flashlight.transform.SetPositionAndRotation(flashlight.transform.position, Quaternion.identity); }

    public void rotateCardinal(char dir) 
    {
        switch (dir) 
        {
            case 'w':
                this.rotationReset();
                flashlight.transform.Rotate(0f,0f,90.0f,Space.Self);
                break;
            case 's':
                this.rotationReset();
                flashlight.transform.Rotate(0f, 0f, 180.0f, Space.Self);
                break;
            case 'e':
                this.rotationReset();
                flashlight.transform.Rotate(0f, 0f, 270.0f, Space.Self);
                break;
            case 'n':
                this.rotationReset();
                break;
            default:
                break;
        }
    }

    public void rotateDynamic() { }

}

using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class FlashLight : MonoBehaviour
{
    public Light2D flashlight;
    public float powerUsageRate;
    public GameObject batteryText; //add battery text from dialouge

    private bool flashlightOn = true;
    private float battery;
    private Text txt;



    // Start is called before the first frame update
    void Start()
    {
        //flashlight = this.gameObject.GetComponent<Light2D>();
        battery = 100;
        txt = batteryText.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (flashlightOn) 
        {
            if (battery >= 0.0f)
            {
                Debug.Log("lowering");
                battery = battery - (powerUsageRate * Time.deltaTime);
            }
            else
            {
                battery = 0.0f;
                flashlightOn = false;
                flashlight.intensity = 0;
            }
        }

        txt.text = "Bat: " + (int) battery; //sets the battery number 

    }

    public void toggle() 
    {
        if (battery > 0)
        {
            flashlightOn = !flashlightOn;
            if (flashlightOn)
                flashlight.intensity = 1;
            else flashlight.intensity = 0;
        }
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

    public void addCharge(float charge) 
    {
        battery = battery + charge;
    }
}

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

    //private bool flashlightOn = true;
    private Text txt;



    // Start is called before the first frame update
    void Start()
    {
        //flashlight = this.gameObject.GetComponent<Light2D>();
        txt = batteryText.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalControl.Instance.flashlightOn) 
        {
            if (GlobalControl.Instance.currentBattery >= 0.0f)
            {
                GlobalControl.Instance.currentBattery = GlobalControl.Instance.currentBattery - (powerUsageRate * Time.deltaTime);
            }
            else
            {
                GlobalControl.Instance.currentBattery = 0.0f;
                GlobalControl.Instance.flashlightOn = false;
                flashlight.intensity = 0;
            }
        }
        txt.text = "Bat: " + (int) GlobalControl.Instance.currentBattery; //sets the battery number 
    }

    void Awake() 
    {
        if (GlobalControl.Instance.flashlightOn)
            flashlight.intensity = 1;


    }

    public void toggle() 
    {
        if (GlobalControl.Instance.currentBattery > 0)
        {
            GlobalControl.Instance.flashlightOn = !GlobalControl.Instance.flashlightOn;
            if (GlobalControl.Instance.flashlightOn)
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
    public bool isFlashlightOn() { return GlobalControl.Instance.flashlightOn; } //can be depricated

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
        GlobalControl.Instance.currentBattery = GlobalControl.Instance.currentBattery + charge;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour
{
    public static GlobalControl Instance;

    // all the bools. oh boy
    [SerializeField] public int buttons = 0;
    public float currentBattery = 100.0f;
    [SerializeField] public bool hasLibKey = false;
    [SerializeField] public bool sawBook = false;
    [SerializeField] public bool house1Open = false;
    [SerializeField] public bool hasLibButton = false;
    [SerializeField] public bool hasFirstBat = false;

    public bool flashlightOn = false;
    public bool hasBasButton = false;


    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        //currentBattery = PlayerPrefs.GetFloat("Battery");
    }


    public bool GetBool(string name)
    {
        switch (name)
        {
            case "sawBook":
                return sawBook;
            case "hasLibKey":
                return hasLibKey;
            case "hasLibButton":
                return hasLibButton;
            case "hasFirstBat":
                return hasFirstBat;
            case "hasBasButton":
                return hasBasButton;
            default:
                return false;
        }
    }

    public void SetBool(string name, bool value)
    {
        switch (name)
        {
            case "sawBook":
                sawBook = value;
                break;
            case "hasLibKey":
                hasLibKey = value;
                break;
            case "hasLibButton":
                hasLibButton = value;
                break;
            case "hasFirstBat":
                hasFirstBat = value;
                break;
            case "hasBasButton":
                hasBasButton = value;
                break;
            default:
                break;
        }
    }
}
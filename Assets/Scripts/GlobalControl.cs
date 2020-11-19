using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour
{
    public static GlobalControl Instance;

    // all the bools. oh boy
    public int buttons = 0;
    public bool hasKey = false;
    public bool sawBook = false;
    public bool house1Open = false;

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
    }


    public bool GetBool(string name)
    {
        switch (name)
        {
            case "sawBook":
                return sawBook;
            case "hasKey":
                return hasKey;
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
            case "hasKey":
                hasKey = value;
                break;
            case "house1Open":
                house1Open = value;
                break;
            default:
                break;
        }
    }
}
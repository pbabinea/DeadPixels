using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.Universal;

public class GlobalControl : MonoBehaviour
{
    public static GlobalControl Instance;

    // all the bools. oh boy
    [SerializeField] public int buttons = 0;
    public float currentBattery = 100.0f;
    public float checkpointBattery = 15.0f;
    public bool hasLibKey = false;
    public bool sawBook = false;
    public bool house1Open = false;
    public bool hasLibButton = false;
    public bool hasFirstBat = false;
    public bool flashlightOn = true;
    public bool hasBasButton = false;
    public bool hasArtButton = false;
    public bool hasMazeButton = false;
    //public static AudioClip itemPickup;

    public float backlight = 0.2f;

    public bool isPaused = false;
    private bool wasUsingFlash;

    void Awake()
    {
        Time.timeScale = 1;

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

    private void Start()
    {
        //Load sound assets:
        //itemPickup = Resources.Load<AudioClip>("Music/Effects/Key");
        //Debug.Log("ITEM SOUND: " + itemPickup);

        //audioSrc = GetComponent<AudioSource>();
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
            case "hasArtButton":
                return hasArtButton;
            case "hasMazeButton":
                return hasMazeButton;
            default:
                return false;
        }
    }

    private void Update()
    {
    
        //use esc to pause/unpause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                if (flashlightOn)
                {
                    flashlightOn = false;
                    //prevent battery running down while in pause screen
                    wasUsingFlash = true;
                }
                PauseGame();
            }
            else
            {
                if (wasUsingFlash)
                {
                    flashlightOn = true;
                    wasUsingFlash = false;
                }
                UnpauseGame();
            }
        }
    }

    //pause game and load pause menu
    void PauseGame()
    {
        Debug.Log("pause");
        Time.timeScale = 0;
        isPaused = true;
        SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
    }
    //unpause and return to game
    public void UnpauseGame()
    {
        Debug.Log("unpause");
        SceneManager.UnloadSceneAsync("PauseMenu");
        isPaused = false;
        Time.timeScale = 1;
    }

    //reset the current scene
    public void RestartLevel()
    {
        Debug.Log("restart");
        UnpauseGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

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
            case "hasArtButton":
                hasArtButton = value;
                break;
            case "hasMazeButton":
                hasMazeButton = value;
                break;
            default:
                break;
        }
    }

    public void setBacklight() 
    {
        GameObject[] backLights = GameObject.FindGameObjectsWithTag("backLight");
        foreach (GameObject l in backLights)
        {
            l.GetComponent<Light2D>().intensity = backlight;
        }
    }

    //play an audio clip
    /*
    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "pickup":
                AudioSource.PlayClipAtPoint(itemPickup, new Vector3(5, 1, 2));
                break;
        }
    }
    */
}
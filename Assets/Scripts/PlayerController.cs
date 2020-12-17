﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Collections.Specialized; //may not be needed
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //private Rigidbody2D rb;
    public float speed = 5f;
    private GameObject currInterObj;
    public FlashLight flashLight;

    public Animator dAnimator;

    public Animator playerMovement;

    //player event bools
    public bool hasLibKey;
    public bool sawBook;
    public int buttons;
    public bool hasLibButton;

    public AudioClip deathSound;
    public AudioClip flashOn;
    public AudioClip flashOff;
    public AudioClip itemPickup;

    public int dir; //1 = up, 2 = down, 3 = left, 4 = right

    // Start is called before the first frame update
    void Start()
    {
        GlobalControl.Instance.setBacklight();

        sawBook = GlobalControl.Instance.sawBook;
        hasLibKey = GlobalControl.Instance.hasLibKey;
        hasLibButton = GlobalControl.Instance.hasLibButton;
        GlobalControl.Instance.buttons = buttons;

        //spawn in from player pref spawn locations
        transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerSpawnX"), PlayerPrefs.GetFloat("PlayerSpawnY"), PlayerPrefs.GetFloat("PlayerSpawnZ"));

        //Unlock House1 when you have the library button
        string currentScene = SceneManager.GetActiveScene().name;
        Debug.Log("Stort - " + currentScene);
        if (currentScene == "Town" && GlobalControl.Instance.hasLibButton)
        {
            Debug.Log("Destroying House1 Lock");
            Destroy(GameObject.Find("House1 Lock"));
            GameObject.Find("House1 Window").transform.position += new Vector3(0, 0, 13);
        }
        if (currentScene == "Town" && GlobalControl.Instance.hasBasButton)
        {
            Debug.Log("Destroying House2 Lock");
            Destroy(GameObject.Find("House2 Lock"));
            GameObject.Find("House2 Window").transform.position += new Vector3(0, 0, 13);
        }
        if (currentScene == "Town" && GlobalControl.Instance.hasArtButton)
        {
            Debug.Log("Destroying House3 Lock");
            Destroy(GameObject.Find("House3 Lock"));
            GameObject.Find("House3 Window").transform.position += new Vector3(0, 0, 13);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //get direction player is facing
        if ((horizontalInput != 0 || verticalInput != 0) && dAnimator.GetCurrentAnimatorStateInfo(0).IsName("DialogueBox_Close"))
        {
            Vector3 x = new Vector3(1 * horizontalInput, 0, 0);
            Vector3 y = new Vector3(0, verticalInput, 0);
            flashLight.turn(x, y);
        }

        // Movement code

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            dir = 2;
            playerMovement.SetBool("idleDown", false);
            playerMovement.SetBool("movingDown", true);
            Debug.Log("movingDown");
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            dir = 1;
            playerMovement.SetBool("idleUp", false);
            playerMovement.SetBool("movingUp", true);
            Debug.Log("movingUp");
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            dir = 3;
            playerMovement.SetBool("idleLeft", false);
            playerMovement.SetBool("movingLeft", true);
            Debug.Log("movingLeft");
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            dir = 4;
            playerMovement.SetBool("idleRight", false);
            playerMovement.SetBool("movingRight", true);
            Debug.Log("movingRight");
        }

        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            playerMovement.SetBool("movingDown", false);
            playerMovement.SetBool("idleDown", true);
            Debug.Log("idleDown");
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
        {
            playerMovement.SetBool("movingUp", false);
            playerMovement.SetBool("idleUp", true);
            Debug.Log("idleUp");
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
            playerMovement.SetBool("movingLeft", false);
            playerMovement.SetBool("idleLeft", true);
            Debug.Log("idleLeft");
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            playerMovement.SetBool("movingRight", false);
            playerMovement.SetBool("idleRight", true);
            Debug.Log("idleRight");
        }

        //FindObjectOfType<GlobalControl>().hasKey = true;

        //toggle flashlight
        if (Input.GetButtonDown("ToggleLight")) 
        {
            flashLight.toggle(flashOn, flashOff);
        }

        //check interactions
        if (currInterObj != null)
        {
            //trigger dialogue
            if (Input.GetKeyDown(KeyCode.F) && currInterObj.tag.Equals("DialogueTrigger") && dAnimator.GetCurrentAnimatorStateInfo(0).IsName("DialogueBox_Close"))
            {
                Debug.Log("F pressed; dialogue");
                currInterObj.GetComponentInParent<DialogueTrigger>().TriggerDialogue();
                if (currInterObj.GetComponent<DialogueTrigger>().hasEvent)
                {
                    currInterObj.GetComponentInParent<DialogueTrigger>().TriggerEvent();
                }
            }
            //continue dialogue
            if (Input.GetKeyDown(KeyCode.Space) && currInterObj.tag.Equals("DialogueTrigger"))
            {
                Debug.Log("Space pressed; continue dialogue");
                currInterObj.GetComponentInParent<DialogueTrigger>().AdvanceDialogue();
            }
            
            //pick up battery
            if (Input.GetKeyDown(KeyCode.F) && currInterObj.CompareTag("Battery"))
            {
                //special case of first battery. unlocks door and performs battery actions
                if (currInterObj.name == "FirstBattery")
                {
                    Battery bat = currInterObj.GetComponent<Battery>();
                    flashLight.addCharge(bat.getCharge());
                    currInterObj.GetComponentInParent<DialogueTrigger>().TriggerEvent();
                }
                else
                {
                    AudioSource.PlayClipAtPoint(itemPickup, new Vector3(5, 1, 2), 1f);
                    //add charge to player's flashlight and destroy the battery
                    Battery bat = currInterObj.GetComponent<Battery>();
                    flashLight.addCharge(bat.getCharge());
                    Destroy(currInterObj);
                }
            }
        }
        //push puzzle block
        if (Input.GetKeyDown(KeyCode.F))// && isPuzzleBlock(currInterObj.tag))
        {
            //This code is supposed to detect which block the PC is looking at. Only works if looking down.
            RaycastHit2D hit;
            //Vector2 rayDir = getDirectionFacing();

            //Vector3 rayDir = flashLight.transform.eulerAngles;

            //Quaternion q = flashLight.transform.rotation;
            //Vector2 rayDir = new Vector2(q.x, q.y);

            Vector2 rayDir = getDirectionFacing();

            //Debug.Log("Q DIR: " + q);
            Debug.Log("DIRECTION: " + rayDir);
            LayerMask mask = LayerMask.GetMask("BoxInteract");
            hit = Physics2D.Raycast(transform.position, rayDir, 1f, mask, -100.0f, 100.0f);
            Debug.Log("HIT COLLIDER: " + hit.collider.gameObject.name);
            Debug.Log("HIT COLLIDER TRANSFORM: " + hit.collider.transform.position);
            Debug.DrawLine(transform.position, hit.collider.transform.position, Color.red);
            hit.collider.gameObject.GetComponentInParent<PuzzleBlock>().interact(hit.collider.gameObject.name);
            //Debug.Log("F pressed - " + currInterObj.name);
            //currInterObj.GetComponentInParent<PuzzleBlock>().interact(currInterObj.name);
        }
    }

        void FixedUpdate() 
        {
        //player movement. cannot move if paused or in dialogue
        Debug.Log("current time scale: " + Time.timeScale);
            if (dAnimator.GetCurrentAnimatorStateInfo(0).IsName("DialogueBox_Close") && !GlobalControl.Instance.isPaused) { 
                float horizontalInput = Input.GetAxis("Horizontal");
                float verticalInput = Input.GetAxis("Vertical");
                //L/R movement
                transform.position = transform.position + new Vector3(horizontalInput * speed * Time.deltaTime, 0, 0);
                //rb.velocity = new Vector2(horizontalInput * speed * Time.deltaTime, rb.velocity.y);

                //Up/Down movement
                transform.position = transform.position + new Vector3(0, verticalInput * speed * Time.deltaTime, 0);
                //rb.velocity = new Vector2(verticalInput * speed * Time.deltaTime, rb.velocity.x);
            }
        }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //set currInterObj
        if (isInteractable(col.tag))
        {
            currInterObj = col.gameObject;
        }

        //load new area
        if (col.gameObject.tag.Equals("AreaTrigger"))
        {
            col.gameObject.GetComponent<SceneTransition>().LoadNextScene();
        }

        //pick up battery
        if (col.gameObject.tag.Equals("Battery"))
        {
            currInterObj = col.gameObject;
        }

        //enemy kills player
        if (col.gameObject.tag.Equals("KILL")) 
        {
            Debug.Log("Death Sound Play");
            if (SceneManager.GetActiveScene().name == "House3" && GlobalControl.Instance.hasMazeButton) GlobalControl.Instance.SetBool("hasMazeButton", false);
            AudioSource.PlayClipAtPoint(deathSound, new Vector3(5, 1, 2));
            GlobalControl.Instance.currentBattery = GlobalControl.Instance.checkpointBattery;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        //unset currInterObj
        if (isInteractable(col.tag))
        {
            Debug.Log("Trigger Exit: " + currInterObj.name);
            currInterObj = null;
        }
    }

    public void SavePlayer()
    {
        GlobalControl.Instance.sawBook = sawBook;
        GlobalControl.Instance.hasLibKey = hasLibKey;
        GlobalControl.Instance.buttons = buttons;
        GlobalControl.Instance.hasLibButton = hasLibButton;
    }

    private bool isPuzzleBlock(string tag)
    {
        if (tag.Equals("PBUp") || tag.Equals("PBDown") || tag.Equals("PBLeft") || tag.Equals("PBRight")) return true;
        return false;
    }

    //used to identify the various interactable objects
    private bool isInteractable(string tag)
    {
        switch (tag)
        {
            case "Battery":
                return true;
            case "DialogueTrigger":
                return true;
            //all puzzle block tags:
            case "PuzzleBlock":
                return true;
            case "PBUp":
                return true;
            case "PBLeft":
                return true;
            case "PBRight":
                return true;
            case "PBDown":
                return true;
            default:
                return false;
        }
    }

    private Vector2 getDirectionFacing() 
    {
        switch (dir)
        {
            case 1:
                return Vector2.up;
                break;
            case 2:
                return Vector2.down;
                break;
            case 3:
                return Vector2.left;
                break;
            case 4:
                return Vector2.right;
                break;
            default:
                return Vector2.zero;
        }
        /*
        if (playerMovement.GetBool("idleDown") || playerMovement.GetBool("movingDown")) 
        {
            return Vector2.down;
        }
        else if (playerMovement.GetBool("idleUp") || playerMovement.GetBool("movingUp"))
        {
            return Vector2.up;
        }
        else if (playerMovement.GetBool("idleLeft") || playerMovement.GetBool("movingLeft"))
        {
            return Vector2.left;
        }
        else if (playerMovement.GetBool("idleRight") || playerMovement.GetBool("movingRight"))
        {
            return Vector2.right;
        }
        return Vector2.zero;
        */
    }
}

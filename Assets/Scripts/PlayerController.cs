using System;
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

    //player event bools
    public bool hasLibKey;
    public bool sawBook;
    public int buttons;
    public bool hasLibButton;

    // Start is called before the first frame update
    void Start()
    {
        sawBook = GlobalControl.Instance.sawBook;
        hasLibKey = GlobalControl.Instance.hasLibKey;
        hasLibButton = GlobalControl.Instance.hasLibButton;
        GlobalControl.Instance.buttons = buttons;

        transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerSpawnX"), PlayerPrefs.GetFloat("PlayerSpawnY"), PlayerPrefs.GetFloat("PlayerSpawnZ"));

        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "Town" && FindObjectOfType<GlobalControl>().hasLibButton)
        {
            Destroy(GameObject.Find("House1 Lock"));
            GameObject.Find("House1 Window").transform.position += new Vector3(0, 0, 13);
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



        //FindObjectOfType<GlobalControl>().hasKey = true;

        //toggle flashlight
        if (Input.GetButtonDown("ToggleLight")) 
        {
            flashLight.toggle();
        }
        if (currInterObj != null)
        {
            if (Input.GetKeyDown(KeyCode.F) && currInterObj.tag.Equals("DialogueTrigger") && dAnimator.GetCurrentAnimatorStateInfo(0).IsName("DialogueBox_Close"))
            {
                Debug.Log("F pressed; dialogue");
                currInterObj.GetComponentInParent<DialogueTrigger>().TriggerDialogue();
                if (currInterObj.GetComponent<DialogueTrigger>().hasEvent)
                {
                    currInterObj.GetComponentInParent<DialogueTrigger>().TriggerEvent();
                }
            }
            if (Input.GetKeyDown(KeyCode.Space) && currInterObj.tag.Equals("DialogueTrigger"))
            {
                Debug.Log("Space pressed; continue dialogue");
                currInterObj.GetComponentInParent<DialogueTrigger>().AdvanceDialogue();
            }
        }
        if (Input.GetKeyDown(KeyCode.F) && currInterObj.tag.Equals("PuzzleBlock"))
        {
            Debug.Log("F pressed");
            currInterObj.GetComponentInParent<PuzzleBlock>().interact(currInterObj.gameObject.name);
        }
    }

        void FixedUpdate() 
        {
            if (dAnimator.GetCurrentAnimatorStateInfo(0).IsName("DialogueBox_Close")) { 
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
        //push block
        if (col.gameObject.tag.Equals("PuzzleBlock"))
        {
            Debug.Log("PuzzleBlock collision");
            currInterObj = col.gameObject;
        }

        //see text
        if (col.gameObject.tag.Equals("DialogueTrigger"))
        {
            Debug.Log("Dialogue collision");
            currInterObj = col.gameObject;
        }

        //pick up an item
        if (col.gameObject.tag.Equals("Collectable"))
        {
            col.gameObject.GetComponent<CollectableBehaviour>().Action();
        }

        //load new area
        if (col.gameObject.tag.Equals("AreaTrigger"))
        {
            col.gameObject.GetComponent<SceneTransition>().LoadNextScene();
        }

        if (col.gameObject.tag.Equals("KILL")) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (col.gameObject.tag.Equals("Battery"))
        {
            Battery bat = col.gameObject.GetComponent<Battery>();
            flashLight.addCharge(bat.getCharge());
            Destroy(col.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("PuzzleBlock") || col.gameObject.tag.Equals("DialogueTrigger"))
        { 
            currInterObj = null;
            Debug.Log(currInterObj);
        }
    }

    public void SavePlayer()
    {
        GlobalControl.Instance.sawBook = sawBook;
        GlobalControl.Instance.hasLibKey = hasLibKey;
        GlobalControl.Instance.buttons = buttons;
        GlobalControl.Instance.hasLibButton = hasLibButton;
    }


}

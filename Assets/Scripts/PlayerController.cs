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
    public bool hasKey = false;
    public FlashLight flashLight;

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerSpawnX"), PlayerPrefs.GetFloat("PlayerSpawnY"), PlayerPrefs.GetFloat("PlayerSpawnZ"));
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //get direction player is facing
        if (horizontalInput != 0 || verticalInput != 0)
        {
            Vector3 x = new Vector3(1 * horizontalInput, 0, 0);
            Vector3 y = new Vector3(0, verticalInput, 0);
            flashLight.turn(x, y);
        }

        //toggle flashlight
        if (Input.GetButtonDown("ToggleLight")) 
        {
            flashLight.toggle();
        }
        if (currInterObj != null)
        {
            if (Input.GetKeyDown(KeyCode.F) && currInterObj.tag.Equals("DialogueTrigger"))
            {
                Debug.Log("F pressed; dialogue");
                currInterObj.GetComponentInParent<DialogueTrigger>().TriggerDialogue();
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
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        //L/R movement
        transform.position = transform.position + new Vector3(horizontalInput * speed * Time.deltaTime, 0, 0);
        //rb.velocity = new Vector2(horizontalInput * speed * Time.deltaTime, rb.velocity.y);

        //Up/Down movement
        transform.position = transform.position + new Vector3(0, verticalInput * speed * Time.deltaTime, 0);
        //rb.velocity = new Vector2(verticalInput * speed * Time.deltaTime, rb.velocity.x);
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

        //load new area
        if (col.gameObject.tag.Equals("AreaTrigger"))
        {
            Debug.Log("stair collision");
            col.gameObject.GetComponent<SceneTransition>().LoadNextScene();
        }

        if (col.gameObject.tag.Equals("KILL")) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
    
    //pick up key
    public void GetKey()
    {
        hasKey = true;
        Debug.Log(hasKey);
    }
}

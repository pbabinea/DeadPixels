using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //private Rigidbody2D rb;
    public float speed = 5f;
    public FlashLight flashLight;
    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
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

        

        //change direction facing

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

    //load new area
    private void OnTriggerEnter2D(Collider2D col)
    {  
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
}

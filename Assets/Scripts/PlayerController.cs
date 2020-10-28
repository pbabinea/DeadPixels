using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //private Rigidbody2D rb;
    public float speed = 5f;

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

        //L/R movement
        transform.position = transform.position + new Vector3(horizontalInput * speed * Time.deltaTime, 0, 0);
        //rb.velocity = new Vector2(horizontalInput * speed * Time.deltaTime, rb.velocity.y);

        //Up/Down movement
        transform.position = transform.position + new Vector3(0, verticalInput * speed * Time.deltaTime, 0);
        //rb.velocity = new Vector2(verticalInput * speed * Time.deltaTime, rb.velocity.x);
    }
}

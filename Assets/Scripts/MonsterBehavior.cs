using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehavior : MonoBehaviour
{
    public float speed;
    private bool targeting = false;
    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (targeting) 
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed);

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ("Player".Equals(collision.gameObject.tag))
        {
            Debug.Log("Enter");
            target = collision.gameObject;
            targeting = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ("Player".Equals(collision.gameObject.tag))
        {
            Debug.Log("Exit");
            targeting = false;
        }

    }
}

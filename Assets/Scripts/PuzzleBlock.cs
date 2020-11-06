using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PuzzleBlock : MonoBehaviour
{
    private float speed = 5f;
    public float step;
    private Vector3 targetPos;// = new Vector3(-2.19f, -0.58f, -7.5398f);
    // Start is called before the first frame update
    void Awake()
    {
        targetPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
    }

    public void interact(string name)
    {
        Debug.Log(name);
        //Debug.Log("player pos: " + pos + "block pos: " + transform.position);
        //Vector2 sz = this.gameObject.GetComponent<BoxCollider2D>().size;
        if (name == "InteractR") //push left
            targetPos = transform.position + new Vector3(-1, 0, 0);
        else if (name == "InteractL") //push right
            targetPos = transform.position + new Vector3(1, 0, 0);
        else if (name == "InteractU") //push down
            targetPos = transform.position + new Vector3(0, -1, 0);
        else if (name == "InteractD") //push up
            targetPos = transform.position + new Vector3(0, 1, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        targetPos = transform.position;
    }
}

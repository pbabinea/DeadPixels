using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBlock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void interact(Vector3 pos)
    {
        Debug.Log("interacting");
        Vector2 sz = this.gameObject.GetComponent<BoxCollider2D>().size;
        if (pos.x >= transform.position.x + (sz.x/2)) //push down
            transform.position += new Vector3(-1, 0, 0);
        else if (pos.x <= transform.position.x - (sz.x / 2)) //push up
            transform.position += new Vector3(1, 0, 0);
        else if (pos.y >= transform.position.x + (sz.y / 2)) //push left
            transform.position += new Vector3(0, -1, 0);
        else if (pos.y <= transform.position.x - (sz.y / 2)) //push right
            transform.position += new Vector3(0, 1, 0);
    }
}

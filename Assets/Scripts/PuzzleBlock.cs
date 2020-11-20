using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PuzzleBlock : MonoBehaviour
{
    private float speed = 5f;
    private float step;
    private Vector3 targetPos;// = new Vector3(-2.19f, -0.58f, -7.5398f);
    private Rigidbody2D rb;

    [SerializeField] private bool leftEnabled = true;
    [SerializeField] private bool rightEnabled = true;
    [SerializeField] private bool upEnabled = true;
    [SerializeField] private bool downEnabled = true;

    // Start is called before the first frame update
    void Awake()
    {
        if (this.gameObject.name == "SpecialBlock2")
        {
            transform.position = new Vector3(PlayerPrefs.GetFloat("blockX"), PlayerPrefs.GetFloat("blockY"), PlayerPrefs.GetFloat("blockZ"));
        }
        targetPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        step = speed * Time.deltaTime;
        rb.MovePosition(Vector3.MoveTowards(transform.position, targetPos, step));
    }

    public void interact(string name)
    {
        Debug.Log(name);
        Debug.Log("rightEnabled: " + rightEnabled);
        //Debug.Log("player pos: " + pos + "block pos: " + transform.position);
        //Vector2 sz = this.gameObject.GetComponent<BoxCollider2D>().size;
        if (name == "InteractR" && leftEnabled) //push left
            targetPos = transform.position + Vector3.left;
        else if (name == "InteractL" && rightEnabled) { //push right
            Debug.Log("Pushing right");
            targetPos = transform.position + Vector3.right;
        }
        else if (name == "InteractU" && downEnabled) //push down
            targetPos = transform.position + Vector3.down;
        else if (name == "InteractD" && upEnabled) //push up
            targetPos = transform.position + Vector3.up;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Wall")) targetPos = transform.position;
    }

    public void SetRightEnabled(bool enabled)
    {
        Debug.Log("SetRightEnabled: " + enabled);
        rightEnabled = enabled;
    }
    public void SetLeftEnabled(bool enabled)
    {
        Debug.Log("SetLeftEnabled: " + enabled);
        leftEnabled = enabled;
    }
    public void SetUpEnabled(bool enabled)
    {
        Debug.Log("SetUpEnabled: " + enabled);
        upEnabled = enabled;
    }
    public void SetDownEnabled(bool enabled)
    {
        Debug.Log("SetDownEnabled: " + enabled);
        downEnabled = enabled;
    }
}

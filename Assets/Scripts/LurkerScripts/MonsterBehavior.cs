using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehavior : MonoBehaviour
{
    public float speed;
    public bool idle = false;
    private bool targeting = false;
    private GameObject player;
    public Transform target;
    public LineOfSight LOS;
    public GameObject sceneLoad; //depricated
    public Transform[] wayPoints; //must be in order!!!!!!!!!
    public int numOfWaypoints;
    public int pt = 0;
    private Animator fsm;
    // Start is called before the first frame update
    void Start()
    {
        numOfWaypoints = wayPoints.Length - 1;
        fsm = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!idle) 
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed);
            if (LOS.checkLOS(this.gameObject, player))
            {
                targeting = true;
            }
        }
        fsm.SetFloat("distance", Vector3.Distance(transform.position, target.position));


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ("Player".Equals(collision.gameObject.tag))
        {
            player = collision.gameObject;
            if (LOS.checkLOS(this.gameObject, player))
            {
                targeting = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ("Player".Equals(collision.gameObject.tag))
        {
            targeting = false;
        }

    }

    public void nextPoint() 
    {
        target = wayPoints[pt];
        pt = (pt + 1) % numOfWaypoints;
    }


    
}

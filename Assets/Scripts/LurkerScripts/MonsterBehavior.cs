using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehavior : MonoBehaviour
{
    public float speed;
    public bool idle = false;
    public GameObject player;
    public LineOfSight LOS;
    public Transform[] wayPoints; //must be in order!!!!!!!!!
    public Vector3 spawn;

    private int moving = 0; //0 = not moving, 1 = patroling, 2 = tracking player, 3 = track to last player position
    private Transform target;
    private int numOfWaypoints;
    private int pt = 0;
    private Animator fsm;
    private Vector3 lastSeen;


    // Start is called before the first frame update
    void Start()
    {
        numOfWaypoints = wayPoints.Length;
        if (numOfWaypoints < 0)
            idle = true;
        fsm = gameObject.GetComponent<Animator>();
        fsm.SetBool("setIdle", idle);
        spawn = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //handles monster movement
        switch (moving) 
        {
            case 0:
                break;
            case 1:
                transform.position = Vector3.MoveTowards(transform.position, target.position, speed);
                fsm.SetFloat("distance", Vector3.Distance(transform.position, target.position));
                break;
            case 2:
                transform.position = Vector3.MoveTowards(transform.position, target.position, speed);
                fsm.SetBool("canSee", LOS.checkLOS(this.gameObject, player));
                break;
            case 3:
                transform.position = Vector3.MoveTowards(transform.position, lastSeen, speed);
                fsm.SetFloat("distance", Vector3.Distance(transform.position, lastSeen));
                break;
            default:
                moving = 0;
                break;
        }


    }

    private void OnTriggerStay2D(Collider2D collision) 
    {

        //handles if the monster is tracking the player
        if ("Player".Equals(collision.gameObject.tag)) //may be really inefficient. COnsider just checking LOS every frame
        {
            if (LOS.checkLOS(this.gameObject, player))
            {
                fsm.SetBool("canSee", true);
                //targeting = true;
            }
        }
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if ("Player".Equals(collision.gameObject.tag))
    //    {
    //        targeting = false;
    //    }
    //
    //}

    public void nextPoint() 
    {
        pt = (pt + 1) % numOfWaypoints; //sets the point the next one in path
        Debug.Log(pt);
    }

    private void setTarget()
    {
        target = wayPoints[pt]; //sets to current waypoint
    }

    private void setTarget(Transform point)
    {
        target = point; //sets to given point
    }

    public void setMovement(string b) 
    {
        Debug.Log("setMove");
        switch (b) 
        {
            case "waypoint":
                moving = 1;
                setTarget();
                break;
            case "player":
                moving = 2;
                setTarget(player.transform);
                break;
            case "playerLastPosition":
                moving = 3;
                lastSeen = player.transform.position;
                break;
            default:
                moving = 0;
                break;
        }
    }

    public void teleport(Vector3 loc) 
    {
        this.gameObject.transform.position = loc;
    }
}

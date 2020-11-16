using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool checkLOS(GameObject viewer, GameObject target) 
    {
        RaycastHit2D hit;
        Vector2 rayDir = target.transform.position - viewer.transform.position;
        int layerMask = -1;
        Debug.Log("Trying");
        hit = Physics2D.Raycast(viewer.transform.position, rayDir, 8.0f, layerMask, -100.0f, 100.0f);
        Debug.Log(hit.transform.position);
        Debug.Log(target.transform.position);
        if (hit.transform == target.transform)
        {
            return true;
        }
        return false;
    }
}

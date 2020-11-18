using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    public bool checkLOS(GameObject viewer, GameObject target) 
    {
        RaycastHit2D hit;
        Vector2 rayDir = target.transform.position - viewer.transform.position;
        int layerMask = -1;
        hit = Physics2D.Raycast(viewer.transform.position, rayDir, 8.0f, layerMask, -100.0f, 100.0f);
        if (hit.transform == target.transform)
        {
            return true;
        }
        return false;
    }
}

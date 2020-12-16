using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoad : MonoBehaviour
{
    public GameObject[] allWayPoints;

    void Start()
    {
        allWayPoints = GameObject.FindGameObjectsWithTag("wayPoint");
        GlobalControl.Instance.setBacklight();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

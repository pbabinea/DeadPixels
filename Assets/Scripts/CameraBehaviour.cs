using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraBehaviour : MonoBehaviour
{
    public Transform player;
    public float maxX;
    public float minX;
    public float maxY;
    public float minY;

    // Update is called once per frame
    void Update()
    {
        //camera centers on player, but stops on edge of bounds
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = player.position;

        //don't move
        if ((targetPosition.x > maxX / 2 || targetPosition.x < minX / 2) && (targetPosition.y > maxY / 2 || targetPosition.y < minY / 2))
            transform.position = currentPosition;
        //only change y position 
        else if (targetPosition.x > maxX / 2 || targetPosition.x < minX / 2)
            transform.position = new Vector3(currentPosition.x, targetPosition.y, currentPosition.z);
        //only change x position
        else if (targetPosition.y > maxY / 2 || targetPosition.y < minY / 2)
            transform.position = new Vector3(targetPosition.x, currentPosition.y, currentPosition.z);
        //center player
        else transform.position = new Vector3(targetPosition.x, targetPosition.y, currentPosition.z);

    }
}

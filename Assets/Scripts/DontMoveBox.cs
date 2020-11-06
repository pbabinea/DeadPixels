using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontMoveBox : MonoBehaviour
{
    public Transform box;

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("collision");
        if ("Player".Equals(col.gameObject.tag))
        {
            PlayerPrefs.SetFloat("blockX", box.position.x);
            PlayerPrefs.SetFloat("blockY", box.position.y);
            PlayerPrefs.SetFloat("blockZ", box.position.z);
        }
    }
}

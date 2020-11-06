using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleDoor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("collision");
        if ("Player".Equals(col.gameObject.tag))
        {
            if (col.gameObject.GetComponent<PlayerController>().hasKey) 
            {
                SceneManager.LoadScene("DemoEnding");
            }
        }
    }
}

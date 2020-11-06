using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBehaviour : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("collision");
        if ("Player".Equals(col.gameObject.tag))
        {
            col.gameObject.GetComponent<PlayerController>().GetKey();
            Destroy(this.gameObject);
        }
    }
}

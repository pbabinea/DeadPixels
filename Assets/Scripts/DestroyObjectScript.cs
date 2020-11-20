using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectScript : MonoBehaviour
{
    // Start is called before the first frame update
    public string check;

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<GlobalControl>().GetBool(check)) Destroy(this.gameObject);
    }
}

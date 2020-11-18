using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class CollectableBehaviour : MonoBehaviour
{
    public string message;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Action()
    {
        //Write text to file
        string path = @"C:\Users\Public\HelloReadMe.txt";
        //Create and write to file if doesn't exist already
        if (!File.Exists(path))
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine(message);
            }
        }
        //Write to pre-existing file
        else using (StreamWriter sw = File.AppendText(path))
        {
                sw.WriteLine(message);
        }
        
        Destroy(this.gameObject);
        //if (this.gameObject.name == "Button1"){ } this can be used to differentiate more detailed actions
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDetect : MonoBehaviour
{
    private PuzzleBlock pBlock;

    private void Start()
    {
        pBlock = GetComponentInParent<PuzzleBlock>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log("wall detect: " + collision.gameObject.tag + " + " + this.gameObject.tag);
        if (collision.gameObject.tag.Equals("Wall") || collision.gameObject.tag.Equals("PuzzleBlock"))
        {
            switch (this.gameObject.tag)
            {
                case "PBLeft":
                    pBlock.SetLeftEnabled(false);
                    Debug.Log("disable left");
                    break;
                case "PBRight":
                    pBlock.SetRightEnabled(false);
                    Debug.Log("disable right");
                    break;
                case "PBDown":
                    pBlock.SetDownEnabled(false);
                    Debug.Log("disable down");
                    break;
                case "PBUp":
                    pBlock.SetUpEnabled(false);
                    Debug.Log("disable up");
                    break;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        Debug.Log("wall detect: " + collision.gameObject.tag + " + " + this.gameObject.tag);
        if (collision.gameObject.tag.Equals("Wall") || collision.gameObject.tag.Equals("PuzzleBlock"))
        {
            switch (this.gameObject.tag)
            {
                case "PBLeft":
                    pBlock.SetLeftEnabled(true);
                    Debug.Log("enable left");
                    break;
                case "PBRight":
                    pBlock.SetRightEnabled(true);
                    Debug.Log("enable right");
                    break;
                case "PBDown":
                    pBlock.SetDownEnabled(true);
                    Debug.Log("enable down");
                    break;
                case "PBUp":
                    pBlock.SetUpEnabled(true);
                    Debug.Log("enable up");
                    break;
            }
        }
    }
}

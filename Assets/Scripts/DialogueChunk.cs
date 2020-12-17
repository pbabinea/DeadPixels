using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueChunk : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerCutsceneDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    public void AdvanceCutsceneDialogue()
    {
        if (dialogue != null)
        {
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
        }
    }
}

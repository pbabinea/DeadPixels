using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

	public Dialogue dialogue;

	public void TriggerDialogue()
	{
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
	}

	public void AdvanceDialogue()
    {
		if (dialogue != null)
        {
			FindObjectOfType<DialogueManager>().DisplayNextSentence();
        }
		else if (dialogue == null)
        {
			FindObjectOfType<GlobalControl>().dialogueOpen = false;
		}
    }

}
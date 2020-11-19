using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

	public Dialogue dialogue;
	public bool hasEvent;
    public string eventType;
	public bool setBoolValue;
	public GameObject target;
	public string boolName;

    private void Awake()
    {
        if (FindObjectOfType<GlobalControl>().sawBook)
        {
			Destroy(target);
        }
    }
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
    }

	public void TriggerEvent()
    {
		Debug.Log("Event triggered!");

        switch (eventType) {
			case "destroyObject":
				Destroy(target);
				FindObjectOfType<GlobalControl>().SetBool(boolName, setBoolValue);
				break;
			case "setBoolean":
				FindObjectOfType<GlobalControl>().SetBool(boolName, setBoolValue);
				break;
			default:
				break;
		}

    }

}
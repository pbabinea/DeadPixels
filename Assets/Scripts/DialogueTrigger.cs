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
	public bool pickUppable;

    private void Awake()
    {
        if (GlobalControl.Instance.sawBook && this.gameObject.name.Equals("Book")|| 
			(GlobalControl.Instance.hasLibKey && this.gameObject.name.Equals("DoorKey")) || 
			(GlobalControl.Instance.hasLibButton && this.gameObject.name.Equals("Button")) ||
			(GlobalControl.Instance.hasFirstBat && this.gameObject.name.Equals("FirstBattery")))
        {
			Debug.Log("Destroying " + this.gameObject.name);
			Destroy(target);
			if (pickUppable) Destroy(this.gameObject);
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
				if (target != null) Destroy(target);
				GlobalControl.Instance.SetBool(boolName, setBoolValue);
				if (pickUppable) Destroy(this.gameObject);
				break;
			case "setBoolean":
				GlobalControl.Instance.SetBool(boolName, setBoolValue);
				if (target != null) Destroy(target);
				if (pickUppable)
				{
					Debug.Log("Picking up " + this.gameObject.name);
					Destroy(this.gameObject);
				}
					break;
			default:
				break;
		}

    }

}
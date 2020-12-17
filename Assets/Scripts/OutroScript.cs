using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutroScript : MonoBehaviour
{
    public GameObject dialogueChunk1;
    public Animator dAnimator;
    GameObject currDialogue;
    bool firstDialogueDone = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartSequence());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currDialogue != null)
        {
            Debug.Log("Space pressed; continue dialogue");
            currDialogue.GetComponentInParent<DialogueChunk>().AdvanceCutsceneDialogue();
        }

        if (firstDialogueDone && dAnimator.GetCurrentAnimatorStateInfo(0).IsName("DialogueBox_Close"))
        {
            Application.Quit();
            Debug.Log("End Game");
        }
    }

    public IEnumerator StartSequence()
    {
        yield return new WaitForSeconds(2);
        FindObjectOfType<FadeController>().FadeIn();
        currDialogue = dialogueChunk1;
        currDialogue.GetComponentInParent<DialogueChunk>().TriggerCutsceneDialogue();
        yield return new WaitForSeconds(5);
        firstDialogueDone = true;
    }
}

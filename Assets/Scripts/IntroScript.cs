using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScript : MonoBehaviour
{

    public GameObject dialogueChunk1;
    public GameObject dialogueChunk2;
    public Animator dAnimator;
    GameObject currDialogue;
    bool cutsceneDone = false;
    bool firstDialogueDone = false;
    bool dialogueDone = false;

    void Start()
    {
        StartCoroutine(StartSequence());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currDialogue != null)
        {
            Debug.Log("Space pressed; continue dialogue");
            currDialogue.GetComponentInParent<DialogueChunk>().AdvanceCutsceneDialogue();
        }

        if (firstDialogueDone && dAnimator.GetCurrentAnimatorStateInfo(0).IsName("DialogueBox_Close"))
        {
            StartCoroutine(FirstSubsequence());
        }

        if (dialogueDone && !cutsceneDone && dAnimator.GetCurrentAnimatorStateInfo(0).IsName("DialogueBox_Close"))
        {
            StartCoroutine(SecondSubsequence());
        }

        if (cutsceneDone && dAnimator.GetCurrentAnimatorStateInfo(0).IsName("DialogueBox_Close"))
        {
            GetComponent<SceneTransition>().LoadNextScene();
        }
    }


    public IEnumerator StartSequence()
    {
        Debug.Log("Starting intro");
        yield return new WaitForSeconds(2);
        currDialogue = dialogueChunk1;
        currDialogue.GetComponentInParent<DialogueChunk>().TriggerCutsceneDialogue();
        yield return new WaitForSeconds(5);
        firstDialogueDone = true;
    }

    public IEnumerator FirstSubsequence()
    {
        Debug.Log("Starting first");
        FindObjectOfType<FadeController>().FadeIn();
        Debug.Log("test");
        currDialogue = dialogueChunk2;
        currDialogue.GetComponentInParent<DialogueChunk>().TriggerCutsceneDialogue();
        yield return new WaitForSeconds(5);
        dialogueDone = true;
    }

    public IEnumerator SecondSubsequence()
    {
        Debug.Log("Starting second");
        FindObjectOfType<FadeController>().FadeOut(2);
        yield return new WaitForEndOfFrame();
        Debug.Log("End of cutscene");
        dialogueDone = false;
        cutsceneDone = true;
    }
}

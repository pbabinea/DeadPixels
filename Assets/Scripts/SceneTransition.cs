using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string nextScene;
    public GameObject transitionAnimator;

    public void LoadNextScene()
    {
        //set spawn location based on exit/entry points of a scene
        string currentScene = SceneManager.GetActiveScene().name;
        switch (currentScene)
        {
            case "DemoMenu":
                if (nextScene == "Bedroom") SetSpawn(6.34f, 0.31f, 1f);
                break;
            case "GameIntro":
                if (nextScene == "Bedroom") SetSpawn(6.34f, 0.31f, 1f);
                break;
            case "Bedroom":
                if (nextScene == "PlayerHouse") SetSpawn(-2.039f, 2.418f, 1f);
                break;
            case "PlayerHouse":
                if (nextScene == "Bedroom")
                {
                    if (GlobalControl.Instance.hasAllButtons()) nextScene = "GameOutro";
                    SetSpawn(-7.97f, -2.38f, 1f);
                }
                if (nextScene == "Town") SetSpawn(-3.44f, -1.24f, 1f);
                break;
            case "Town":
                if (nextScene == "PlayerHouse") SetSpawn(-0.53f, -2.04f, 1f);
                if (nextScene == "Library Puzzle 1")
                {
                    //skip Library Puzzle 1 if Library Puzzle 2 has already been solved (i.e. you have the key already)
                    if (FindObjectOfType<GlobalControl>().hasLibKey) nextScene = "Library Puzzle 2";
                    SetSpawn(-2.02f, -4.19f, 1f);
                }
                if (nextScene == "House1") SetSpawn(0.99f, -3.38f, 1f);
                if (nextScene == "House2") SetSpawn(-4.01f, -3.65f, 1f);
                if (nextScene == "House3") SetSpawn(-4.01f, -2.58f, 1f);
                break;
            case "Library Puzzle 1":
            case "Library Puzzle 2":
                if (nextScene == "Town") SetSpawn(18.95f, 9.14f, 1f);
                if (nextScene == "Library Puzzle 2")
                {
                    // make sure the block stays put between library puzzle 1 and 2
                    Vector3 blockPos = FindObjectOfType<PuzzleBlock>().transform.position;
                    PlayerPrefs.SetFloat("blockX", blockPos.x);
                    PlayerPrefs.SetFloat("blockY", blockPos.y);
                    PlayerPrefs.SetFloat("blockZ", blockPos.z);
                    SetSpawn(4.17f, -2.75f, 1f);
                    GlobalControl.Instance.checkpointBattery = GlobalControl.Instance.currentBattery;
                    SceneManager.LoadScene(nextScene);
                }
                if (nextScene == "Library Puzzle 3") SetSpawn(4.51f, 3.52f, 1f);
                break;
            case "House1":
                if (nextScene == "Town") SetSpawn(25.96f, -1.51f, 1f);
                if (nextScene == "Basement1") SetSpawn(9.03f, -1.62f, 1f);
                break;
            case "Basement1":
                if (nextScene == "House1") SetSpawn(15.5f, 3.92f, 1f); 
                break;
            case "Library Puzzle 3":
                if (nextScene == "Library Puzzle 2") SetSpawn(4.17f, -2.75f, 1f);
                break;
            case "House2":
                if (nextScene == "Town") SetSpawn(-17.09f, -11.31f, 1f);
                break;
            case "House3":
                if (nextScene == "Town") SetSpawn(-29.36f, 16.2f, 1f);
                break;
            default:
                break;
        }
        //set a checkpoint battery upon loading new scene
        if (currentScene != "DemoMenu") GlobalControl.Instance.checkpointBattery = GlobalControl.Instance.currentBattery;
        //load next scene
        transitionAnimator.GetComponent<FadeTransition>().SceneFadeOut(nextScene);
    }

    //set the spawn point of the player
    private void SetSpawn(float x, float y, float z)
    {
        PlayerPrefs.SetFloat("PlayerSpawnX", x);
        PlayerPrefs.SetFloat("PlayerSpawnY", y);
        PlayerPrefs.SetFloat("PlayerSpawnZ", z);
    }
}
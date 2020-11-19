using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string nextScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNextScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "DemoMenu")
        {
            if (nextScene == "Bedroom") SetSpawn(6.34f, 0.31f, 1f);
        }
        if (currentScene == "PlayerHouse")
        {
            if (nextScene == "Bedroom") SetSpawn(-7.97f, -2.38f, 1f);
            if (nextScene == "Town") SetSpawn(-3.44f, -1.24f, 1f);
        }
        if (currentScene == "Bedroom")
        {
            if (nextScene == "PlayerHouse") SetSpawn(-2.039f, 2.418f, 1f);
        }
        if (currentScene == "Town")
        {
            if (nextScene == "PlayerHouse") SetSpawn(-0.53f, -2.04f, 1f);
            if (nextScene == "Library Puzzle 1")
            {
                if (FindObjectOfType<GlobalControl>().hasLibKey) nextScene = "Library Puzzle 2";
                SetSpawn(-2.02f, -4.19f, 1f);
            }
            if (nextScene == "House1") SetSpawn(0.99f, -3.38f, 1f);
        }
        if (currentScene == "Library Puzzle 1" || currentScene == "Library Puzzle 2")
        {
            if (nextScene == "Town") SetSpawn(18.95f, 9.14f, 1f);
            if (nextScene == "Library Puzzle 2") SetSpawn(4.17f, -2.75f, 1f);
            if (nextScene == "Library Puzzle 3") SetSpawn(4.51f, 3.52f, 1f);
        }
        if (currentScene == "House1")
        {
            if (nextScene == "Town") SetSpawn(25.96f, -1.51f, 1f);
            if (nextScene == "Basement1") SetSpawn(9.03f, -1.62f, 1f);
        }
        if (currentScene == "Basement1")
        {
            if (nextScene == "House1") SetSpawn(15.5f, 3.92f, 1f);
        }
        if (currentScene == "Library Puzzle 3")
        {
            if (nextScene == "Library Puzzle 2") SetSpawn(4.17f, -2.75f, 1f);
        }
        SceneManager.LoadScene(nextScene);
    }

    private void SetSpawn(float x, float y, float z)
    {
        PlayerPrefs.SetFloat("PlayerSpawnX", x);
        PlayerPrefs.SetFloat("PlayerSpawnY", y);
        PlayerPrefs.SetFloat("PlayerSpawnZ", z);
    }
}
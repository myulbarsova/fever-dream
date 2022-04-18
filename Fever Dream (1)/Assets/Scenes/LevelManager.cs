using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void loadLevel(string level)
    {
        //Debug.Log("Attempting to load" + level);  // logs a message to the concole
        SceneManager.LoadScene(level);
    }

    public void quitlevel()
    {
        Debug.Log("Attempting to quit level.");
        Application.Quit();
    }
}
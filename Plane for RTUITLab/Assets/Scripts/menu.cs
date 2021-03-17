using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public void lvl1()
    {   
        SceneManager.LoadScene("Game");
        
    }
    public void exitToMenu()
    {Debug.Log("Exit pressed!");
        SceneManager.LoadScene("Menu");

    }
    public void exitGame()
    {
        Application.Quit();
    }
}

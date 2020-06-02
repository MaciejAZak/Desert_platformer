using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuStart : MonoBehaviour
{

    public void StartFirstLevel()
    {
        ResetGameSession();
        SceneManager.LoadScene(1);
    }
    public void BackToMenu()
    {
        ResetGameSession();
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResetGameSession()
    {
        if (FindObjectOfType<GameSession>() != null)
        {
            var gameSession = FindObjectOfType<GameSession>().gameObject;
            Destroy(gameSession);
        }
    }
}

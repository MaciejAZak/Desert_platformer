using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] int PlayerLives = 4;
    [SerializeField] int PlayerScore = 0;
    [SerializeField] float ReloadLevelDelay = 2f;
    [SerializeField] Text LivesText;
    [SerializeField] Text ScoreText;

    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions >1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        LivesText.text = PlayerLives.ToString();
        ScoreText.text = PlayerScore.ToString();
    }

    public void AddToScore(int pointsToAdd)
    {
        PlayerScore += pointsToAdd;
        ScoreText.text = PlayerScore.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if (PlayerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    private void TakeLife()
    {
        PlayerLives--;
        LivesText.text = PlayerLives.ToString();
        StartCoroutine(ReloadCurrentLevel());
    }

    IEnumerator ReloadCurrentLevel()
    {
        yield return new WaitForSecondsRealtime(ReloadLevelDelay);
        var currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevelIndex);
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}

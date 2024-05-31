using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.IO;

public class LogicManager : MonoBehaviour
{
    private int playerScore;
    private int highScore;
    public TextMeshProUGUI highScoreTMP;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverScreen;
    public GameObject bird;
    public AudioSource gameOverSfx;
    public AudioSource addPointSfx;
    public AudioSource bgTheme;
    private SaveAndLoad saveAndLoad;

    public void Awake()
    {
        saveAndLoad = gameObject.AddComponent<SaveAndLoad>();

        try
        {
            highScore = saveAndLoad.Load();
            UpdateHighScoreTMP(highScore);

        }
        catch (Exception)
        {
            saveAndLoad.Save(highScore);
        }
    }

    public void UpdateHighScoreTMP(int highScoreInt)
    {
        highScoreTMP.text = "High Score: " + highScoreInt.ToString();
    }

    [ContextMenu("Increase Score")] // This is a function/method attribute
    public void addScore(int scoreToAdd)
    {
        addPointSfx.Play();
        playerScore = playerScore + scoreToAdd;
        scoreText.text = playerScore.ToString();

        if (playerScore > highScore)
        {
            highScore = playerScore;
            UpdateHighScoreTMP(playerScore);
        }
    }

    public void restartGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void gameOver()
    {
        bgTheme.Stop();
        gameOverSfx.Play();
        gameOverScreen.SetActive(true);

        saveAndLoad.Save(highScore);
    }

    public void despawnBird()
    {
        Destroy(bird);
    }
}

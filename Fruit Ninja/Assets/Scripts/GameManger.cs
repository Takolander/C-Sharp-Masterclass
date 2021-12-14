using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManger : MonoBehaviour
{
    [Header("Score Elements")]
    public int score;
    public int highScore;
    public Text scoreText;
    public Text highScoreText;

    [Header("GameOver")]
    public GameObject gameOverPanel;
    public Text gameOverPanelScoreText;
    public Text gameOverPanelHighScoreText;

    [Header("Sounds")]
    public AudioClip[] sliceSounds;

    private AudioSource audioSource; 

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        gameOverPanel.SetActive(false);
        GetHighScore();
    }

    private void GetHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = "Best: " + highScore.ToString();
    }

    public void IncreaseScore(int points)
    {
        score += points;
        scoreText.text = score.ToString();

        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScoreText.text = "Best: " + score.ToString();
        }
    }

    public void OnBombHit()
    {
        Time.timeScale = 0;

        gameOverPanelScoreText.text = "Score: " + score.ToString();

        gameOverPanelHighScoreText.text = "Highscore: " + highScore.ToString();

        gameOverPanel.SetActive(true);

        Debug.Log("Bomb hit");
    }

    public void RestartGame()
    {
        score = 0;
        scoreText.text = score.ToString();

        gameOverPanel.SetActive(false);

        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Interactable"))
        {
            Destroy(g);
        }

        Time.timeScale = 1;
    }

    public void PlayRandomSliceSound()
    {
        AudioClip randomSound = sliceSounds[Random.Range(0, sliceSounds.Length)];
        audioSource.PlayOneShot(randomSound);
    }
}

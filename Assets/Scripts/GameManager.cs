using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GameObject pausePanel;
    private bool paused = false;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public GameObject titleScreen;

    private int livesValue = 3;
    public TextMeshProUGUI livesText;

    public List<GameObject> targets;
    public bool isGameActive;
    
    private float spawnRate = 1f;
    private int score = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGameActive)
            {
                ChangePaused();
            }
        }
    }

    private IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int rndIndex = Random.Range(0, targets.Count);
            Instantiate(targets[rndIndex]);

        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        isGameActive = false;

        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);

    }


    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {

        score = 0;
        spawnRate /= difficulty;

        paused = false;
        pausePanel.SetActive(false);
        isGameActive = true;

        restartButton.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        titleScreen.SetActive(false);

        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        ApplyDamage(0);
    }


    public void ApplyDamage(int damageValue)
    {
        if (livesValue <= 0)
        {
            GameOver();
        }
        else
        {
            livesValue -= damageValue;
            livesText.text = "Lives: " + livesValue;
        }
    }

    public void ChangePaused()
    {
        if (!paused)
        {
            paused = true;
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            paused = false;
            pausePanel.SetActive(false);
            Time.timeScale = 1;
        }
    }
}

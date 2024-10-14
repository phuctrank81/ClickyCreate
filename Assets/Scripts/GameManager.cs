using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    //public TextMeshProUGUI timeText;
    public GameObject titleScreen;
    private int score;
    public bool isPause = false;
    public GameObject sliderVolume;

    private float spawnRate = 1.0f;
    private int lives = 3;
    private int times = 60;
    public bool isGameActive;
    public Button restartButton;
    public TextMeshProUGUI gameOverText;
    private bool paused;
    public GameObject pauseScreen;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangePaused();
        }

    }

    void ChangePaused()
    {
        if (!paused)
        {
            paused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            paused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }

    IEnumerator SpawnTarget()

    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
            restartButton.gameObject.SetActive(true);
            gameOverText.gameObject.SetActive(true);
            isGameActive = false;    
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        score = 0;
        lives = 3;
        times = 60;
        spawnRate /= difficulty;
        

        StartCoroutine(SpawnTarget());
        
        UpdateScore(0);
        UpdateLives(0);
        titleScreen.gameObject.SetActive(false);
        sliderVolume.gameObject.SetActive(false);
    }

    public void UpdateLives(int livesToDeduct)
    {
        lives -= livesToDeduct;
        livesText.text = "Lives: " + lives;

        if (lives <= 0)
        {
            GameOver();
        }
    }

}



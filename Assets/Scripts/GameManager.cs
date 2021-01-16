using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int lives;
    public int score;
    public int remainingBricks;
    private Text livesText;
    private Text scoreText;
    public int lastLevelPlayed;
    public GameObject gameOverPanel;
    public GameObject youWonPanel;
    public Text lastLevelPlayeds;
    public int currentLevel;
    public Queue<IEnumerator> coroutineQueue = new Queue<IEnumerator>();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CoroutineCoordinator());

        remainingBricks = GameObject.FindGameObjectsWithTag("Bricks").Length;
        livesText = GameObject.Find("LivesText").GetComponent<Text>();
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        currentLevel = SceneManager.GetActiveScene().buildIndex;


        livesText.text = "Lives: " + lives;
        scoreText.text = "Score: " + score;

        LoadPlayer();
        lastLevelPlayeds.text = "Level : " + currentLevel;
    }

    public void updateLives(int changeInLives)
    {
        this.lives += changeInLives;
        livesText.text = "Lives: " + lives;

        if (lives < 0)
        {
            Time.timeScale = 0f;
            gameOverPanel.SetActive(true);
        }

    }
    public void updateScore(int changeScore)
    {
        this.score += changeScore;
        scoreText.text = "Score: " + score;
    }
    public void updateNumberOfBricks(int updateBricks)
    {
        this.remainingBricks += updateBricks;
        if (remainingBricks <= 0)
        {
            Time.timeScale = 0f;

            if (currentLevel >= lastLevelPlayed)
            {
                lastLevelPlayed = currentLevel + 1;
            }
            SaveSystem.saveGame(this);

            youWonPanel.SetActive(true);
        }
    }

    public void SaveGame()
    {
       
        SaveSystem.saveGame(this);
    }

    public void LoadPlayer()
    {
        GameData data = SaveSystem.LoadGame();

        lives = data.lives;
        score = data.score;
        lastLevelPlayed = data.level;

        livesText.text = "Lives: " + lives;
        scoreText.text = "Score: " + score;
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        PauseMenu.isPaused = false;
        if(this.lives < 0)
        {
            this.lives = 3;
            this.score = 0;
            this.lastLevelPlayed = 1;
        }
        SaveSystem.saveGame(this);
        SceneManager.LoadScene(0);
    }

    public void restartLostLevel()
    {
        lives = 3;
        score = 0;
        SaveSystem.saveGame(this);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
        gameOverPanel.SetActive(false);
    }

    public void loadNextLevel()
    {
        Time.timeScale = 1f;
        youWonPanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }

    IEnumerator CoroutineCoordinator()
    {
        while (true)
        {
            while (coroutineQueue.Count > 0)
                yield return StartCoroutine(coroutineQueue.Dequeue());
            yield return null;
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameData
{
    public int level;
    public int lives;
    public int score;

    public GameData(GameManager gameManager)
    {
        level = gameManager.lastLevelPlayed;
        lives = gameManager.lives;
        score = gameManager.score;
    }

    public GameData()
    {

    }
  
}

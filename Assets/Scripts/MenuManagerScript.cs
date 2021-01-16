using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManagerScript : MonoBehaviour
{
    public GameObject continueButtom;
    public GameObject mainPanel;
    public GameObject areYouSurePanel;
    public GameObject creditsPanel;
    public Text playButtom;

    public void Start()
    {
        string path = Application.persistentDataPath + "/player.txt";
        if (!File.Exists(path))
        {
            continueButtom.SetActive(false);
        }
        else
        {
            playButtom.text = "New Game";
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit button pushed");
    }

    public void PlayGame()
    {
        string path = Application.persistentDataPath + "/player.txt";
        if (File.Exists(path)) {
            mainPanel.SetActive(false);
            areYouSurePanel.SetActive(true);
        }
        else
        {
            GameData newGame = new GameData();
            newGame.lives = 3;
            newGame.score = 0;
            newGame.level = 1;

            SaveSystem.saveGame(newGame);

            SceneManager.LoadScene(newGame.level);
        }
    }

    public void ContinueGame()
    {
        GameData data = SaveSystem.LoadGame();
        SceneManager.LoadScene(data.level);
    }

    public void areYouSureYES()
    {

        GameData newGame = new GameData();
        newGame.lives = 3;
        newGame.score = 0;
        newGame.level = 1;

        SaveSystem.saveGame(newGame);

        SceneManager.LoadScene(newGame.level);

    }

    public void areYouSureNO()
    {
        mainPanel.SetActive(true);
        areYouSurePanel.SetActive(false);
    }

    public void CreditsButtom()
    {
        mainPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void BackCreditsButtom()
    {
        creditsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }
}

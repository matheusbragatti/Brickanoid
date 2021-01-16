using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingMenuScript : MonoBehaviour
{
    public GameObject endingPanel;
    public GameObject creditsPanel;



    public void NewGameButtom()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitToMenuButtom()
    {
        SceneManager.LoadScene(0);
    }
    public void QuitToDesktopButtom()
    {
        Application.Quit();
        Debug.Log("Quit button pushed");
    }
    public void CreditsButtom()
    {
        endingPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void BackCreditsButtom()
    {
        creditsPanel.SetActive(false);
        endingPanel.SetActive(true);
    }

}

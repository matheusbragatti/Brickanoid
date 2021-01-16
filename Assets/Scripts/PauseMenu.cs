using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public static bool isPaused = false;
    public GameObject menuObject;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                resume();
            }
            else
            {
                pause();
            }
        }

    }

    public void resume()
    {
        menuObject.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void pause()
    {
        menuObject.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

    }
}

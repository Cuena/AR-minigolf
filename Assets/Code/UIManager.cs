using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject pauseMenu, mainUI, endMenu;
    public Text scoreBox;

    void Awake()
    {
       if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void pauseGame()
    {
        mainUI.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        mainUI.SetActive(true);
        Time.timeScale = 1;
    }

    public void endGame(int strokes)
    {
        Time.timeScale = 0;
        mainUI.SetActive(false);
        endMenu.SetActive(true);
        scoreBox.text = strokes.ToString();    
    }
}

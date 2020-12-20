using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{

    public static LevelSelector instance;

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
    public void levelOneLoader()
    {
        
        SceneManager.LoadScene("TourScene");
    }
    public void builderLoader()
    {
        SceneManager.LoadScene("AllInOne");
    }
    public void scoreLoader()
    {
        SceneManager.LoadScene("Ranking");
    }
    public void goHome()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("LevelSelection");
    }
}

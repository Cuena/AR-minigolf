using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
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

    }
    public void goHome()
    {
        SceneManager.LoadScene("LevelSelection");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    private GameObject game;
    private GameObject builder;
    public GameObject ball;
   

    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.Find("pref_Golfball_02 Variant");
        game = GameObject.Find("CanvasGame");
        builder = GameObject.Find("CanvasBuilder");
        game.SetActive(false);
        ball.SetActive(false);
    }

    public void toGame()
    {
        game.SetActive(true);
        ball.SetActive(true);
        GameObject start = GameObject.Find("pref_TrackBeachStart(Clone)");
        builder.SetActive(false);
        

    }

    public void toBuilder()
    {
        game.SetActive(false);
        ball.SetActive(false);
        builder.SetActive(true);
      
    }

    public void toSaver()
    {
        game.SetActive(false);
        ball.SetActive(false);
        builder.SetActive(false);
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

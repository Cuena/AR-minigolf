using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;

    private int currentLevel;
    public GameObject ballPrefab;
    public GameObject[] levelDatas;

    public ball b;
    

    public GameObject[] GameObjects { get { return levelDatas; } }

    private int shotCount = 0;

    
    void Awake()
    {
        if (instance ==null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentLevel = 0;
        levelDatas[currentLevel].SetActive(true);
        b.setReference(levelDatas[currentLevel].transform.GetChild(0));


    }

    public void SpawnLevel(int levelIndex)
    {
        Instantiate(levelDatas[levelIndex], Vector3.zero, Quaternion.identity);
        shotCount = 0;

        b.setReference(levelDatas[levelIndex].transform);
        GameObject ball = Instantiate(ballPrefab, Vector3.zero, Quaternion.identity);
    }

    public void nextLevel()
    {   
        levelDatas[currentLevel].SetActive(false);
        currentLevel++;
        levelDatas[currentLevel].SetActive(true);
        b.setReference(levelDatas[currentLevel].transform.GetChild(0));
        b.Restart0();
    }

    public void shotTaken()
    {
        if (shotCount < 100)
        {
            shotCount++;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

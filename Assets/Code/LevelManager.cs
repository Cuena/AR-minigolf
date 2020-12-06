using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;

    private GameObject ballPrefab;
    private GameObject[] levelDatas;

    public GameObject[] GameObjects { get { return levelDatas; } }

    private int shotCount = 0;

    // Start is called before the first frame update
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

    public void SpawnLevel(int levelIndex)
    {
        Instantiate(levelDatas[levelIndex], Vector3.zero, Quaternion.identity);
        shotCount = 0;

        GameObject ball = Instantiate(ballPrefab, Vector3.zero, Quaternion.identity);
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

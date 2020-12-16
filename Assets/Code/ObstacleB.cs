using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleB : MonoBehaviour
{
    public bool dirRight = true;
    public float speed = 2.0f;
    public Transform reference;
    


    void Update()
    {
        if (dirRight)
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        else
            transform.Translate(-Vector2.right * speed * Time.deltaTime);

        if (transform.position.x >= reference.position.x + 0.5f)
        {
            dirRight = false;
        }

        if (transform.position.x <= reference.position.x)
        {
            dirRight = true;
        }

        
    }
}

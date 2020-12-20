using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerB : MonoBehaviour
{
    public static AudioManagerB instance;
    public static AudioClip hitSound, bounceSound;
    static AudioSource audioSrc;


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

    void Start()
    {
        hitSound = Resources.Load<AudioClip>("hit");
        bounceSound = Resources.Load<AudioClip>("hole");
        audioSrc = GetComponent<AudioSource>();  
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "hit":
                audioSrc.PlayOneShot(hitSound);
                break;
            case "hole":
                audioSrc.PlayOneShot(bounceSound);
                break;

        }
    }
}

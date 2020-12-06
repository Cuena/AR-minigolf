using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;

public class PowerBar : MonoBehaviour
{
    private Power power;
    private Image powerImage;
    public GameObject ball;

    private bool isPressed = false;
    // Start is called before the first frame update
    void Awake()
    {
        powerImage = transform.Find("Bar").GetComponent<Image>();
        power = new Power();

        
    }
    // Update is called once per frame
    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("hit"))
        {
            Debug.Log("pressed");
            isPressed = true;    
        }
        if (CrossPlatformInputManager.GetButtonUp("hit"))
        {
            Debug.Log("released");
            isPressed = false;
            power.Reset();
            powerImage.fillAmount = power.GetPowerNormalized();
           
        }
        
        if (isPressed)
        {
            power.Fill();
            powerImage.fillAmount = power.GetPowerNormalized();
        }
       
    }

   
}

//public class Power
//{
//    public const int POWER_MAX = 100;
//    private float powerAmount;
//    private float fillSpeed;

//    public Power()
//    {
//        powerAmount = 0;
//        fillSpeed = 120f;
//    }

//    public void Fill()
//    {
//        powerAmount += fillSpeed * Time.deltaTime;
//    }

//    public float GetPowerNormalized()
//    {
//        return powerAmount / POWER_MAX;
//    }

//    public void Reset()
//    {
//        powerAmount = 0;
//    }

//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
public class ball : MonoBehaviour
{

    private Rigidbody golfball;
    public float maxForce, multForce;
    public float rotationV = 30, rotationSpeed = 110;
    public MeshRenderer arrowRenderer;
    public Transform reference;
    private Power power;
    public Image powerImage;
    private bool isPressed = false;
    private bool isMoving = false;
    public ParticleSystem holeEffect;
    public Text strokesT;
    private int nStrokes;
    //private IEnumerator coroutine;

    public float force = 1;
    
    void Awake()
    {
        golfball = GetComponent<Rigidbody>();
        power = new Power();
        
        nStrokes = 0;
        strokesT.text = nStrokes.ToString();

    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        this.Restart0();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (golfball.transform.position.y < -1)
        {
            this.Restart0();
        }
        float velX = CrossPlatformInputManager.GetAxis("Horizontal");
        Rotate(velX);
        if (!isMoving)
        {
            if (CrossPlatformInputManager.GetButtonDown("hit"))
            {
                //Debug.Log("pressed");
                isPressed = true;
            }
            if (CrossPlatformInputManager.GetButtonUp("hit"))
            {
                //Debug.Log("released");
                isPressed = false;
                Hit(power.GetPowerNormalized());
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

    void FixedUpdate() 
    { 
        if (golfball.velocity.magnitude < 0.1f) golfball.velocity = Vector3.zero; 
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "hole")
        {
            Debug.Log("HOOOOLE");
            //nStrokes = 0;
            strokesT.text = nStrokes.ToString();
            AudioManagerB.PlaySound("hole");
            holeEffect.Play();
            LevelManager.instance.nextLevel();
        }

    }

    public void Hit(float f)
    {
        AudioManagerB.PlaySound("hit");
        golfball.AddForce(transform.forward * f, ForceMode.Impulse);
        nStrokes++;
        strokesT.text = nStrokes.ToString();
        StartCoroutine(go());
    }
   
    //void Move(float value)
    //{
    //    golfball.MovePosition(transform.position + transform.forward * Time.deltaTime * 50 * value);
    //}



    public void Restart()
    {
        //StopCoroutine(coroutine);
        golfball.velocity = Vector3.zero;
        golfball.angularVelocity = Vector3.zero;
        golfball.transform.rotation = Quaternion.identity;
        arrowRenderer.enabled = true;
        isMoving = false;
    }

    public void Restart0()
    {
        this.Restart();
        transform.position = reference.position + new Vector3(0, 0.3f, 0.5f);
    }

    public void setReference(Transform t)
    {
        this.reference = t;
    }

    void Rotate(float value)
    {
        golfball.MoveRotation(Quaternion.Euler(transform.rotation.eulerAngles + Vector3.up * Time.deltaTime * rotationSpeed * value));
    }
    IEnumerator go()
    {
        isMoving = true;
        arrowRenderer.enabled = false;
        yield return new WaitForSeconds(1);
        while (golfball.velocity.magnitude > 0.15f)
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        transform.position = transform.position + new Vector3(0, 0.04f, 0);
        Restart();
       

    }

   
}

public class Power
{
    public const int POWER_MAX = 100;
    private float powerAmount;
    private float fillSpeed;

    public Power()
    {
        powerAmount = 0;
        fillSpeed = 120f;
    }

    public void Fill()
    {
        powerAmount += fillSpeed * Time.deltaTime;
    }

    public float GetPowerNormalized()
    {
        return powerAmount / POWER_MAX;
    }

    public void Reset()
    {
        powerAmount = 0;
    }

}


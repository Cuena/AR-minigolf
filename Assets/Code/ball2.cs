using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
public class ball2 : MonoBehaviour
{

    private Rigidbody golfball;
    
    public float rotationV = 30, rotationSpeed = 110;
    public MeshRenderer arrowRenderer;
    public Transform reference;
    
    private powerer powerer;
    
    public Image powererImage;
    
    private bool isPressed = false;
    private bool isMoving = false;
    
    public ParticleSystem holeEffect;
    public Text strokesT;
    
    private int nStrokes;
    //private IEnumerator coroutine;

    public float force = 1;

    public void setReference(Transform transform)
    {
        reference = transform;
    }

    void Awake()
    {
        golfball = GetComponent<Rigidbody>();
        powerer = new powerer();

        nStrokes = 0;
        strokesT.text = nStrokes.ToString();

    }

    private void Start()
    {
        Application.targetFrameRate = 60;

    }

    // Update is called once per frame
    void Update()
    {
        if (golfball.transform.position.y < -1)
        {
            this.Restart();
        }
        float velX = CrossPlatformInputManager.GetAxis("Horizontal");
        Rotate(velX);
        if (!isMoving)
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
                Hit(powerer.GetpowererNormalized());
                powerer.Reset();

                powererImage.fillAmount = powerer.GetpowererNormalized();

            }

            if (isPressed)
            {
                powerer.Fill();
                powererImage.fillAmount = powerer.GetpowererNormalized();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "hole")
        {
            Debug.Log("HOOOOLE");
            nStrokes = 0;
            strokesT.text = nStrokes.ToString();
            holeEffect.Play();
            GameObject.Find("Start").GetComponent<CanvasManager>().toBuilder();
        }
    }

    public void Hit(float f)
    {
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
        transform.position = reference.position + new Vector3(0, 0.3f, 0.05f);
        arrowRenderer.enabled = true;
        isMoving = false;
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
        golfball.velocity = Vector3.zero;
        golfball.angularVelocity = Vector3.zero;
        golfball.transform.rotation = Quaternion.identity;
        arrowRenderer.enabled = true;
        isMoving = false;

    }


}

public class powerer
{
    public const int powerer_MAX = 100;
    private float powererAmount;
    private float fillSpeed;

    public powerer()
    {
        powererAmount = 0;
        fillSpeed = 120f;
    }

    public void Fill()
    {
        powererAmount += fillSpeed * Time.deltaTime;
    }

    public float GetpowererNormalized()
    {
        return powererAmount / powerer_MAX;
    }

    public void Reset()
    {
        powererAmount = 0;
    }

}


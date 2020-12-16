using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BuilderManager : MonoBehaviour
{
    public Sprite[] images;
    public GameObject[] prefavs;
    public GameObject image;

    public GameObject up;
    public GameObject down;
    public GameObject rotate;
    private GameObject start;

    public Transform target;

    private int count;
    private GameObject selectedPrefav;
    private int numChildren;

    private bool imageMode;

    // Start is called before the first frame update
    void Start()
    {
        numChildren = 2;
        count = 0;
        start = GameObject.Find("Start");
        imageMode = true;
    }

    public void check()
    {
        if (imageMode)
        {
            selectedPrefav = Instantiate(prefavs[count], target.position, target.rotation, target);
            selectedPrefav.GetComponent<Transform>().localScale = new Vector3(0.14f, 0.1f, 0.17f);
            numChildren++;
            changeMode();
        }
        else
        {
            changeMode();
        }
    }

    public void nextImage()
    {
        if (imageMode)
        {
            if (GameObject.FindGameObjectsWithTag("start").Length == 0 || GameObject.FindGameObjectsWithTag("hole").Length == 0)
            {
                if (GameObject.FindGameObjectsWithTag("start").Length == 0)
                {
                    if (GameObject.FindGameObjectsWithTag("hole").Length == 0)
                    {
                        if (count < 6) count++;
                    }
                    else if (count < 5) count++;
                }
                else if (count == 4)
                {
                    count += 2;
                }
                else if (count < 6) count++;


            }
            else if (count < 4) count++;

            Debug.Log(count);
            setImage(images[count]);
        }
        else
        {
            selectedPrefav.transform.position += transform.right * Time.deltaTime;

        }
        
    }

    public void atras()
    {
        selectedPrefav.transform.position += -1 * transform.forward * Time.deltaTime;

    }

    public void delante()
    {
        selectedPrefav.transform.position += transform.forward * Time.deltaTime;
    }

    public void giroOne()
    {
        selectedPrefav.transform.Rotate(new Vector3(0,30,0));
    }

    public void backImage()
    {
        if (imageMode)
        {
            if (GameObject.FindGameObjectsWithTag("start").Length == 0)
            {
                if (count > 0) count--; 
            }
            else if (count == 6)
            {
                count -= 2;
            }
            else if (count < 6) count--;
            Debug.Log(count);
            setImage(images[count]);
        }
        else
        {
            selectedPrefav.transform.position += transform.right * Time.deltaTime * -1;

        }
    }
    public void crox()
    {
        if (imageMode)
        {
            if (numChildren > 2)
            {
                Destroy(start.transform.GetChild(numChildren - 1).gameObject);
                numChildren--;
            }
        }
        else
        {

            Destroy(selectedPrefav);
            changeMode();
        }
    }

    public void changeMode()
    {
        if (imageMode)
        {
            image.SetActive(false);
            up.SetActive(true);
            down.SetActive(true);
            rotate.SetActive(true);
            imageMode = false;
        }
        else
        {
            count = 0;
            setImage(images[count]);
            image.SetActive(true);
            up.SetActive(false);
            down.SetActive(false);
            rotate.SetActive(false);
            imageMode = true;
        }
    }

    public void home()
    {
        SceneManager.LoadScene("LevelSelection");
    }

    void setImage(Sprite sprite)
    {
        image.GetComponent<Image>().sprite = sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

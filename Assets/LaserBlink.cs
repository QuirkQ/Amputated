using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBlink : MonoBehaviour
{
    public float onOffTimer;
    public GameObject laser;
    public GameObject laserOn;
    public GameObject laserGoingOn;

    // Start is called before the first frame update
    void Start()
    {
        Off();
    }

    void Off()
    {
        
        laser.gameObject.SetActive(false);

        laserOn.gameObject.SetActive(false);

        laserGoingOn.gameObject.SetActive(false);
        GetComponent<SpriteRenderer>().enabled = true;
        Invoke("On", onOffTimer);
        Invoke("GoingOn", onOffTimer - .6f);
    }

    void GoingOn()
    {
        laser.gameObject.SetActive(false);
        laserOn.gameObject.SetActive(false);
        GetComponent<SpriteRenderer>().enabled = false;
        laserGoingOn.gameObject.SetActive(true);
    }

    void On()
    {
        laser.gameObject.SetActive(true);
        laserOn.gameObject.SetActive(true);
        laserGoingOn.gameObject.SetActive(false);
        GetComponent<SpriteRenderer>().enabled = false;
        Invoke("Off", onOffTimer);
    }
}

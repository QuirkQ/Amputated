using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBlink : MonoBehaviour
{
    public float onOffTimer;

    // Start is called before the first frame update
    void Start()
    {
        Off();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Off()
    {
        var laser = gameObject.transform.GetChild(0);
        laser.gameObject.SetActive(false);
        var laserOn = gameObject.transform.GetChild(1);
        laserOn.gameObject.SetActive(false);
        GetComponent<SpriteRenderer>().enabled = true;
        Invoke("On", onOffTimer);
    }
    void On()
    {
        var laser = gameObject.transform.GetChild(0);
        laser.gameObject.SetActive(true);
        var laserOn = gameObject.transform.GetChild(1);
        laserOn.gameObject.SetActive(true);
        GetComponent<SpriteRenderer>().enabled = false;
        Invoke("Off", onOffTimer);
    }
}

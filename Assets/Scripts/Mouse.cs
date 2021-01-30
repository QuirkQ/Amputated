using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    public GameObject player;
    public bool isSticked;
    public float minScale;
    public float maxScale;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, transform.position.y);

        //transform.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        transform.position = player.transform.position;
        Vector2 mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 thisPos = new Vector2(transform.position.x, transform.position.y);
        transform.right = mousePos - thisPos;
        if (isSticked && transform.localScale.x <= maxScale)
        {
            Debug.Log("Is Active " + transform.localScale.x);
            transform.localScale = new Vector3(transform.localScale.x + Time.deltaTime * speed, transform.localScale.y, transform.localScale.z);
        }
        else if (!isSticked && transform.localScale.x >= minScale)
        {
            Debug.Log("Is Deactive " + transform.localScale.x);
            transform.localScale = new Vector3(transform.localScale.x - Time.deltaTime * speed, transform.localScale.y, transform.localScale.z);
        }
    }
}

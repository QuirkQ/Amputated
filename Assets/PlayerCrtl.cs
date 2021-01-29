using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrtl : MonoBehaviour
{
    public bool grounded;
    public bool inverted;
    public Rigidbody2D rb2d;
    public float bounceStrength;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (grounded)
        {
            Vector2 thisPos = transform.position;
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (inverted)
            {
                mousePosition.y = mousePosition.y * -1;
            }
            Vector2 direction = mousePosition - thisPos;
            transform.right = direction;
            rb2d.velocity = transform.right * bounceStrength;
        }
    }
}

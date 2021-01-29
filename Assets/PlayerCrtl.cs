using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrtl : MonoBehaviour
{
    public bool grounded;
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
            
            Vector2 position = new Vector2(transform.position.x, transform.position.y);
            Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 direction = position + mousePosition;
            Vector2 newvector = (direction * bounceStrength) * -1/10;
            rb2d.velocity = newvector;
            Debug.Log("Mouse Pos: " + mousePosition + " Direction: " + direction + " New Vector: " + newvector);
        }
    }
}

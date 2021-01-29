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
            /*
            Vector2 position = new Vector2(transform.position.x, transform.position.y);
            Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 direction = position + mousePosition;
            Vector2 newvector = (direction * bounceStrength) /10;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, mousePosition, 1f);
            Debug.DrawRay(transform.position, Vector2.right, Color.green, 1f);
            Vector2 rayLoc = hit.point;
            rb2d.velocity = rayLoc *-1;
            Debug.Log(hit.point);
            //Debug.Log("Mouse Pos: " + mousePosition + " Direction: " + direction + " New Vector: " + newvector);*/
            Vector2 thisPos = transform.position;
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (mousePosition.y < 0)
            {
                mousePosition.y = mousePosition.y * -1;
            }
            Vector2 direction = mousePosition - thisPos;
            transform.right = direction;
            rb2d.velocity = transform.right * bounceStrength;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrtl : MonoBehaviour
{
    public Mouse mouse;

    public bool grounded;
    public bool inverted;
    public Rigidbody2D rb2d;
    public float bounceStrength;
    public LayerMask ground;
    public GameObject playerObj;
    public Transform right;
    public Transform left;
    public Transform up;
    public Transform down;
    public Animation animation;
    public bool isBouncing;

    private float timer;
    private int counter;
    private Vector2 bouncePos;
    private Vector2 bounceNormal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    /*
    void Update()
    {
        playerObj.transform.position = transform.position;
        if (grounded)
        {
            Vector2 rightv2 = new Vector2(right.position.x, right.position.y);
            Vector2 leftv2 = new Vector2(left.position.x, left.position.y);
            Vector2 upv2 = new Vector2(up.position.x, up.position.y);
            Vector2 downv2 = new Vector2(down.position.x, down.position.y);

            Vector2 thisPos = transform.position;
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePosition - thisPos;
            RaycastHit2D hitRight = Physics2D.Raycast(transform.position, rightv2, .6f, ground);
            if (hitRight.collider != null)
            {
                if (mousePosition.x > thisPos.x)
                {
                    direction.x = direction.x * -1;
                }
            }
            RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, leftv2, .6f, ground);
            if (hitLeft.collider != null)
            {
                if (mousePosition.x < thisPos.x)
                {
                    direction.x = direction.x * -1;
                }
            }
            RaycastHit2D hitUp = Physics2D.Raycast(transform.position, upv2, .6f, ground);
            if (hitUp.collider != null)
            {
                if (mousePosition.y < thisPos.y)
                {
                    direction.y = direction.y * -1;
                }
            }
            RaycastHit2D hitDown = Physics2D.Raycast(transform.position, downv2, .6f, ground);
            if (hitDown.collider != null)
            {
                if (mousePosition.y > thisPos.y)
                {
                    direction.y = direction.y * -1;
                }
            }
            Debug.Log(direction);
            
            transform.right = direction*-1f;
            rb2d.velocity = transform.right * bounceStrength;
        }
    }*/
    void Update()
    {
        /*
        Debug.DrawRay(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position, Color.green);
        playerObj.transform.position = transform.position;

        if (grounded && counter < 1)
        {
            Vector2 rightv2 = new Vector2(right.position.x, right.position.y);
            Vector2 leftv2 = new Vector2(left.position.x, left.position.y);
            Vector2 upv2 = new Vector2(up.position.x, up.position.y);
            Vector2 downv2 = new Vector2(down.position.x, down.position.y);
            Vector2 posPlayer = new Vector2(transform.position.x,transform.position.y);

            RaycastHit2D hitRight = Physics2D.Raycast(transform.position, rightv2 - posPlayer, 1, ground);
            if (hitRight.collider != null)
            {
                Debug.DrawRay(transform.position, rightv2 - posPlayer, Color.blue, 1f);
            }
            RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, leftv2 - posPlayer, 1, ground);
            if (hitLeft.collider != null)
            {
                Debug.DrawRay(transform.position, leftv2 - posPlayer, Color.blue, 1f);
            }
            RaycastHit2D hitUp = Physics2D.Raycast(transform.position, upv2 - posPlayer, 1, ground);
            if (hitUp.collider != null)
            {
                Debug.DrawRay(transform.position, upv2 - posPlayer, Color.blue, 1f);
            }
            RaycastHit2D hitDown = Physics2D.Raycast(transform.position, downv2-posPlayer, 1, ground);
            if (hitDown.collider != null)
            {
                Debug.DrawRay(transform.position, downv2 - posPlayer, Color.blue, 1f);
            }
            timer = .1f;
            counter = 1;
        } else if(!grounded)
        {
            counter = 0;
        }
        if (timer >= .1f)
        {
            var pos = mouse.position;
            var dir = pos - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Debug.DrawRay(transform.position, transform.right, Color.red, 1f);
            rb2d.AddForce(transform.right * bounceStrength);
            timer = 0f;
        }*/


    }

    public void BounceBall(Vector2 pos, Vector2 normal)
    {
        mouse.isSticked = true;
        isBouncing = true;
        Debug.DrawRay(pos, normal, Color.red, 1f);
        bouncePos = pos;
        bounceNormal = normal;
        var angle = Mathf.Atan2(normal.y, normal.x) * Mathf.Rad2Deg;
        //Debug.Log("normal Angle: " + angle);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        animation.Play();
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        Invoke("ApplyForce", 0.1f);
        
    }
    void ApplyForce()
    {
        rb2d.constraints = RigidbodyConstraints2D.None;
        var angle = Mathf.Atan2(bounceNormal.y, bounceNormal.x) * Mathf.Rad2Deg;
        var posi = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var dir = posi - transform.position;
        var mouseAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Debug.DrawRay(bouncePos, dir, Color.blue, 1f);
        var angleDif = mouseAngle - angle;
        if (angleDif < -180) angleDif += 360;
        if (angleDif >= 180) angleDif -= 360;
        angleDif = Mathf.Clamp(angleDif, -70, 70);
        var bounceAngle = angle + angleDif;
        Vector2 bounceDir = new Vector2(Mathf.Cos(bounceAngle * Mathf.Deg2Rad), Mathf.Sin(bounceAngle * Mathf.Deg2Rad));
        Debug.DrawRay(bouncePos, bounceDir, Color.green, 1f);
        rb2d.AddForce(bounceDir * bounceStrength);
        isBouncing = false;
        mouse.isSticked = false;
    }
}

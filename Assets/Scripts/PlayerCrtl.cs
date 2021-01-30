using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrtl : MonoBehaviour
{
    public bool dead;
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

    void Update()
    {
        Debug.Log(dead);
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
        Debug.DrawRay(bouncePos, dir, Color.blue, 1f); //debug
        var angleDif = mouseAngle - angle;
        if (angleDif < -180) angleDif += 360;
        if (angleDif >= 180) angleDif -= 360;
        angleDif = Mathf.Clamp(angleDif, -70, 70);
        var bounceAngle = angle + angleDif;
        Vector2 bounceDir = new Vector2(Mathf.Cos(bounceAngle * Mathf.Deg2Rad), Mathf.Sin(bounceAngle * Mathf.Deg2Rad));
        Debug.DrawRay(bouncePos, bounceDir, Color.green, 1f); // debug
        rb2d.AddForce(bounceDir * bounceStrength);
        isBouncing = false;
        mouse.isSticked = false;
    }
}

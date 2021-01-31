using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScientistPatrol : MonoBehaviour
{
    public float speed;
    public float distance;
    public LayerMask ground;
    public Transform midPoint;

    private float x;
    private float y;
    private Vector2 newPosV2;
    private float scaleX;
    void Start()
    {
        newPos();
        scaleX = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Vector2.Distance(transform.position, newPosV2) > distance))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 2f, ground);
            Debug.DrawRay(transform.position, Vector2.right, Color.green);
            if (hit.collider != null)
            {

                newPos();
                Turn();
            }
            transform.position = Vector2.MoveTowards(transform.position, newPosV2, Time.deltaTime * speed);

        }
        else
        {
            newPos();
            Turn();
        }
    }

    void newPos()
    {
        x = Random.Range(-10f, 10f) + transform.position.x;
        y = transform.position.y;
        newPosV2 = new Vector2(x, y);
    }
    void midPos()
    {
        newPosV2 = new Vector2(midPoint.position.x, midPoint.position.y);
    }
    void Turn()
    {
        if (transform.position.x > newPosV2.x)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Debug.Log("hit wall");
            gameObject.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            ParticleSystem.EmissionModule em = gameObject.transform.GetChild(0).GetComponent<ParticleSystem>().emission;
            em.enabled = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrtl : MonoBehaviour
{
    public bool dead;
    public Mouse mouse;
    public List<GameObject> spawnedList = new List<GameObject>();
    public List<Animation> animationList = new List<Animation>();
    public bool grounded;
    public bool inverted;
    public Rigidbody2D rb2d;
    public float bounceStrength;
    public LayerMask ground;
    public GameObject playerObj;
    public Animation animation;
    public Animation legAnimation;
    public bool isBouncing;
    public GameObject LegPref;
    public CircleCollider2D circleCollider;
    public Transform lookAtObject;

    private Vector2 bouncePos;
    private Vector2 bounceNormal;
    private int legsInt;
    private float rotateLegs;
    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        this.CheckIfDead();
        

    }
    /*private void FixedUpdate()
    {
        if (legsInt > 6)
        {
            var legs = gameObject.transform.GetChild(0);
            legs.transform.right = lookAtObject.position - transform.position;
        }
    }*/

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
        //PlayAllAnimations("LegLandAni");
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
        //PlayAllAnimations("LegJumpAni");
        isBouncing = false;
        mouse.isSticked = false;
    }
    void PlayAllAnimations(string animationName)
    {
        if (legsInt > 0)
        {
            for (int i = 0; i <= spawnedList.Count; i++)
            {  
                int test = spawnedList.Count - 1;
                legAnimation = spawnedList[test].GetComponent<Animation>();
                legAnimation.GetComponent<Animation>().clip = legAnimation.GetComponent<Animation>().GetClip(animationName);
                legAnimation.GetComponent<Animation>().Play();
                //animationList[test].clip = animationList[test].GetClip(animationName);
                //animationList[test].Play();
                Debug.Log(i);
            }
            
        }

    }

    void CheckIfDead()
    {
        if (dead)
        {
            Application.LoadLevel(Application.loadedLevel);
        }
	}
	
    public void AddLeg()
    {
        legsInt += 1;
        if (legsInt == 1)
        {
            Vector2 leg1Pos = transform.position;
            Quaternion leg1Rot = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - 100);
            var clone = Instantiate(LegPref, leg1Pos, leg1Rot, gameObject.transform.GetChild(0));
            circleCollider.radius = 1.08f;
            spawnedList.Add(clone);
            animationList.Add(clone.GetComponent<Animation>());
            bounceStrength = bounceStrength * 1.5f;
        } else if (legsInt == 2)
        {
            Vector2 leg2Pos = transform.position;
            Quaternion rotleg2 = Quaternion.Euler(spawnedList[0].transform.rotation.eulerAngles.x, spawnedList[0].transform.rotation.eulerAngles.y, spawnedList[0].transform.rotation.eulerAngles.z + 20);
            var clone = Instantiate(LegPref, leg2Pos, rotleg2, gameObject.transform.GetChild(0));
            clone.transform.localScale = new Vector3(clone.transform.localScale.x * -1, clone.transform.localScale.y, clone.transform.localScale.z);
            spawnedList.Add(clone);
            animationList.Add(clone.GetComponent<Animation>());
            bounceStrength = bounceStrength * 1.2f;
        } else if (legsInt > 2 && legsInt < 10)
        {
            rotateLegs += 68;
            int i = 1;
            
            Vector2 legsPos = transform.position;
            Quaternion legsRot = Quaternion.Euler(0, 0, rotateLegs);
            var clone = Instantiate(LegPref, legsPos, legsRot, gameObject.transform.GetChild(0));
            //Debug.Log("Legs rotate: " + rotateLegs + " New Leg Rotation: " + legsRot);
            clone.transform.localScale = new Vector3(clone.transform.localScale.x * i, clone.transform.localScale.y, clone.transform.localScale.z);
            spawnedList.Add(clone);
            animationList.Add(clone.GetComponent<Animation>());
            bounceStrength = bounceStrength * 1.1f;
        } else if (legsInt >= 10)
        {
            //Debug.Log("HIEERRR");
            int i = Random.Range(-1, 2);
            while (i == 0)
            {
                i = Random.Range(-1, 2);
            }
            int t = 0;
            while (t == 0)
            {
                int childnumber = Random.Range(1, gameObject.transform.GetChild(0).childCount);
                Transform randomChild = gameObject.transform.GetChild(0).GetChild(childnumber);
                Transform randomChildChild = randomChild.transform.GetChild(0);
                if (randomChildChild.childCount < 2)
                {

                    Transform randomChildChildChild = randomChildChild.transform.GetChild(0);
                    Transform randomChildChildChildChild = randomChildChildChild.transform.GetChild(0);
                    Vector3 legsPos = new Vector3(randomChildChildChildChild.position.x, randomChildChildChildChild.position.y, randomChildChildChildChild.position.z + 1);
                    Quaternion legsRot = Quaternion.Euler(randomChild.rotation.eulerAngles.x, randomChild.rotation.eulerAngles.y, randomChild.rotation.eulerAngles.z);
                    //Debug.Log(randomChild.rotation.eulerAngles.z);
                    //Debug.Log(spawnedList.IndexOf(randomChild.gameObject));
                    var clone = Instantiate(LegPref, legsPos, legsRot, randomChildChild);
                    //Debug.Log("Legs rotate: " + rotateLegs + " New Leg Rotation: " + legsRot);
                    clone.transform.localScale = new Vector3(clone.transform.localScale.x * .5f * i, clone.transform.localScale.y * .5f, clone.transform.localScale.z * .5f); ;
                    //Debug.Log(randomChild + clone);
                    spawnedList.Add(clone);
                    animationList.Add(clone.GetComponent<Animation>());
                    t = 1;
                }
            }
            
           
        }
    }
}

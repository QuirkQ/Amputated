using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrtl : MonoBehaviour
{
    public bool dead;
    public CameraShake cameraShake;
    public Mouse mouse;
    public GroundCheck groundCheck;
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
    
    public AudioSource audioSource;
    public AudioSource audioSource2;
    public AudioClip slimeBounce1;
    public AudioClip slimeBounce2;
    public AudioClip bigSlimeBounce1;
    public AudioClip bigSlimeBounce2;
    public AudioClip deathSound;

    private float secondsBetweenBounce;
    private Vector2 bouncePos;
    private Vector2 bounceNormal;
    private int legsInt;
    private float rotateLegs;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        secondsBetweenBounce = .2f;
    }

    void Update()
    {


        if (groundCheck.isStillColliding && !isBouncing)
        {
            timer += Time.deltaTime;
            if (timer >= .5f)
            {
                CheckGroundAgain();
                timer = 0;
            }
        }
        else if (timer > 0)
        {
            timer = 0;
        }
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
        if (legsInt > 0)
        {
            cameraShake.Shake();
        }

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
        Invoke("ApplyForce", secondsBetweenBounce);
        
            int i = Random.Range(0, 2);
            if (i == 0)
            {
                audioSource.clip = bigSlimeBounce1;
                audioSource.Play();
                audioSource2.clip = slimeBounce1;
                audioSource2.Play();
            }
            else
            {
                audioSource.clip = bigSlimeBounce2;
                audioSource.Play();
                audioSource2.clip = slimeBounce1;
                audioSource2.Play();
            }
        

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
        //Debug.Log(bounceDir * bounceStrength);
        rb2d.AddForce(bounceDir * bounceStrength);
        //PlayAllAnimations("LegJumpAni");
        isBouncing = false;
        mouse.isSticked = false;
    }
    void CheckGroundAgain()
    {
        if (groundCheck.isStillColliding)
        {
            groundCheck.CollideAgain();
        }
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

    public void Kill()
    {
        
            audioSource2.clip = deathSound;
            audioSource2.Play();
            Application.LoadLevel(Application.loadedLevel);
            
	}

    void IncreaseCameraShake()
    {
        if (cameraShake.Amount.x <= 1.2f)
        {
            cameraShake.Amount = cameraShake.Amount * 1.3f;
        }

    }
    public void AddAllLegs()
    {
        for (int i = 0; i <= legsInt; i++)
        {
            AddLeg();
        }
    }

	
    public void AddLeg()
    {
        legsInt += 1;
        audioSource.volume += .015f;

        if (legsInt <= 17)
        {
            Debug.Log("Leg Added " + legsInt);
            if (legsInt == 1)
            {
                Vector2 leg1Pos = transform.position;
                Quaternion leg1Rot = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - 100);
                var clone = Instantiate(LegPref, leg1Pos, leg1Rot, gameObject.transform.GetChild(0));
                circleCollider.radius = 1.08f;
                spawnedList.Add(clone);
                animationList.Add(clone.GetComponent<Animation>());
                bounceStrength = bounceStrength * 1.5f;
                secondsBetweenBounce = 0.1f;
                IncreaseCameraShake();
            }
            else if (legsInt == 2)
            {
                Vector2 leg2Pos = transform.position;
                Quaternion rotleg2 = Quaternion.Euler(spawnedList[0].transform.rotation.eulerAngles.x, spawnedList[0].transform.rotation.eulerAngles.y, spawnedList[0].transform.rotation.eulerAngles.z + 20);
                var clone = Instantiate(LegPref, leg2Pos, rotleg2, gameObject.transform.GetChild(0));
                clone.transform.localScale = new Vector3(clone.transform.localScale.x * -1, clone.transform.localScale.y, clone.transform.localScale.z);
                spawnedList.Add(clone);
                animationList.Add(clone.GetComponent<Animation>());
                bounceStrength = bounceStrength * 1.2f;
                IncreaseCameraShake();
            }
            else if (legsInt > 2 && legsInt < 10)
            {
                int i = 1;
                Quaternion legsRot = Quaternion.Euler(spawnedList[legsInt - 2].transform.rotation.eulerAngles.x, spawnedList[legsInt - 2].transform.rotation.eulerAngles.y, spawnedList[legsInt - 2].transform.rotation.eulerAngles.z + 60);
                Vector2 legsPos = transform.position;
                var clone = Instantiate(LegPref, legsPos, legsRot, gameObject.transform.GetChild(0));
                //Debug.Log("Legs rotate: " + rotateLegs + " New Leg Rotation: " + legsRot);
                clone.transform.localScale = new Vector3(clone.transform.localScale.x * i, clone.transform.localScale.y, clone.transform.localScale.z);
                spawnedList.Add(clone);
                animationList.Add(clone.GetComponent<Animation>());
                bounceStrength = bounceStrength * 1.1f;
                IncreaseCameraShake();
            }
            else if (legsInt >= 10)
            {
                int t = 0;
                while (t == 0)
                {
                    int childnumber = Random.Range(1, gameObject.transform.GetChild(0).childCount);
                    Transform randomChild = gameObject.transform.GetChild(0).GetChild(childnumber);
                    Transform randomChildChild = randomChild.transform.GetChild(0);
                    if (randomChild.childCount < 2)
                    {

                        Transform randomChildChildChild = randomChildChild.transform.GetChild(0);
                        Transform randomChildChildChildChild = randomChildChildChild.transform.GetChild(0);
                        Vector3 legsPos = new Vector3(randomChildChildChildChild.position.x, randomChildChildChildChild.position.y, randomChildChildChildChild.position.z + 1);
                        Quaternion legsRot = Quaternion.Euler(randomChild.rotation.eulerAngles.x, randomChild.rotation.eulerAngles.y, randomChild.rotation.eulerAngles.z);
                        //Debug.Log(randomChild.rotation.eulerAngles.z);
                        //Debug.Log(spawnedList.IndexOf(randomChild.gameObject));
                        var clone = Instantiate(LegPref, legsPos, legsRot, randomChild);
                        //Debug.Log("Legs rotate: " + rotateLegs + " New Leg Rotation: " + legsRot);
                        clone.transform.localScale = new Vector3(clone.transform.localScale.x * .7f, clone.transform.localScale.y * .7f, clone.transform.localScale.z * .7f); ;
                        //Debug.Log(randomChild + clone);
                        spawnedList.Add(clone);
                        animationList.Add(clone.GetComponent<Animation>());
                        t = 1;
                        IncreaseCameraShake();
                    }
                }


            }
        }
        
    }
}

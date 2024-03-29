using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquachScript : MonoBehaviour
{
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float playerRot = player.rotation.z;


        if (player.rotation.z >= 180)
        {
            float playerRotDif = player.rotation.z - 180;
            playerRot = player.rotation.z - playerRotDif;
            transform.localScale = new Vector3(transform.localScale.x *-1, transform.localScale.y, transform.localScale.z);
        }
        this.transform.rotation = Quaternion.Euler(player.rotation.x, player.rotation.y, playerRot);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegPickup : MonoBehaviour
{
    public PlayerCrtl playerCtrl;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Debug.Log("HIIIIII");
            playerCtrl.AddLeg();
            gameObject.SetActive(false);
        }
    }
}

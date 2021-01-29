using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public PlayerCrtl playerCtrl;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerCtrl.grounded = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        playerCtrl.grounded = false;
    }
}

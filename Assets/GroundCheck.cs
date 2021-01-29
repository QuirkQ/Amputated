using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public PlayerCrtl playerCtrl;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 closestPoint = collision.bounds.ClosestPoint(playerCtrl.rb2d.position);
        if (playerCtrl.rb2d.position.x > closestPoint.x)
        {
            playerCtrl.inverted = true;
        } else if (playerCtrl.rb2d.position.y > closestPoint.y) {
            playerCtrl.inverted = true;
        }
        playerCtrl.grounded = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        playerCtrl.grounded = false;
        playerCtrl.inverted = false;
    }
}

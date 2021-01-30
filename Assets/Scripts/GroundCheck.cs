using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public PlayerCrtl playerCtrl;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int collidedBodyTag = collision.gameObject.layer;
        if (!playerCtrl.isBouncing && collidedBodyTag == 7)
        {
            ContactPoint2D[] contactPoints = new ContactPoint2D[1];
            collision.GetContacts(contactPoints);
            playerCtrl.BounceBall(contactPoints[0].point, contactPoints[0].normal);
            playerCtrl.grounded = true;
        } else if (collidedBodyTag == 8)
        {
            playerCtrl.dead = true;
        }
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        playerCtrl.grounded = false;
        playerCtrl.inverted = false;
    }
}

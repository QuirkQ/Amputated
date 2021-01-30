using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public PlayerCrtl playerCtrl;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!playerCtrl.isBouncing)
        {
            ContactPoint2D[] contactPoints = new ContactPoint2D[1];
            collision.GetContacts(contactPoints);
            playerCtrl.BounceBall(contactPoints[0].point, contactPoints[0].normal);

            /*Vector3 closestPoint = collision.bounds.ClosestPoint(playerCtrl.rb2d.position);
            if (playerCtrl.rb2d.position.x > closestPoint.x)
            {
                playerCtrl.inverted = true;
            }
            else if (playerCtrl.rb2d.position.y > closestPoint.y)
            {
                playerCtrl.inverted = true;
            }*/
            playerCtrl.grounded = true;
        }
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        playerCtrl.grounded = false;
        playerCtrl.inverted = false;
    }
}

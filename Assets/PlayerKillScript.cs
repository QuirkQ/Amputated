using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKillScript : MonoBehaviour
{
    public PlayerCrtl playerCrtl;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            playerCrtl.Kill();
        }
    }
}

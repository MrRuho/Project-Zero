using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

    private Player player;
   
    //Note6. Tunnistaa sen koskettaako pelaaja maata vai ei. 
    private void Start()
    {
        player = gameObject.GetComponentInParent<Player>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TurretRangeCone")) //Estaa pelaajaa tekemasta tuplahyppya turretin rangecolliderin sisalla.
        {
            player.grounded = false;
        }
        else
        player.grounded = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("TurretRangeCone")) //Estaa pelaajaa tekemasta tuplahyppya turretin rangecolliderin sisalla.
        {
            player.grounded = false;
        }
        else
            player.grounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.grounded = false;
    }
}
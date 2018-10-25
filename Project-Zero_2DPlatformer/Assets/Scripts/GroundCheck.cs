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

    private void OnTriggerEnter2D(Collider2D collision)
    {
<<<<<<< HEAD
        if (collision.isTrigger == false)  //Estaa pelaajaa tekemasta tuplahyppya turretin rangecolliderin sisalla.

            player.grounded = true;
=======
        /*if (collision.CompareTag("TurretRangeCone")) // Lista Tageista joita taman objektin kuuluu ignoorata. (Ilman tata, luokittelee ne maaksi mahdollistaa esim tuplahypyn.)
        {
            player.grounded = false;
        }
        else */
        player.grounded = true;
>>>>>>> 7696f349259250598af8e1c8aec5a4bb32e5f037
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
<<<<<<< HEAD
        if (collision.isTrigger == false)
     
=======

       /* if (collision.CompareTag("TurretRangeCone"))
        {
            player.grounded = false;
        }
        else */
>>>>>>> 7696f349259250598af8e1c8aec5a4bb32e5f037
            player.grounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.grounded = false;
    }
}
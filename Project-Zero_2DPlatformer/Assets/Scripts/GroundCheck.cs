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

    void OnTriggerEnter2D(Collider2D col)
    {
        player.grounded = true;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        player.grounded = false;
    }

}

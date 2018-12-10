using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour {
   
    public int powerUp = 0; // Mikä ase Player_Weapons.cs valitaan.

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player_Weapons.weapon = powerUp;
            Destroy(gameObject);
        }
    }
}

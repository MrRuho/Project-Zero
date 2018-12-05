using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour {
    public int currenPowerUp = 0;
    public static int powerUp = 0; // 
	// Use this for initialization
	void Start ()
    {
        powerUp = currenPowerUp;
	}
	
	// Update is called once per frame
	void Update ()
    {
        

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player_Weapons.weapon = powerUp;
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed = 50f;
    public float jumpPower = 150f;

    public bool grounded;

    private Rigidbody2D rb2d;

	// Use this for initialization
	void Start ()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>(); // Note 1. Luodaan Pelaajaan kiinnitetty Rigidbody2D "rb2d", jotta sita voidaan manipuloida. (esim. Kun hahmoa liikutetaan.)
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal"); // Note 2. h = Oikea ja vasen nuoli, Oletus nappaimet A ja D.

        rb2d.AddForce((Vector2.right * speed) * h); //Note 3. H on oletuksena -1 ja 1. jolloin A ja D nappia painettaessa hahmo liikkuu vasemmalle tai oikealle. 
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float maxSpeed = 3;
    public float speed = 50f;
    public float jumpPower = 150f;
    public float DoubleJumpPower = 200f;

    public bool grounded;
    public bool CanDoubleJump;

    public int curHealth;
    public int maxHealth = 100;

    private bool HasJumped;
    private Rigidbody2D rb2d;
    private Animator anim;

	// Use this for initialization
	void Start ()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>(); // Note 1. Luodaan Pelaajaan kiinnitetty Rigidbody2D "rb2d", jotta sita voidaan manipuloida. (esim. Kun hahmoa liikutetaan.)
        anim = gameObject.GetComponent<Animator>(); // Note 5. Luodaan pelaajaan kiinnitetty Animator, jolla voidaan manipuloida animaatioita.

        curHealth = maxHealth;
	}

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Grounded", grounded);
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));


        if (Input.GetAxis("Horizontal") < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetAxis("Horizontal") > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        // ------------------------- double jump Start ----------------------
        if (Input.GetButtonDown("Jump"))
        {
            if (grounded)
            {
                {
                    HasJumped = false;
                }
                {
                    rb2d.AddForce(Vector2.up * jumpPower);
                    CanDoubleJump = true;
                }
            }
            else
            {
                if (CanDoubleJump && HasJumped == false)
                {
                    CanDoubleJump = false;
                    rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                    rb2d.AddForce(Vector2.up * DoubleJumpPower);
                    HasJumped = true;
                }

            }


        } // -----------------------double jump End -------------------------

        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }
        if (curHealth <= 0)
        {
            Die();
        }
    }

    private void FixedUpdate()
    {
        // create fake friction /Easing the X speed of our player. Player not slide.
        Vector3 easeVelocity = rb2d.velocity;
        easeVelocity.y = rb2d.velocity.y;
        easeVelocity.z = 0.0f;
        easeVelocity.x *= 0.75f;

        
        if(grounded)
        {
            rb2d.velocity = easeVelocity;

        }
        //------------------------------------------------
        float h = Input.GetAxis("Horizontal"); // Note 2. h = Oikea ja vasen nuoli, Oletus nappaimet A ja D.

        rb2d.AddForce((Vector2.right * speed) * h); //Note 3. H on oletuksena -1 ja 1. jolloin A ja D nappia painettaessa hahmo liikkuu vasemmalle tai oikealle.

        //note 4. Rajoittaa pelaajan maksimi nopeutta.
        if (rb2d.velocity.x > maxSpeed)
        {
            rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
        }
        if (rb2d.velocity.x < -maxSpeed)
        {
            rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
        }

    }

    void Die()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bullet : MonoBehaviour {
    public float lifeTime = 20f;
    public float speed = 20f;
    public int damage = 40;
    public Rigidbody2D rb2d;
    public GameObject impactEffect;
    public GameObject blood;

    Player_Bullet bullet;
 
    // Use this for initialization
    void Start ()
    {
        rb2d.velocity = transform.right * speed;
        StartCoroutine(travelTime());
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            if (enemy != null)
            {
                Instantiate(impactEffect, transform.position, transform.rotation);
                Instantiate(blood, transform.position, transform.rotation); // NOTE. veriroiskahduksen lopullinen kulma asennetaan BloodSplachConrol.cs:ssa.
                enemy.TakeDamage(damage);
            }
        }

        if (collision.gameObject.tag != "Bullet" || collision.gameObject.tag != "Player")
        {
            
            Instantiate(impactEffect, transform.position, transform.rotation);
            // HUOM! Objecti tuhotaan palapalalta, jotta partikkeli efekti ei tuhoutuisi mukana. 
            Destroy(GetComponent<Rigidbody>());
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(GetComponent<CircleCollider2D>());
            Destroy(GetComponent<SpriteRenderer>());
            Destroy(gameObject, 1);
        }
    }

  

    /* void OnBecameInvisible()
     {
         Debug.Log("Bullet become invisible and get destroyed");
         Destroy(GetComponent<Rigidbody>());
         Destroy(GetComponent<BoxCollider2D>());
         Destroy(GetComponent<CircleCollider2D>());
         Destroy(GetComponent<SpriteRenderer>());
         Destroy(gameObject,1);


     }*/
    IEnumerator travelTime() {
        yield return new WaitForSeconds(lifeTime);
        Destroy(GetComponent<Rigidbody>());
        Destroy(GetComponent<BoxCollider2D>());
        Destroy(GetComponent<CircleCollider2D>());
        Destroy(GetComponent<SpriteRenderer>());
        Destroy(gameObject);
        yield return 0;
    }
}

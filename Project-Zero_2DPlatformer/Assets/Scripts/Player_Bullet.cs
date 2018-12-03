using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bullet : MonoBehaviour {

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
        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(GetComponent<Rigidbody>());
        Destroy(GetComponent<BoxCollider2D>());
        Destroy(GetComponent<SpriteRenderer>());
        Destroy(gameObject, 1);
    }

    void OnBecameInvisible()
    {
        Destroy(GetComponent<Rigidbody>());
        Destroy(GetComponent<BoxCollider2D>());
        Destroy(GetComponent<SpriteRenderer>());
        Destroy(gameObject, 1);
    }
}

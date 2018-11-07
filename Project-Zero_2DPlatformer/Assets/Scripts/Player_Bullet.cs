using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bullet : MonoBehaviour {

    public float speed = 20f;
    public int damage = 40;
    public Rigidbody2D rb2d;
    public GameObject impactEffect;
    public GameObject blood;

	// Use this for initialization
	void Start ()
    {
        rb2d.velocity = transform.right * speed;
	}

    void OnTriggerEnter2D( Collider2D collision)
    {
        if (collision.isTrigger == false)
        {
            Enemy enemy = collision.GetComponent<Enemy>();

            if (enemy != null)
            {
                Instantiate(blood, transform.position, transform.rotation); // NOTE. veriroiskahduksen lopullinen kulma asennetaan BloodSplachConrol.cs:ssa.
                enemy.TakeDamage(damage);  
            }
            Destroy(gameObject);
            Instantiate(impactEffect, transform.position, transform.rotation);
            
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }


}

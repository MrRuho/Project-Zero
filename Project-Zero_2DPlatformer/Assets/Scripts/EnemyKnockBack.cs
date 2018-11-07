using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockBack : MonoBehaviour {

    public Vector2 customVelocity;

    public GameObject bouncer;
    Animator anim;
    Vector2 velocity;
    EnemyPatrol enemyPatrol;

    // Use this for initialization
    void Start()
    {
        enemyPatrol.gameObject.GetComponent<EnemyPatrol>();
        velocity = customVelocity;
        anim = gameObject.GetComponentInParent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Bullet")
        {
            Debug.Log("Bullet hit");
            enemyPatrol.speed = -5f;
            bouncer.GetComponent<Rigidbody2D>().velocity = velocity;
        }
    }
}

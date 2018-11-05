using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHorizontalPowerPunch : MonoBehaviour {


    public Vector2 customVelocity;

    public GameObject bouncer;
    Animator anim;
    Vector2 velocity;

    Tranpoline powerpunch;

    // Use this for initialization
    void Start()
    {
        velocity = customVelocity;
        anim = gameObject.GetComponentInParent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            anim.SetBool("MushroomAttack", true);
            other.gameObject.GetComponent<Player>().speed = velocity.x;
            bouncer.GetComponent<Rigidbody2D>().velocity = velocity;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            other.gameObject.GetComponent<Player>().playerCanDieIfHitsWall = true;
        }
    }

}


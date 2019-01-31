using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

    private Player player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            player.Damage(5);
        }

        if (collision.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallKill : MonoBehaviour {

    private float timer = 0f;
    private Player player;
    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        { 
            timer += Time.deltaTime;  
        }
        if (timer >= 0.5f)
        {
            player.Damage(5);  
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            timer = 0f;
        }
    }
}

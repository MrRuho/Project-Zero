using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallKill : MonoBehaviour {

    private float timer = 0f;
    public float killTime = 0.5f;
    
    private Player player;
    public BoxCollider2D boxCollider;

    // Use this for initialization
    void Start ()
    {
        
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        
    }

    private void Update()
    {
        if (Player.sliding == true)
        {
            boxCollider.offset = new Vector3(0.1f, 0.0f, 0);
        }
        else
        {
            boxCollider.offset = new Vector3(0.1f, 0.30f, 0);
        }

        if (timer >= killTime)
        {
            Debug.Log(timer);
            player.Damage(5);
        }
    }



    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            timer += Time.deltaTime;
        }
      
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            timer = 0f;
        }
    }

}

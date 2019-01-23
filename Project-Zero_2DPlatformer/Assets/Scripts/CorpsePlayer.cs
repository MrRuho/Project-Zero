using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpsePlayer : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private float force = 800.0f;
        // Start is called before the first frame update
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        rb2d.AddForce(Vector2.right * force);
        Physics2D.IgnoreLayerCollision(12, 11);
        Physics2D.IgnoreLayerCollision(12, 0);
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    void OnCollisionEnter(Collision collide)
    {
        //Output the name of the GameObject you collide with
        Debug.Log("I hit the GameObject : " + collide.gameObject.name);
    }
}

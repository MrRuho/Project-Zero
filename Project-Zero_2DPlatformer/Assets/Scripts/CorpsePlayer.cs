using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpsePlayer : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float force = Player.deadSpeed;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("deadSpeed: " + force);
        if (Player.deadSpeed < -5)
        {
            force = Player.deadSpeed * 40;
        }
        else if (Player.deadSpeed < 2)
        {
            force = Player.deadSpeed;
        }
        else if(Player.deadSpeed > 2)
        {    
            force = Player.deadSpeed * 70;
        }
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        rb2d.AddForce(Vector2.right * force);
        Physics2D.IgnoreLayerCollision(12, 11);
        Physics2D.IgnoreLayerCollision(12, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.dead == false)
        {
            Destroy(GameObject.FindGameObjectWithTag("DeadPlayerCameraPoint"));
        }
    }
}

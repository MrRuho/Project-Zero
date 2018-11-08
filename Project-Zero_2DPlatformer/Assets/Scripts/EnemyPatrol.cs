using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour {

    public float speed;

    private bool movingRight = false;
    private bool getHit = false;
    public Transform groundDetection;
    public Transform wallDetection;

    void Update()
    {
        

        if (getHit == false)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.right * 0 * Time.deltaTime);
        }

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position,Vector2.down, 0.1f);
        RaycastHit2D wallInfo = Physics2D.Raycast(wallDetection.position, Vector2.zero, 0.1f);
        if (groundInfo.collider == false | wallInfo.collider == true)
        {
            if (movingRight == false)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = true; 
            } else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = false;
            }
        }
    
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            getHit = true;
            StartCoroutine(hasBeenHit());
        }
    }

    IEnumerator hasBeenHit()
    {
        yield return new WaitForSeconds(1);
        getHit = false;
    }
}

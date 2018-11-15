using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour {

    public float speed;

    private bool movingRight = false;
    private bool getHit = false;
    public Transform groundDetection;
    public Transform wallDetection;
    public Transform meleeAttackZone;

 
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
     
        Debug.DrawRay(transform.position, Vector2.down, Color.white);
        Debug.DrawRay(transform.position, Vector2.zero, Color.yellow);
        
        if (getHit == false)
        {
            AttackDirectionControl();
            if (groundInfo.collider == false | wallInfo.collider == true)
            {
               
                if (movingRight == false)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    movingRight = true;
                    
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    movingRight = false;
                    
                }
            }
        }
    }

    void AttackDirectionControl()
    {
        if (movingRight == true)
        {
            RaycastHit2D meleeInfo = Physics2D.Raycast(meleeAttackZone.position, Vector2.right, 0.5f);
            Debug.DrawRay(transform.position, Vector2.right, Color.red);         
        }
        else
        {
            RaycastHit2D meleeInfo = Physics2D.Raycast(meleeAttackZone.position, Vector2.left, 0.5f);
            Debug.DrawRay(transform.position, Vector2.left, Color.red);
       
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            getHit = true;
            StartCoroutine(HasBeenHit());
        }
    }

    IEnumerator HasBeenHit()
    {
        yield return new WaitForSeconds(1);
        getHit = false;
    }
}

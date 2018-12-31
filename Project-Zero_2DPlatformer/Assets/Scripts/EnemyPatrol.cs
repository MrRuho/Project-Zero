using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : Enemy {

    public float speed;
    private bool movingRight = false;
    private bool getHit = false;
    private bool grounded = false;
    private int currentHealth = 1;

    private Player player;

    public Transform groundDetection;
    public Transform wallDetection;
    public Transform meleeAttackZone;

    Animator anim;
    Enemy enemy;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        anim = gameObject.GetComponent<Animator>();
        currentHealth = enemy.health;
    }

    void Update()
    {
        int layerMask = 1 << 1;
        layerMask = ~layerMask; //wall Raycast ei huomio pelaajaa.

        if (getHit == false)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.right * 0 * Time.deltaTime);
        }

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position,Vector2.down, 0.1f);
        RaycastHit2D wallInfo = Physics2D.Raycast(wallDetection.position, Vector2.zero, 0.1f, layerMask);

       

        Debug.DrawRay(transform.position, Vector2.down, Color.white);
        Debug.DrawRay(transform.position, Vector2.zero, Color.yellow);
        
        if (getHit == false && grounded)
        {   
            AttackDirectionControl();
            if (groundInfo.collider == false | wallInfo.collider == true )
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
        RaycastHit2D PlayerDetectorRight = Physics2D.Raycast(meleeAttackZone.position, Vector2.right, 5.0f);
        RaycastHit2D PlayerDetectorLeft = Physics2D.Raycast(meleeAttackZone.position, Vector2.left, 5.0f);

        if (getHit == false) {

            if (movingRight == true) {
               
                Debug.DrawRay(transform.position, Vector2.right, Color.red);

                if (PlayerDetectorRight.collider != null) {

                    if (PlayerDetectorRight.collider.gameObject.tag == "Player"){

                        speed = -4;
                    }
                } else {
                    speed = -1;
                }
            }

            if (movingRight == false) {
                
                Debug.DrawRay(transform.position, Vector2.left, Color.red);

                if (PlayerDetectorLeft.collider != null) {

                    if (PlayerDetectorLeft.collider.gameObject.tag == "Player") {

                        speed = -4;
                    }
                } else {

                    speed = -1;
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.Damage(5);
            getHit = true;
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(GetComponent<Rigidbody2D>());
            ZombieDyingAnim();
        }

        if (collision.gameObject.tag == "Bullet")
        {
            getHit = true;
            StartCoroutine(HasBeenHit());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground")) {

            grounded = true;

        } else {

            grounded = false;

        }
    }

    IEnumerator HasBeenHit()
    {
        yield return new WaitForSeconds(1);
        if (currentHealth >= 0)
        {
            getHit = false;
        }
        else
        {
            getHit = true;
        }
    }

    void ZombieDyingAnim()
    {
        anim.SetBool("Dying", true);
    }
}

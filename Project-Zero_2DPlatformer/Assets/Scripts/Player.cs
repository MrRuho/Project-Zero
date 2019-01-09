﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public float maxSpeed = 3;
    public float speed = 10f;
    public float minFloatingSpeed = 1f; // Maksimi nopeus johon pelaajan nopeus tippuu hiljalleen kun pelaaja on ilmassa.
    public float jumpPower = 150f;
    public float doubleJumpPower = 200f;
    float orginalSpeed;
    public int curHealth;
    public int maxHealth = 100;
    public bool grounded;
    public bool canDoubleJump;
    public bool wallSliding;
    public bool facingRight = true;
    public bool playerCanDieIfHitsWall = false;
    public bool wallCheck;
    public static bool dead = false; // Kohteet jotka tarvitsevat tätä tietoa. CameraFollow.cs

    private float jumpPowerOrginal;
    private bool hasJumped;
    public static bool sliding = false; // Kohteet jotka tarvitsevat tätä tietoa. WallKill.cs
    private bool timeToBoost = false;

    //References
    public Transform zombieSpawn;
    public GameObject spawnZombie;
    public GameObject blood;
    public Transform wallCheckPoint;
    public LayerMask wallLayerMask;
    
    private CapsuleCollider2D capsuleCollider2D;
    private Rigidbody2D rb2d;
    private Animator anim;
    private GameMaster gameMaster;
    // Use this for initialization
    void Start ()
    {
        dead = false;
        orginalSpeed = speed;
       // speed = orginalSpeed;
        jumpPowerOrginal = jumpPower; //Pelaajan hyppyvoima palutuu normaaliksi pelaajan osuessa maahan. Esim sieni popistaa hyppyvoiman sinkoessaan pelaajan ilmaan.
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();

        //maarittaa capsulecolliderin koon heron ollessa pystyasenossa.
        capsuleCollider2D = gameObject.GetComponent<CapsuleCollider2D>();
        if (capsuleCollider2D != null)
        {
            capsuleCollider2D.size = new Vector3(0.55f, 1.2f, 0);
            capsuleCollider2D.offset = new Vector3(0.08f, -0.04f, 0);
            capsuleCollider2D.direction = CapsuleDirection2D.Vertical;
        }

        curHealth = maxHealth;
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
	}

    // Update is called once per frame
    void Update()
    {
        
        anim.SetBool("Grounded", grounded);
        anim.SetFloat("Speed", Mathf.Abs(speed));
  
        //--- Kyykky tai liuku. Start. Muuttaa capsuleCollider2D kokoa ja suuntaa.---
        if (Input.GetKeyDown("x") && grounded && !sliding && !dead)
        {
            sliding = true;
            capsuleCollider2D.size = new Vector3(0.9f, 0.5f, 0);
            capsuleCollider2D.offset = new Vector3(0, -0.19f, 0);
            capsuleCollider2D.direction = CapsuleDirection2D.Horizontal;
            anim.SetBool("Slide", true);
            // Slide animaation loputtua Animation kaynnistaa SlideEnds() eventin joka palauttaa capsulecolliderin normaaliksi.
        }

        //---- kyyky tai liuku. end ---

      /*  if (Input.GetAxis("Horizontal") < -0.1f)
        {       
            transform.localScale = new Vector3(-1, 1, 1);
            facingRight = false;
        }

        if (Input.GetAxis("Horizontal") > 0.1f)
        {  
            transform.localScale = new Vector3(1, 1, 1);
            facingRight = true;
        }*/
        // ------------------------- double jump Start ----------------------
        if (Input.GetButtonDown("Jump")&& !wallSliding && !sliding && !dead)
        {
            if (grounded)
            {
                jumpPower = jumpPowerOrginal; //Pelaajan hyppyvoima palutuu normaaliksi pelaajan osuessa maahan. Esim sieni popistaa hyppyvoiman sinkoessaan pelaajan ilmaan.
                {
                    hasJumped = false;
                }
                {
                    rb2d.AddForce(Vector2.up * jumpPower);
                    canDoubleJump = true;  
                }
            }
            else
            {
                if (canDoubleJump && hasJumped == false)
                {
                    canDoubleJump = false;
                    rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                    rb2d.AddForce(Vector2.up * doubleJumpPower);
                    hasJumped = true;
                }
            }   
        } // -----------------------double jump End -------------------------

        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }
        if (curHealth <= 0 && dead == false)
        {
            Die();
        }
        // ---------------------- wall climping jumping start ----------------------------
         if (!grounded)
         {
            wallCheck = Physics2D.OverlapCircle(wallCheckPoint.position, 0.1f, wallLayerMask);

            if (facingRight && Input.GetAxis("Horizontal")> 0.1f || !facingRight && Input.GetAxis("Horizontal") < 0.1f)
             {
                 if (wallCheck)
                 {
                     HandleWallSliding();
                 }
             }
         }

         if (wallCheck == false || grounded)
        {
            wallSliding = false;
        }
     
    }

    void HandleWallSliding()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, -0.7f);
        wallSliding = true;

        if (Input.GetButtonDown("Jump"))
        {
            if (facingRight)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                rb2d.AddForce(new Vector2(-1, 2) * jumpPower);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
                rb2d.AddForce(new Vector2(1, 2) * jumpPower);
            }
        }
    }
    //---------------------- wall climping jumping end ----------------------------

    // Slide animaation loputtua Animation kaynnistaa taman eventin.
    void SlideEnds()
    {
        capsuleCollider2D.size = new Vector3(0.55f, 1.2f, 0);
        capsuleCollider2D.offset = new Vector3(0.08f, -0.04f, 0);
        capsuleCollider2D.direction = CapsuleDirection2D.Vertical;
        anim.SetBool("Slide", false);
        sliding = false;
    }

    private void FixedUpdate()
    {
        
        if (playerCanDieIfHitsWall == false && grounded && !dead)
        {
            if (speed <= orginalSpeed  && playerCanDieIfHitsWall == false && timeToBoost == true)
            {
                StartCoroutine(BoostSpeed());
                timeToBoost = false;
            }

            transform.Translate(speed * Time.deltaTime, 0, 0); // Liikuttaa pelajaa oikealle.
        }

        if (playerCanDieIfHitsWall == false && !grounded && !dead)
        {
            StartCoroutine(LoseSpeed());
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }

        if (playerCanDieIfHitsWall == true && !dead) // Lyo pelaajan vasemmalle esim."EnemyHorizontalPowerPunch.cs" aktivoi vaman ja asettaa nopeudeksi -20.
        {           
            transform.Translate(speed * Time.deltaTime, 0.1f, 0);
              
        }
        // create fake friction /Easing the X speed of our player. Player not slide and stops moving immediately
        Vector3 easeVelocity = rb2d.velocity;
        easeVelocity.y = rb2d.velocity.y;
        easeVelocity.z = 0.0f;
        easeVelocity.x *= 0.75f;
        float h = Input.GetAxis("Horizontal");

        if (grounded)
        {
            rb2d.velocity = easeVelocity;
        }
        //------------------------------------------------
        if (grounded)
        {
            rb2d.AddForce((Vector2.right * speed) * h);
        }
        else
        {
            rb2d.AddForce((Vector2.right * speed / 2) * h);
        }

        //Rajoittaa pelaajan maksimi nopeutta.
        if (rb2d.velocity.x > maxSpeed)
        {
            rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
        }
        if (rb2d.velocity.x < -maxSpeed)
        {
            rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
        }    
    }

    IEnumerator LoseSpeed()
    {    
            for (float i = minFloatingSpeed; i <= speed && !grounded;)
            {
            yield return new WaitForSeconds(0.01f);
            speed -= 0.001f;
            
            }
        
        yield return timeToBoost = true;
    }

    IEnumerator BoostSpeed()
    {
        for (float i = orginalSpeed; i >= speed;)
        {
            speed += 0.1f;
            yield return new WaitForSeconds(0.01f);
        }
        yield return 0;
    }

    void Die()
    {       //  Huome! HighScore ominaisuutta ei ole asennettu peliin nakyvaksi.
        if (PlayerPrefs.HasKey("HighScore"))
        {
            if(PlayerPrefs.GetInt("HighScore")< gameMaster.points)
            {
                PlayerPrefs.SetInt("HighScore", gameMaster.points);
            }
            else
            {
                PlayerPrefs.SetInt("HighScore", gameMaster.points);
            }
        }
        //------------------------- HighScore end---------------------------------------
        gameObject.tag = "Enemy";
        speed = 0;
        dead = true;
        rb2d.constraints = RigidbodyConstraints2D.None;
        Destroy(GameObject.FindGameObjectWithTag("CameraPoint"));
        Debug.Log("Paye is dead");
        Destroy(GetComponent<BoxCollider2D>());
        Destroy(GetComponent<CapsuleCollider2D>());
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(gameObject);
        playerSpawnPoint.spawNewSoldier = true;
        Instantiate(spawnZombie, zombieSpawn.position, zombieSpawn.rotation);
        // StartCoroutine(NextSoldier());

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //lataa koko kentän alusta.
    }

    IEnumerator NextSoldier()
    {
        Instantiate(spawnZombie, zombieSpawn.position, zombieSpawn.rotation);
        yield return 0;
    }

    // Vahingon aiheuttajilla on paasy tahan.
    public void Damage(int dmg)
    {
        curHealth -= dmg;
        gameObject.GetComponent<Animation>().Play("Player_RedFlash");   
    }

    // Aktivoituu pelajaan saadessa vahinkoa.
    public IEnumerator KnockBack(float knockDur, float knockBackPwr, Vector3 knockBackDir)
    {
        float timer = 0;
        rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
        while (knockDur > timer)
        {
            timer += Time.deltaTime;
            rb2d.velocity = new Vector2(0, 0);
            rb2d.AddForce(new Vector3(knockBackDir.x * -100, knockBackDir.y * knockBackPwr, transform.position.z));
        }
        yield return 0;
    }
    //--------------------------------------------
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            gameMaster.points += 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {      
        if (playerCanDieIfHitsWall)
        {
            curHealth = 0;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public float maxSpeed = 3;
    public float speed = 50f;
    public float jumpPower = 150f;
    public float doubleJumpPower = 200f;
    public int curHealth;
    public int maxHealth = 100;
    public bool grounded;
    public bool canDoubleJump;
    public bool wallSliding;
    public bool facingRight = true;
    public bool playerCanDieIfHitsWall = false;
    public bool wallCheck;

    private float jumpPowerOrginal;
    private bool hasJumped;
    private bool sliding = false;
    //References
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
        jumpPowerOrginal = jumpPower; //Pelaajan hyppyvoima palutuu normaaliksi pelaajan osuessa maahan. Esim sieni popistaa hyppyvoiman sinkoessaan pelaajan ilmaan.
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();

        //maarittaa capsulecolliderin koon heron ollessa pystyasenossa.
        capsuleCollider2D = gameObject.GetComponent<CapsuleCollider2D>();
        if (capsuleCollider2D != null)
        {
            capsuleCollider2D.size = new Vector3(0.58f, 0.9f, 0);
            capsuleCollider2D.offset = new Vector3(0, 0, 0);
            capsuleCollider2D.direction = CapsuleDirection2D.Vertical;
        }

        curHealth = maxHealth;
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
	}

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Grounded", grounded);
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));

        //--- Kyykky tai liuku. Start. Muuttaa capsuleCollider2D kokoa ja suuntaa.---
        if (Input.GetKeyDown("z") && grounded && !sliding)
        {
            sliding = true;
            capsuleCollider2D.size = new Vector3(0.9f, 0.5f, 0);
            capsuleCollider2D.offset = new Vector3(0, -0.19f, 0);
            capsuleCollider2D.direction = CapsuleDirection2D.Horizontal;
            anim.SetBool("Slide", true);
            // Slide animaation loputtua Animation kaynnistaa SlideEnds() eventin joka palauttaa capsulecolliderin normaaliksi.
        }

        //---- kyyky tai liuku. end ---

        if (Input.GetAxis("Horizontal") < -0.1f)
        {
            
            transform.localScale = new Vector3(-1, 1, 1);
            facingRight = false;
        }

        if (Input.GetAxis("Horizontal") > 0.1f)
        {
            
            transform.localScale = new Vector3(1, 1, 1);
            facingRight = true;
        }
        // ------------------------- double jump Start ----------------------
        if (Input.GetButtonDown("Jump")&& !wallSliding && !sliding)
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
        if (curHealth <= 0)
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
        capsuleCollider2D.size = new Vector3(0.58f, 0.9f, 0);
        capsuleCollider2D.offset = new Vector3(0, 0, 0);
        capsuleCollider2D.direction = CapsuleDirection2D.Vertical;
        anim.SetBool("Slide", false);
        sliding = false;
    }

    private void FixedUpdate()
    {
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

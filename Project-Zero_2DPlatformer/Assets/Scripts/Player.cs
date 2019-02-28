using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public float maxSpeed = 3;
    public float speed = 10f;
    public static float deadSpeed; //CorpsePlayer.cs
    public float minFloatingSpeed = 1f; // Maksimi nopeus johon pelaajan nopeus tippuu hiljalleen kun pelaaja on ilmassa.
    public float jumpPower = 150f;
    public float doubleJumpPower = 200f;
    public float maxJumpPower = 500f;

    private float orginalSpeed;
    public int curHealth;
    public int maxHealth = 5;
    public bool grounded;
    public bool canDoubleJump;
    public bool wallSliding;
    public bool facingRight = true;
    public bool playerCanDieIfHitsWall = false;
    public bool wallCheck;
    public static bool dead = false; // Kohteet jotka tarvitsevat tätä tietoa. CameraFollow.cs / playerSpawnPoint.cs / NameGenerator.cs / CreateEnemysPoint.cs /EnemySpawnPoint.cs

    private float jumpPowerOrginal;
    private bool hasJumped;
    public static bool sliding = false; // Kohteet jotka tarvitsevat tätä tietoa. WallKill.cs / BootKill.cs
    private bool timeToBoost = false;
    private bool timeToJump = false;
    private bool JumpChargeControl = false;

    float animationPlaySpeed;
    //References
    public Transform firepoint;
    public Transform corpseSpawnPoint;
    public GameObject spawnCorpse;
    public GameObject blood;
    public Transform wallCheckPoint;
    public LayerMask wallLayerMask;
    
    private CapsuleCollider2D capsuleCollider2D;
    private Rigidbody2D rb2d;
    private Animator anim;
    private GameMaster gameMaster;
    private CameraFollow cameraFollow;
    // Use this for initialization
    void Start ()
    {
        Physics2D.IgnoreLayerCollision(11, 12); //ignooraa pelaaja ruumiit.
        sliding = false;
        dead = false;
        orginalSpeed = speed;
        curHealth = maxHealth;
        jumpPowerOrginal = jumpPower; //Pelaajan hyppyvoima palutuu normaaliksi pelaajan osuessa maahan. Esim sieni popistaa hyppyvoiman sinkoessaan pelaajan ilmaan.
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        cameraFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();

        //maarittaa capsulecolliderin koon heron ollessa pystyasenossa.
        capsuleCollider2D = gameObject.GetComponent<CapsuleCollider2D>();
        if (capsuleCollider2D != null)
        {
            capsuleCollider2D.size = new Vector3(0.55f, 1.2f, 0);
            capsuleCollider2D.offset = new Vector3(0.08f, -0.04f, 0);
            capsuleCollider2D.direction = CapsuleDirection2D.Vertical;
        }
  
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(jumpPower);
          Debug.Log(speed);
        
        anim.SetBool("Grounded", grounded);
        anim.SetFloat("Speed", Mathf.Abs(speed));
  
        //--- Kyykky tai liuku. Start. Muuttaa capsuleCollider2D kokoa ja suuntaa.---
        if (Input.GetKeyDown("x")  && !sliding && !dead && sliding == false)
        {
            anim.speed = 1;
            if (grounded)
            {
                speed++;
            }
            sliding = true;
            capsuleCollider2D.size = new Vector3(0.9f, 0.5f, 0);
            capsuleCollider2D.offset = new Vector3(0, -0.04f, 0);
            capsuleCollider2D.direction = CapsuleDirection2D.Horizontal;
            firepoint.localPosition = new Vector3(1.374f, 0f, 0f);
            anim.SetBool("Slide", true);
            // Slide animaation loputtua Animation kaynnistaa SlideEnds() eventin joka palauttaa capsulecolliderin normaaliksi.
        }
        //---- kyyky tai liuku. end ---

        // ------------------------- double jump Start ----------------------
        if (Input.GetButtonDown("Jump") && !dead)
        {
            if (JumpChargeControl == false && grounded)
            {
                JumpChargeControl = true;
                StartCoroutine(JumpCharge());
            } else  if (canDoubleJump && hasJumped == false)
            {
                canDoubleJump = false;
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                rb2d.AddForce(Vector2.up * doubleJumpPower);
                hasJumped = true;
            }

            if (grounded)
            {
                hasJumped = false;
                // jumpPower = jumpPowerOrginal; //Pelaajan hyppyvoima palutuu normaaliksi pelaajan osuessa maahan. Esim sieni popistaa hyppyvoiman sinkoessaan pelaajan ilmaan.
                {
                    //hasJumped = false;
                }
                {
                    
                   /* if (timeToJump)
                    {
                        rb2d.AddForce(Vector2.up * jumpPower);
                        JumpChargeControl = false;
                        canDoubleJump = true;
                        timeToJump = false;
                        jumpPower = jumpPowerOrginal;
                    }*/
                }
            } /*else
            {
                if (canDoubleJump && hasJumped == false)
                {     
                    canDoubleJump = false;
                    rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                    rb2d.AddForce(Vector2.up * doubleJumpPower);
                    hasJumped = true;          
                }
            }  */ 
        }

     /*   if (timeToJump)
        {
            rb2d.AddForce(Vector2.up * jumpPower);
            JumpChargeControl = false;
            canDoubleJump = true;
            timeToJump = false;
            jumpPower = jumpPowerOrginal;
        }*/
        // -----------------------double jump End -------------------------
    }

    // Slide animaation loputtua Animation kaynnistaa taman eventin.
    void SlideEnds()
    {
        firepoint.localPosition = new Vector3(1.374f, 0.23f, 0f);
        capsuleCollider2D.size = new Vector3(0.55f, 1.2f, 0);
        capsuleCollider2D.offset = new Vector3(0.08f, -0.04f, 0);
        capsuleCollider2D.direction = CapsuleDirection2D.Vertical;
        anim.SetBool("Slide", false);
        if (grounded) {
            speed--;
        }

        sliding = false;
    }

    private void FixedUpdate()
    {        
        if (playerCanDieIfHitsWall == false && !dead && grounded)
        {
            anim.speed = speed / 26;
            if (speed < orginalSpeed)
            {
                timeToBoost = true;
            }
         
            if (playerCanDieIfHitsWall == false && timeToBoost == true)
            {
                StartCoroutine(BoostSpeed());
                timeToBoost = false;
            }

            transform.Translate(speed * Time.deltaTime, 0, 0); // Liikuttaa pelajaa oikealle.

        } else if (playerCanDieIfHitsWall == false && !grounded && !dead ) //pelaaja menettää nopeutta suorittaessaan tuplahypyn.
        {  
            StartCoroutine(LoseSpeed());
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }

        if (playerCanDieIfHitsWall == true && !dead) // Lyo pelaajan vasemmalle esim."EnemyHorizontalPowerPunch.cs" aktivoi vaman ja asettaa nopeudeksi -20.
        {           
            transform.Translate(speed * Time.deltaTime, 0.1f, 0);          
        }
    }

    IEnumerator JumpCharge() {
        while (Input.GetButton("Jump")&& jumpPower < maxJumpPower) {
            yield return new WaitForSeconds(0.001f);
            jumpPower = jumpPower + 50;
        }
        yield return timeToJump = true;
        yield return Jump(timeToJump);
    }

    IEnumerator LoseSpeed()
    {
        float speedloseSpeed = 0;
        if (canDoubleJump)
        {
            speedloseSpeed = 0.0001f;
        }
        else {
            speedloseSpeed = 0.0008f;
        }
            for (float i = minFloatingSpeed; i <= speed && !grounded;)
            {
            yield return new WaitForSeconds(0.01f);
            speed -= speedloseSpeed;
            
            }
        
        yield return timeToBoost = true;
    }

    IEnumerator BoostSpeed()
    {
        for (float i = orginalSpeed; i >= speed;)
        {
            speed += 0.01f;
            yield return new WaitForSeconds(0.05f);
        }
        yield return 0;
    }

    // Vahingon aiheuttajilla on paasy tahan.
    public void Damage(int dmg)
    {
        curHealth -= dmg;
        if (curHealth <= 0)
        {
            Die();
        }
    }
    private bool Jump(bool TimeToJump)
    {
        rb2d.AddForce(Vector2.up * jumpPower);
        JumpChargeControl = false;
        canDoubleJump = true;   
        jumpPower = jumpPowerOrginal;
        return timeToJump = false;
    }

    void Die()
    {       
        dead = true;
        Destroy(GameObject.FindGameObjectWithTag("CameraPoint"));
        gameObject.tag = "Enemy";
        deadSpeed = speed;
        Instantiate(spawnCorpse, corpseSpawnPoint.position, corpseSpawnPoint.rotation);
        speed = 0;
        rb2d.constraints = RigidbodyConstraints2D.None;
        Destroy(GetComponent<BoxCollider2D>());
        Destroy(GetComponent<CapsuleCollider2D>());
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(gameObject);
        playerSpawnPoint.spawNewSoldier = true;
    }

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
            Damage(5);
        }
    }


    void OnBecameInvisible() // kamera palaa smootisti aloitukseen kunnes näkee taas pelaajan ja pysyy sitten tiukasti siinä.
    {
        cameraFollow.SeePlayer(false);
    }
  
    void OnBecameVisible()
    {
        cameraFollow.SeePlayer(true);
    }

}

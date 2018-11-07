using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockPack : MonoBehaviour {

    private Rigidbody2D rb2d;
    private Enemy enemy;

    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            

            StartCoroutine(player.KnockBack(0.02f, 1000, player.transform.position));
        }
    }
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
}

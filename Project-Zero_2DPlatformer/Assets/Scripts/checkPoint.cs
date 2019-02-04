using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPoint : MonoBehaviour{

    bool playerHasReachedThisCheckpoint = false;
    public Transform newPlayerSpawnPoint;
    public GameObject playerSpawnPoint;

    /*Tuhoaa ensin vanhanpelaajan luontipisteen, jonka jälkeen luo uuden.*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && playerHasReachedThisCheckpoint == false)
        {
            playerHasReachedThisCheckpoint = true;
            Destroy(GameObject.FindGameObjectWithTag("PlayerSpawnPoint"));
            Instantiate(playerSpawnPoint, newPlayerSpawnPoint.position, newPlayerSpawnPoint.rotation);
        }
    }
}

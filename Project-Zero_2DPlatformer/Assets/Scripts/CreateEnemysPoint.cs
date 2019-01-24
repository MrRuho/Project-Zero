using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemysPoint : MonoBehaviour
{
    public static bool PlayerHasEnteredZone = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && PlayerHasEnteredZone == false)
        {
            PlayerHasEnteredZone = true;
            Debug.Log(" PlayerHasEnteredZone " + PlayerHasEnteredZone);
        }
    }
}

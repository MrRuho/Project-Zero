using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {

    public int LevelToLoad;

    private GameMaster gameMaster;

    private void Start()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameMaster.InputText.text = ("[E] to Enter");
            if (Input.GetKeyDown("e"))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            if (Input.GetKeyDown("e"))
            {
                SceneManager.LoadScene(LevelToLoad);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameMaster.InputText.text = (" ");
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {

    public int points;

    public Text pointsText;
    public Text InputText;

    private void Start()
    {
        if (PlayerPrefs.HasKey("CoinsStore"))
        {
            if (SceneManager.GetActiveScene().name == "Main_Test_Level")
            {
                Debug.Log("Main_Test_Level Loaded. Reset game progress.");
                PlayerPrefs.DeleteKey("CoinsStore");
                points = 0;
            }
            else
            {
                points = PlayerPrefs.GetInt("CoinsStore", 0);
                Debug.Log("Saved game progress.");
            }
        }
    }

    void Update()
    {
        pointsText.text = ("Coins:" + points);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

    public int points;

    public Text pointsText;
    public Text InputText;

    void Update()
    {
        pointsText.text = ("Coins:" + points);
    }

}

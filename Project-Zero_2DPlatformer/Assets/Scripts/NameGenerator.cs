using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameGenerator : MonoBehaviour
{

    public List<string> names;
    public string[] lines;

    private GameMaster gameMaster;
    // Use this for initialization
    void Start()
    {
        // Etsii satunnaisen nimen/ rivin (myos tyhjat jo niita on) Resource kansiosta Names.txt tiedostosta ja asettaa sen nimeksi.
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        TextAsset nameText = Resources.Load<TextAsset>("Names");
        lines = nameText.text.Split("\n"[0]);
        gameMaster.SoldierName.text = lines[Random.Range(0, lines.Length)];
    }
}

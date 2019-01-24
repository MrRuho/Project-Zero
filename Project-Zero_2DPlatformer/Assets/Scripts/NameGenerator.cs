using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameGenerator : MonoBehaviour
{

    public List<string> names;
    public string[] lines;

    private GameMaster gameMaster;

    private bool nameHasSet = false;
    // Use this for initialization
    void Start()
    {
        // Etsii satunnaisen nimen/ rivin (myos tyhjat jo niita on) Resource kansiosta Names.txt tiedostosta ja asettaa sen nimeksi.
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        TextAsset nameText = Resources.Load<TextAsset>("Names");
        lines = nameText.text.Split("\n"[0]);
        gameMaster.SoldierName.text = lines[Random.Range(0, lines.Length)];
    }

    void Update()
    {
        if (nameHasSet == false && Player.dead == false)
        {
            gameMaster = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
            TextAsset nameText = Resources.Load<TextAsset>("Names");
            lines = nameText.text.Split("\n"[0]);
            gameMaster.SoldierName.text = lines[Random.Range(0, lines.Length)];
            nameHasSet = true;
        }
        if (Player.dead == true)
        {
            nameHasSet = false;
        }
    }
}

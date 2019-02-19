using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    public Sprite[] HeartSprites;

    public Image HeartsUI;

    private Player player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        
    }

    void Update()
    {
       // HeartsUI.sprite = HeartSprites[player.curHealth]; taman tilalle tulee aikanaan sotilas rankit ja mitallit.
    }
}

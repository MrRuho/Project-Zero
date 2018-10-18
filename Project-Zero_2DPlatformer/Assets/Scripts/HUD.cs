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
        
        if (player.curHealth > player.maxHealth)
        {
            player.curHealth = player.maxHealth;
        }

        if (player.curHealth < 0)
        {
            player.curHealth = 0;
        }

        HeartsUI.sprite = HeartSprites[player.curHealth];
    }
}

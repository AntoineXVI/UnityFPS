using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int playerHealth;
    public Text HealthText;
    private playerlogic player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<playerlogic>();
    }

    private void Update()
    {
        playerHealth = player.health;
        SetupHp(playerHealth);
    }
    public void SetupHp(int Hp)
    {
        HealthText.text = "HP : " + Hp.ToString();
    }
}

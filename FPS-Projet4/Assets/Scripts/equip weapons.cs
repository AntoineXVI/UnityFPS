using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class equipweapons : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");        
    }

    private void Update()
    {
        if (gameObject.transform.parent == player.transform) //si l'arme est un enfant = equipé
        {
            player.GetComponent<playerlogic>().isWeaponEquiped = true;
            player.GetComponent<playerlogic>().weapon = gameObject;
            gameObject.SetActive(true);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //arme par terre recuperable
            Debug.Log("collide");
            player.GetComponent<playerlogic>().weapon = gameObject;
            Debug.Log(player.GetComponent<playerlogic>().weapon);
            player.GetComponent<playerlogic>().isWeaponArround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //arme par terre n'est plus recuperable
            Debug.Log("no collide");
            player.GetComponent<playerlogic>().isWeaponArround = false;
        }
    }
    
}

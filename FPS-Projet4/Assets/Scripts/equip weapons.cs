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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("weapon equipped");
            player.GetComponent<playerlogic>().isWeaponEquiped = true;
            player.GetComponent<playerlogic>().weapon = gameObject;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("weapon droppped");
            player.GetComponent<playerlogic>().isWeaponEquiped = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isJump : MonoBehaviour
{
    public GameObject Player;
    // Start is called before the first frame update

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("true");
            Player.GetComponent<playerlogic>().onGround = true;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player.GetComponent<playerlogic>().onGround = false;
        }
    }
}

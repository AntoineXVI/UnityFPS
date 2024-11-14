using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealLogic : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player Gained HP");
            other.gameObject.GetComponent<playerlogic>().FullHealth();
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobLogic : MonoBehaviour
{
    //GameObject mob;
    public int health = 3;

    // Start is called before the first frame update
    void Start()
    {
        //mob.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoseHealth()
    {
        health--;
        if (health == 0)
        {
            Destroy(gameObject);
        }
    }
}

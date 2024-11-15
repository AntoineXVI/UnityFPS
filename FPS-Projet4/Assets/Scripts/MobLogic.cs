using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobLogic : MonoBehaviour
{
    //GameObject mob;
    public int health = 6;
    GameObject weapon;

    private AudioManagingScript audioClass;

    // Start is called before the first frame update
    void Start()
    {
        weapon = GameObject.FindWithTag("WeaponSpawn");
        GameObject audioObject = GameObject.FindWithTag("Audio");
        if (audioObject != null)
        {
            audioClass = audioObject.GetComponent<AudioManagingScript>();
        }
        else
        {
            Debug.LogError("UI object with tag 'UI' not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoseHealth()
    {
        health--;
        if (health >= 0)
        {
            audioClass.ennemyDeathSound();
            Instantiate(weapon, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}

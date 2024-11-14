using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ammologic : MonoBehaviour
{
    float speed;
    GameObject weapon;
    Vector3 SpawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        speed = 8f;
        weapon = GameObject.FindWithTag("Player").GetComponent<playerlogic>().weapon;
        SpawnPosition = weapon.transform.GetChild(4).position;
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += -transform.right * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player Lost HP");
            other.gameObject.GetComponent<playerlogic>().LoseHealth();
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Ennemy")
        {
            Debug.Log("Mob Lost HP");
            other.gameObject.GetComponent<MobLogic>().LoseHealth();
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Weapon")
        {
            Debug.Log("Our Weapon was hit");
        }
        if (other.gameObject.tag == "Environment")
        {
            Debug.Log("BulletDestroyed");
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("BulletDestroyed");
            Destroy(gameObject);
        }
    }
}
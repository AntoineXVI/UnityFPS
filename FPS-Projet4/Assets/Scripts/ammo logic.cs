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
        speed = 4f;
        weapon = GameObject.FindWithTag("Player").GetComponent<playerlogic>().weapon;
        SpawnPosition = weapon.transform.GetChild(4).position;
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += -transform.right * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hit bullet");
        Destroy(gameObject); 
    }
}
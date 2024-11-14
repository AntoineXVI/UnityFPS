using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnnemyBehaviour : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject following;
    private float stopDistance = 15f;

    public GameObject weapon;
    public GameObject bullet;
    public Transform bulletSpawnPoint;
    public float shootInterval = 2f;
    private float lastShootTime;

    private Vector3 weaponPos;
    private Vector3 bulletPos;

    private void followPlayer()
    {
        if (following != null)
        {
            float distance = Vector3.Distance(agent.transform.position, following.transform.position);
            if (distance > stopDistance)
            {
                agent.updateRotation = true;
                agent.SetDestination(following.transform.position);
            }
            else
            {
                agent.updateRotation = false;
                RotateTowards(following.transform.position);
                if (Time.time > lastShootTime + shootInterval)
                {
                    Debug.Log("Got To Time");
                    Shoot();
                    lastShootTime = Time.time;
                }
            }
        }
        else
        {
            throw new System.Exception("No player to follow");
        }
    }

    private void RotateTowards(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * agent.angularSpeed);
    }

    private void Shoot()
    {
        if (bulletPos != null)
        {
            bulletPos = weapon.transform.Find("BulletPosition").transform.position;
            GameObject bullet1 = Instantiate(bullet, bulletPos, weapon.transform.rotation);
            bullet1.transform.RotateAround(bullet1.transform.position, Vector3.up, -90);
            bullet1.name = "bullet1";
            bullet1.AddComponent<BoxCollider>();
            bullet1.GetComponent<BoxCollider>().isTrigger = true;
            bullet1.AddComponent<ammologic>();
            bullet1.transform.localScale *= 3;
        }
        else
        {
            Debug.Log("error weapon");
        }
    }

    private GameObject FindChildWithTag(Transform parent, string tag)
    {
        foreach (Transform child in parent)
        {
            if (child.CompareTag(tag))
            {
                return child.gameObject;
            }
        }
        return null;
    }

    void Start()
    {
        weapon = FindChildWithTag(transform, "Weapon");
        agent = GetComponent<NavMeshAgent>();
        bullet = GameObject.Find("BulletLite_01");
        following = GameObject.FindWithTag("Player");
        lastShootTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        followPlayer();
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Android;

public class playerlogic : MonoBehaviour
{
    public int speed;
    public int initSpeed;
    public int speedCameraY;
    public int speedCameraX;
    public GameObject weapon;
    public GameObject bullet;
    public GameObject bulletCasing;

    public bool isWeaponEquiped;
    public bool isWeaponArround;

    private Vector3 weaponPos;
    private Vector3 bulletPos;
    private Quaternion weaponRota;    

    private AudioManagingScript audioClass;
    private gameOverLogic uiClass;

    public int health = 100;

    // Start is called before the first frame update
    void Start()
    {
        speed = 5;
        initSpeed = 5;
        speedCameraY = 40;
        speedCameraX = 50;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        bullet = GameObject.Find("BulletLite_01_1");
        bulletCasing = GameObject.Find("BulletLite_01_2");
        weapon = null;

        isWeaponEquiped = false;
        isWeaponArround = false;

        // Initialisation de uiClass
        GameObject audioObject = GameObject.FindWithTag("Audio");
        if (audioObject != null)
        {
            audioClass = audioObject.GetComponent<AudioManagingScript>();
        }
        else
        {
            Debug.LogError("UI object with tag 'UI' not found");
        }
        GameObject uiObject = GameObject.FindWithTag("UICaller");
        if (uiObject != null)
        {
            uiClass = uiObject.GetComponent<gameOverLogic>();
        }
        else
        {
            Debug.LogError("UI object with tag 'UI' not found");
        }
    }

    bool grounded()
    {
        return GetComponent<Rigidbody>().velocity.y == 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("SetCursor")) //hide or show the cursor
        {
            if (Cursor.visible)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }

        if (Input.GetButtonDown("Jump") && grounded()) //saut s'il est au sol
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
        }
        if (Input.GetButtonDown("Sprint"))
        {
            speed *= 2;
        }
        if (Input.GetButtonUp("Sprint"))
        {
            speed = initSpeed;
        }
        if (Input.GetButtonDown("Shoot")) //tire s'il a une arme
        {
            if (isWeaponEquiped)
            {
                Debug.Log("shoot");
                if (weapon && weapon.GetComponent<equipweapons>().ammo >= 1)
                {
                    bulletPos = weapon.transform.Find("BulletPosition").transform.position;
                    GameObject bullet1 = Instantiate(bullet, bulletPos, weapon.transform.rotation);
                    bullet1.transform.RotateAround(bullet1.transform.position, Vector3.up, -90);
                    bullet1.name = "bullet1";
                    bullet1.AddComponent<BoxCollider>();
                    bullet1.GetComponent<BoxCollider>().isTrigger = true;
                    bullet1.AddComponent<ammologic>();
                    bullet1.transform.localScale *= 3;

                    weapon.GetComponent<equipweapons>().ammo -= 1;

                    // Bullet casing
                    bulletPos = weapon.transform.Find("BulletPosition").transform.position;
                    GameObject bulletCasing1 = Instantiate(bullet, weapon.transform.position, weapon.transform.rotation);
                    bulletCasing1.name = "bulletCasing1";
                    bulletCasing1.transform.RotateAround(bulletCasing1.transform.position, Vector3.up, -90);
                    bulletCasing1.AddComponent<Rigidbody>();
                    bulletCasing1.AddComponent<BoxCollider>();
                    bulletCasing1.AddComponent<ammologic>();
                    bulletCasing1.transform.localScale *= 3;
                    Destroy(bulletCasing1, 0.5f);
                }
                else
                {
                    Debug.Log("no ammo");
                }
            }
            else
            {
                Debug.Log("no weapon");
            }
        }
        if (Input.GetButtonDown("Reload")) //recharge s'il a une arme
        {
            if (isWeaponEquiped)
            {
                Debug.Log("reload");
                weapon.GetComponent<equipweapons>().ammo = 10;
            }
            else
            {
                Debug.Log("no weapon");
            }
        }

        if (Input.GetButtonDown("TakeWeapon")) //ramasse une arme
        {
            if (!isWeaponEquiped && isWeaponArround)
            {
                Debug.Log("weapon taken");
                weapon.transform.SetParent(transform);
                weaponPos = GameObject.Find("weaponPosition").transform.position;
                weaponRota = GameObject.Find("weaponPosition").transform.rotation;
                weapon.transform.position = weaponPos;
                weapon.transform.rotation = weaponRota;
                isWeaponArround = false;
            }
            else if (isWeaponEquiped)
            {
                Debug.Log("weapon already equipped");
            }
            else
            {
                Debug.Log("no weapon here");
            }
        }
        if (Input.GetButtonDown("DropWeapon")) //lâche son arme
        {
            if (isWeaponEquiped)
            {
                Debug.Log("drop");
                isWeaponEquiped = false;
                weapon.SetActive(false);
                weapon = null;
            }
            else
            {
                Debug.Log("no weapon");
            }
        }

        // Mouvements
        float translationV = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        float translationH = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        transform.Translate(translationH, 0, translationV);

        // Camera
        float rotationV = Input.GetAxis("Mouse Y") * Time.deltaTime * speed;
        float rotationH = Input.GetAxis("Mouse X") * Time.deltaTime * speed;
        transform.RotateAround(transform.position, Vector3.up, rotationH * speedCameraX);
        transform.RotateAround(transform.position, transform.right, -rotationV * speedCameraY);
    }

    public void LoseHealth()
    {
        health--;
        if (health <= 0)
        {
            if (uiClass != null)
            {
                uiClass.OverUI();
            }
            else
            {
                Debug.LogError("uiClass is not initialized");
            }
            Destroy(gameObject);
        }
    }

    public void FullHealth()
    {
        health = 100;
    }
}
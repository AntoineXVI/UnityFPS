using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
<<<<<<< HEAD:FPS-Projet4/Assets/Scripts/player logic.cs
using UnityEngine.Android;
=======
using UnityEngine.Playables;
using static equipweapons;
>>>>>>> 35e6791650b17374148537b8e6c30f01e9c788bd:FPS-Projet4/Assets/Scripts/PlayerLogic.cs

public class playerlogic : MonoBehaviour
{
    public int speed;
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
    private Animator weaponAnimator;
    private equipweapons weaponInfo;

    // Insérer ici l'appel de classe startLogic
    private AudioManagingScript audioClass;
    private gameOverLogic uiClass;

    public int health = 100;

    // Start is called before the first frame update
    void Start()
    {
        speed = 5;
        speedCameraY = 40;
        speedCameraX = 50;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        bullet = GameObject.Find("BulletLite_01_1");
        bulletCasing = GameObject.Find("BulletLite_01_2");

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
        if (GetComponent<Rigidbody>().velocity.y == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("SetCursor")) //hide or show the cursor
        {
            Debug.Log("cursor");
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
            speed /= 2;
        }
        if (Input.GetButtonDown("Crouch"))
        {
            //crouch
            Debug.Log("Crouch");
        }
        if (Input.GetButtonUp("Crouch"))
        {
            //stop crouch
            Debug.Log("uncrouch");
        }
        if (Input.GetButtonDown("Shoot")) //tire s'il a une arme
        {
            if (isWeaponEquiped)
            {
                Debug.Log("shoot");
                weaponInfo = weapon.GetComponent<equipweapons>();
                //balle
                if (weapon)
                {
<<<<<<< HEAD:FPS-Projet4/Assets/Scripts/player logic.cs
                    
                    if (weapon.GetComponent<equipweapons>().ammo >= 1) //si on a plus d'une balle
=======
                    if ((weaponInfo.ammo >= 1) && (weaponInfo.WeaponCurrentState == WeaponState.Idle)) //si on a plus d'une balle
>>>>>>> 35e6791650b17374148537b8e6c30f01e9c788bd:FPS-Projet4/Assets/Scripts/PlayerLogic.cs
                    {
                        audioClass.fireSound();
                        bulletPos = weapon.transform.Find("BulletPosition").transform.position;
                        GameObject bullet1 = Instantiate(bullet, bulletPos, weapon.transform.rotation);
                        bullet1.transform.RotateAround(bullet1.transform.position, Vector3.up, -90);
                        bullet1.name = "bullet1";
                        bullet1.AddComponent<BoxCollider>();
                        bullet1.GetComponent<BoxCollider>().isTrigger = true;
                        bullet1.AddComponent<ammologic>();
                        bullet1.transform.localScale *= 3;

                        weapon.GetComponent<equipweapons>().ammo -= 1;

                        //douille de balle
                        bulletPos = weapon.transform.Find("BulletPosition").transform.position;
                        GameObject bulletCasing1 = Instantiate(bullet, weapon.transform.position, weapon.transform.rotation);
                        bulletCasing1.name = "bulletCasing1";
                        bulletCasing1.transform.RotateAround(bulletCasing1.transform.position, Vector3.up, -90);
                        bulletCasing1.AddComponent<Rigidbody>();
                        bulletCasing1.AddComponent<BoxCollider>();
                        bulletCasing1.AddComponent<ammologic>();
                        bulletCasing1.transform.localScale *= 3;
                    }
                    else
                    {
                        Debug.Log("no ammo");
                    }
                }
                else
                {
                    Debug.Log("error weapon");
                    Debug.Log(weapon);
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
<<<<<<< HEAD:FPS-Projet4/Assets/Scripts/player logic.cs
                audioClass.reloadSound();
                weapon.GetComponent<equipweapons>().ammo = 10;
=======
                //reload
                weapon.GetComponent<equipweapons>().WeaponCurrentState = WeaponState.Reload;
                weaponAnimator = weapon.GetComponent<Animator>();
                weaponAnimator.SetBool("isReload", true);
                Debug.Log("switch state");
                StartCoroutine(ReloadWeapon());                
>>>>>>> 35e6791650b17374148537b8e6c30f01e9c788bd:FPS-Projet4/Assets/Scripts/PlayerLogic.cs
            }
            else
            {
                Debug.Log("no weapon");
            }
        }

        if (Input.GetButtonDown("TakeWeapon")) //rmasse une arme
        {
            if (!isWeaponEquiped) //s'il y a une arme au sol et le player n'en a pas
            {
                Debug.Log("take weapon");
                //take weapon
                if (isWeaponArround)
                {
                    Debug.Log("weapon taken");
                    Destroy(weapon.GetComponent<Rigidbody>());
                    weapon.transform.SetParent(transform);
                    if (weapon.name == "M4_8")
                    {
                        weaponPos = GameObject.Find("weaponPositionM4").transform.position;
                        weaponRota = GameObject.Find("weaponPositionM4").transform.rotation;
                    }
                    if (weapon.name == "M1911")
                    {
                        weaponPos = GameObject.Find("weaponPositionM1911").transform.position;
                        weaponRota = GameObject.Find("weaponPositionM1911").transform.rotation;
                    }

                    weapon.transform.position = weaponPos;
                    weapon.transform.rotation = weaponRota;
                    isWeaponArround = false;
                }
                else
                {
                    Debug.Log("no weapon here");
                }
            }
            else
            {
                Debug.Log("weapon already equipped");
            }
        }
        if (Input.GetButtonDown("DropWeapon")) //lache son arme
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
        //mouvements
        float translationV = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        float translationH = Input.GetAxis("Horizontal") * Time.deltaTime * speed;

        transform.Translate(translationH, 0, translationV);
        //camera 
        float rotationV = Input.GetAxis("Mouse Y") * Time.deltaTime * speed;

        float rotationH = Input.GetAxis("Mouse X") * Time.deltaTime * speed;

        transform.RotateAround(transform.position, Vector3.up, rotationH * speedCameraX);
        transform.RotateAround(transform.position, transform.right, -rotationV * speedCameraY);
    }

<<<<<<< HEAD:FPS-Projet4/Assets/Scripts/player logic.cs
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
=======
    private IEnumerator ReloadWeapon()
    {
        Debug.Log("Reloading...");

        // Obtenir la durée d'animation
        float reloadTime = weapon.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;

        // Attendre la fin de l'animation
        yield return new WaitForSeconds(reloadTime);
        weapon.GetComponent<Animator>().SetBool("isReload", false);
        weapon.GetComponent<equipweapons>().ammo = 10;
        weapon.GetComponent<equipweapons>().WeaponCurrentState = WeaponState.Idle;
        Debug.Log("Reloading end");
    }

}
>>>>>>> 35e6791650b17374148537b8e6c30f01e9c788bd:FPS-Projet4/Assets/Scripts/PlayerLogic.cs

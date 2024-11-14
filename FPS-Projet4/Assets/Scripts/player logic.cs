using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Playables;
using static equipweapons;

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
        weapon = null;

        isWeaponEquiped = false;
        isWeaponArround = false;
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
                    if ((weaponInfo.ammo >= 1) && (weaponInfo.WeaponCurrentState == WeaponState.Idle)) //si on a plus d'une balle
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
                //reload
                weapon.GetComponent<equipweapons>().WeaponCurrentState = WeaponState.Reload;
                weaponAnimator = weapon.GetComponent<Animator>();
                weaponAnimator.SetBool("isReload", true);
                Debug.Log("switch state");
                StartCoroutine(ReloadWeapon());                
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
                    weapon.transform.SetParent(transform);
                    weaponPos = GameObject.Find("weaponPosition").transform.position;
                    weaponRota = GameObject.Find("weaponPosition").transform.rotation;
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

        transform.Translate(translationH, 0, translationV) ;
        //camera 
        float rotationV = Input.GetAxis("Mouse Y") * Time.deltaTime * speed;
       
        float rotationH = Input.GetAxis("Mouse X") * Time.deltaTime * speed;

        transform.RotateAround(transform.position, Vector3.up, rotationH * speedCameraX);
        transform.RotateAround(transform.position, transform.right, -rotationV * speedCameraY);
    }

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

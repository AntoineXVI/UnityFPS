using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

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
    private PlayerState currentState;
    private Animator playerAnimator;

    private enum PlayerState
    {
        Idle,
        Run,
        Jump,
        Shoot,
        GetHit,
        Reload,
        TakeWeapon,
        DropWeapon
    }

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
        currentState = PlayerState.Idle;
        playerAnimator = GetComponent<Animator>();

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
        //stateMachine

        switch (currentState)
        {
            case PlayerState.Idle:
                Reset();
                Debug.Log(currentState);
                break;

            case PlayerState.Run:
                Reset();
                Debug.Log(currentState);
                playerAnimator.SetBool("isRuning", true);
                Run();
                break;

            case PlayerState.Jump:
                Reset();
                Debug.Log(currentState);
                playerAnimator.SetBool("isJumping", true);
                Jump();
                break;

            case PlayerState.Shoot:
                Reset();
                Debug.Log(currentState);
                playerAnimator.SetBool("isShooting", true);
                Shoot();
                break;

            case PlayerState.GetHit:
                Reset();
                Debug.Log(currentState);
                playerAnimator.SetBool("isHit", true);
                GetHit();
                break;
            
            case PlayerState.Reload:
                Reset();
                Debug.Log(currentState);
                playerAnimator.SetBool("isReloading", true);
                Reload();
                break;

            case PlayerState.TakeWeapon:
                Reset();
                Debug.Log(currentState);
                playerAnimator.SetBool("Take", true);
                TakeWeapon();
                break;

            case PlayerState.DropWeapon:
                Reset();
                Debug.Log(currentState);
                playerAnimator.SetBool("Drop", true);
                DropWeapon();
                break;
        }

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
            currentState = PlayerState.Jump;
        }
        if (Input.GetButtonDown("Sprint")) 
        {
            currentState = PlayerState.Run;
        }
        if (Input.GetButtonUp("Sprint"))
        {
            currentState = PlayerState.Idle;
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
            currentState = PlayerState.Shoot;
            
        }
        if (Input.GetButtonDown("Reload")) //recharge s'il a une arme
        {
            currentState = PlayerState.Reload;    
        }
       
        if (Input.GetButtonDown("TakeWeapon")) //rmasse une arme
        {
            currentState = PlayerState.TakeWeapon;
        }
        if (Input.GetButtonDown("DropWeapon")) //lache son arme
        {
            currentState = PlayerState.DropWeapon;
            
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
    private void Reset()
    {
        playerAnimator.SetBool("isRunning",false);
        playerAnimator.SetBool("isShooting",false);
        playerAnimator.SetBool("isReloading",false);
        playerAnimator.SetBool("isJumping", false);
        playerAnimator.SetBool("isHit",false);
        playerAnimator.SetBool("Drop",false);
        playerAnimator.SetBool("Take",false);

        speed = initSpeed;
    }

    private void Jump()
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
    }
    private void Shoot()
    {
        if (isWeaponEquiped)
        {
            Debug.Log("shoot");
            //balle
            if (weapon)
            {
                if (weapon.GetComponent<equipweapons>().ammo >= 1) //si on a plus d'une balle
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
    private void GetHit()
    {

    }
    private void Run()
    {
        speed *= 2;
    }
    private void Reload()
    {
        if (isWeaponEquiped)
        {
            Debug.Log("reload");
            //reload
            weapon.GetComponent<equipweapons>().ammo = 10;
        }
        else
        {
            Debug.Log("no weapon");
        }
    }
    private void TakeWeapon()
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
    private void DropWeapon()
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
}

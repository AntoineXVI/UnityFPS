using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerlogic : MonoBehaviour
{
    public int speed;
    public int speedCameraY;
    public int speedCameraX;
    public bool isWeaponEquiped;
    public GameObject weapon;

    // Start is called before the first frame update
    void Start()
    {
        speed = 5;
        speedCameraY = 40;
        speedCameraX = 50;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        isWeaponEquiped = true;
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
        //Debug.Log("tag = " + onGround);
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
        if (Input.GetButtonDown("Shoot"))
        {
            if (isWeaponEquiped)
            {
                Debug.Log("shoot");
                //shoot
            }
            else
            {
                Debug.Log("no weapon");
            }
            
        }
        if (Input.GetButtonDown("Reload"))
        {
            if (isWeaponEquiped)
            {
                Debug.Log("reload");
                //reload
            }
            else
            {
                Debug.Log("no weapon");
            }            
        }
        if (Input.GetButtonDown("TakeWeapon"))
        {
            if (!isWeaponEquiped)
            {
                Debug.Log("take weapon");
                //take weapon
            }
            else
            {
                Debug.Log("weapon already equipped");
            }
            
        }
        if (Input.GetButtonDown("DropWeapon"))
        {
            if (isWeaponEquiped)
            {
                Debug.Log("drop");
                isWeaponEquiped = false;
                //drop weapon
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
}

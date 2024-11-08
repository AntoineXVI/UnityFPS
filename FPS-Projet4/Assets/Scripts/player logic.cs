using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerlogic : MonoBehaviour
{
    public int speed;
    public int speedCameraY;
    public int speedCameraX;
    private bool onGround;
    // Start is called before the first frame update
    void Start()
    {
        speed = 5;
        speedCameraY = 40;
        speedCameraX = 50;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            onGround = true;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            onGround = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("tag = " + onGround);
        if (Input.GetButtonDown("Jump") /*&& onGround*/) //saut s'il est au sol
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
            //if weapon equipped
            //shoot
            Debug.Log("shoot");
        }
        if (Input.GetButtonDown("Reload"))
        {
            //if weapon equipped
            //reload
            Debug.Log("reload");
        }
        if (Input.GetButtonDown("TakeWeapon"))
        {
            //if 0 weapon equipped
            Debug.Log("take weapon");
        }
        if (Input.GetButtonDown("DropWeapon"))
        {
            //if weapon equipped
            Debug.Log("drop");
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
}

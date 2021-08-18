using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    
    public Transform FirePoint;
    public GameObject bulletPrefab;
    
    public float launchVelocity = 700f;


    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {


                Shoot();


            }
        }

    }

    void Shoot()
    {
        Instantiate(bulletPrefab, FirePoint.position, FirePoint.rotation);

    }



    void ResetFire()
    {

    }
}

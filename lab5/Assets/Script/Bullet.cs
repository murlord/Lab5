using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody rb;
    public float lifetime;
    public int damage;

    void Start()
    {
        rb.velocity = transform.right * speed;

        if (lifetime <= 0)
        {
            lifetime = 2.0f;
        }


    }
  
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class DestroyMySelf : MonoBehaviour
{
    private float Timer = 2.0f;

    private float DodgeTimer = 0.5f;
    
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Timer -= Time.deltaTime;
        if (Timer <= 0)
        {
            Destroy(gameObject);
        }
        
        DodgeTimer -= Time.deltaTime;
        if(rb.velocity.y > 4 && DodgeTimer <= 0)
        {
            rb.velocity = new Vector2(rb.velocity.x * Random.Range(-4f , 4f),0f);
            DodgeTimer = 0.5f;
        }
        
    }
   
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AIChase : MonoBehaviour
{ 
    public GameObject player;
    public float speed = 1.0f; 
    public float distanceToPlayer = 10.0f;
    private float _distance;
    public float roationSpeed = 0.5f;
    public Player Player;

    private GameManager GameManager;
    
    public bool deltaDistance = false;

    private void Start()
    {
        GameManager = GameManager.Instance;
    }


    // Update is called once per frame
    void Update()
    {
        if(Player.HP <= 0) return;
        _distance = Vector2.Distance(player.transform.position, transform.position);
        Vector2 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        deltaDistance = _distance < distanceToPlayer;
        
        if (!deltaDistance) return;
        transform.position = Vector2.MoveTowards(this.transform.position,player.transform.position, speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(Vector3.forward * angle * roationSpeed);
    }
}

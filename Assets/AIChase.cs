using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour
{ 
    public GameObject player;
    public float speed = 1.0f; 
    public float distanceToPlayer = 10.0f;
    private float _distance;
    public float roationSpeed = 0.5f;
    
        // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _distance = Vector2.Distance(player.transform.position, transform.position);
        Vector2 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (!(_distance < distanceToPlayer)) return;
        transform.position = Vector2.MoveTowards(this.transform.position,player.transform.position, speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(Vector3.forward * angle * roationSpeed);
    }
}

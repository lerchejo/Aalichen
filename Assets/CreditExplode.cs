using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditExplode : MonoBehaviour
{
    public List<Sprite> Contribs = new();
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            foreach (Sprite contrib in Contribs)
            {
                GameObject credit = new GameObject();
                credit.transform.SetParent(transform.parent);
                credit.AddComponent<SpriteRenderer>().sprite = contrib;
                credit.AddComponent<Rigidbody2D>().gravityScale = 0.2f;
                //credit.GetComponent<Rigidbody2D>().freezeRotation = true;
                credit.GetComponent<Rigidbody2D>().angularDrag = 2f;
                credit.GetComponent<Rigidbody2D>().velocity = new Vector2(UnityEngine.Random.Range(-4.0f, 4.0f) , UnityEngine.Random.Range(2.0f, 3.0f) );
                
                credit.AddComponent<BoxCollider2D>();
                credit.AddComponent<DestroyMySelf>();
                credit.transform.position = transform.position;
            }
            Destroy(gameObject);
        }
    }
}

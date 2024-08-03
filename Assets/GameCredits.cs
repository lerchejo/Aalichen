using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCredits : MonoBehaviour
{
    public List<CreditObject> Credits = new();

    public float MaxCooldown = 5.0f;
    
    private float currentCooldown = 0.0f;
    
    private int currentCredit = 0;
    
    private void Update()
    {
        if(currentCooldown <= 0)
        {
            if (currentCredit >= Credits.Count)
            {
                currentCredit = 0;
            }
                //GameObject credit = Instantiate(Credits[currentCredit].Name, transform);
                GameObject credit = new GameObject();
                credit.AddComponent<SpriteRenderer>().sprite = Credits[currentCredit].Name;
                credit.AddComponent<Rigidbody2D>().gravityScale = 0.1f;
                credit.AddComponent<BoxCollider2D>();
                credit.AddComponent<CreditExplode>().Contribs = Credits[currentCredit].Roles;
                credit.transform.position = new Vector3(0, 10, 0);
                credit.transform.SetParent(gameObject.transform);
                
            
            
            currentCooldown = MaxCooldown;
            currentCredit++;
        }
        else
        {
            currentCooldown -= Time.deltaTime;
        }
    }
}

[Serializable]
public class CreditObject
{
    public string title;
    public Sprite Name;
    public List<Sprite> Roles;
}
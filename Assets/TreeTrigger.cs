using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeTrigger : MonoBehaviour
{

    private SpriteRenderer Shadow;
    private SpriteRenderer spriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        //Shadow is the child of the tree 
        Shadow = transform.GetChild(0).GetComponent<SpriteRenderer>();
        
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            spriteRenderer.sortingLayerName = "Layer 1";
            Shadow.sortingLayerName = "Default";
            Debug.Log("Player is near the tree");
        }
        
    }
    
    IEnumerator Await()
    {
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.sortingLayerName = "Layer 2";
        Shadow.sortingLayerName = "Layer 2";
    }
    
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Await());
            
            Debug.Log("Player is no longer near the tree");
        }
    }
}

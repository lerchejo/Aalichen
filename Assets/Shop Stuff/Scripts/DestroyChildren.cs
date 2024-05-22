using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyChildren : MonoBehaviour
{

    public void Execute() 
    {
        foreach (Transform child in transform) 
        {
            Destroy(child.gameObject);
        }
    
    }
}

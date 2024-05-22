using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deleteMe : MonoBehaviour
{
    public Camera Camera;

    private void Start()
    {
        Camera = Camera.main;
    }


    private void Update()
    {
               Camera.backgroundColor = Color.Lerp(Color.cyan, Color.blue, Mathf.PingPong(Time.time, 1));
    }
}

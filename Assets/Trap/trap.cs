using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class trap : MonoBehaviour
{
    //public NewMovement player;
    public float time = 3.0f;
    public float delay = 0.3f;
    Rigidbody2D Player_rb = null;
    [FormerlySerializedAs("player")] private NewMovement player_movement;
    private Animator trap_animator;
    private Animator walk_animator;


    private void Start()
    {
        player_movement = NewMovement.Instance;
        trap_animator = GetComponent<Animator>();
        walk_animator = Player.Instance.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Freeze player for time seconds
            Player_rb = other.gameObject.GetComponent<Rigidbody2D>();
            StartCoroutine(trapPlayer(time));
            print("Player trapped");
        }
    }
    
    IEnumerator trapPlayer(float time)
    {
        yield return  new WaitForSeconds(delay);        
        trap_animator.SetBool("trapped", true);
        walk_animator.SetBool("MoveUp", false);
        walk_animator.SetBool("MoveDown", false);
        walk_animator.SetBool("MoveSide", false);
        // Disable physics and movement
        if (Player_rb != null)
        {
            Player_rb.velocity = Vector2.zero; // Stop any current movement immediately
            Player_rb.isKinematic = true; // Disable physics interactions
        }
        if (player_movement != null)
        {
            player_movement.enabled = false; // Disable the movement script
        }

        // Wait for the specified time
        yield return new WaitForSeconds(time);
        trap_animator.SetBool("trapped", false);
        
        // Re-enable physics and movement
        if (Player_rb != null)
        {
            Player_rb.isKinematic = false;
        }
        if (player_movement != null)
        {
            player_movement.enabled = true;
        }
    }
}

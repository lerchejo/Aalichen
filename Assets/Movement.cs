using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D body;

    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;

    public AnimatorController UpAnimator;
    public AnimatorController DownAnimator;
    public AnimatorController SideAnimator;
    
    public float runSpeed = 20.0f;
    public float dashSpeed = 40.0f; // Speed when dashing
    private bool isDashing = false; // Flag to check if dashing
    public float dashDuration = 0.25f; // How long the dash should last
    private Coroutine currentDashCoroutine;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down
        
        // Get reference to the Animator component
        Animator animator = GetComponent<Animator>();
        
        Animation animation = GetComponent<Animation>();
        
        // Set the direction parameter based on the direction of movement
        if (horizontal > 0)
        {
            animator.runtimeAnimatorController = SideAnimator;
            transform.localScale = new Vector3(1, 1, 1); // Set scale to 1 for moving right

        }
        else if (horizontal < 0)
        {
            animator.runtimeAnimatorController = SideAnimator;
            transform.localScale = new Vector3(-1, 1, 1); // Set scale to -1 for moving left


        }
        else if (vertical > 0)
        {
            animator.runtimeAnimatorController = UpAnimator;
            animation.Play("front-walk-animation");

        }
        else if (vertical < 0)
        {      
            animator.runtimeAnimatorController = DownAnimator;
            animation.Play("walk-back-animation");

        }

        
        // Check if space bar is being held down
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // If a dash is already in progress, stop it
            if (currentDashCoroutine != null)
            {
                StopCoroutine(currentDashCoroutine);
            }

            // Start a new dash
            currentDashCoroutine = StartCoroutine(Dash());
        }

      
    }
    
    IEnumerator Dash()
    {
        isDashing = true;
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
    }
    
    private void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }

        // Check if dashing
        if (isDashing)
        {
            body.velocity = new Vector2(horizontal * dashSpeed, vertical * dashSpeed);
        }
        else
        {
            body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
        }
    }
}
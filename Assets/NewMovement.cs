using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;
using UnityEngine.UI;
using Slider = UnityEngine.UI.Slider;

public class NewMovement : MonoBehaviour
{
    Rigidbody2D body;
        
    public float horizontal;
    public float vertical;
    float moveLimiter = 0.7f;

  //  public AnimatorController UpAnimator;
  //  public AnimatorController DownAnimator;
  //  public AnimatorController SideAnimator;
    
    public float runSpeed = 20.0f;
    public float dashSpeed = 40.0f; // Speed when dashing
    private bool isDashing = false; // Flag to check if dashing
    public float dashDuration = 0.25f; // How long the dash should last
    private Coroutine currentDashCoroutine;
    public float dashCooldown = 1.0f; // Cooldown duration in seconds
    private float lastDashTime = -1.0f; // Time when the last dash occurred
    
    public Slider CooldownSlider;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        
        float timeUntilNextDash = Mathf.Max(0, lastDashTime + dashCooldown - Time.time);

        // Update the value of the slider
        CooldownSlider.value = 1 - timeUntilNextDash / dashCooldown;
        
        // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down
        
        // Get reference to the Animator component
        Animator animator = GetComponent<Animator>();
        
        Animation animation = GetComponent<Animation>();
        // Check if there is no input
        if (horizontal == 0 && vertical == 0)
        {
            animator.SetBool("MoveUp", false);
            animator.SetBool("MoveDown", false);
            animator.SetBool("MoveSide", false);
        }
        else
        {
            // Resume the animation
            animator.speed = 1;

            // Set the direction parameter based on the direction of movement
            if (horizontal > 0)
            {
                animator.SetBool("isEatingNazi", false);
                animator.SetBool("isEatingFood", false);
                animator.SetBool("MoveUp", false);
                animator.SetBool("MoveDown", false);
                animator.SetBool("MoveSide", true);
                transform.localScale = new Vector3(1, 1, 1); // Set scale to 1 for moving right

            }
            else if (horizontal < 0)
            {
                animator.SetBool("isEatingNazi", false);
                animator.SetBool("isEatingFood", false);
                animator.SetBool("MoveUp", false);
                animator.SetBool("MoveDown", false);
                animator.SetBool("MoveSide", true);
                
                transform.localScale = new Vector3(-1, 1, 1); // Set scale to -1 for moving left


            }
            else if (vertical > 0)
            {
                animator.SetBool("isEatingNazi", false);
                animator.SetBool("isEatingFood", false);
                animator.SetBool("MoveDown", false);
                animator.SetBool("MoveSide", false);
                animator.SetBool("MoveUp", true);

            }
            else if (vertical < 0)
            {
                animator.SetBool("isEatingNazi", false);
                animator.SetBool("isEatingFood", false);
                animator.SetBool("MoveUp", false);
                animator.SetBool("MoveSide", false);
                animator.SetBool("MoveDown", true);
            }


            // Check if space bar is being held down
            if (Input.GetKeyDown(KeyCode.Space) && Time.time >= lastDashTime + dashCooldown)
            {
                // If a dash is already in progress, stop it
                if (currentDashCoroutine != null)
                {
                    StopCoroutine(currentDashCoroutine);
                }

                // Start a new dash
                currentDashCoroutine = StartCoroutine(Dash());

                // Update the time of the last dash
                lastDashTime = Time.time;
            }
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
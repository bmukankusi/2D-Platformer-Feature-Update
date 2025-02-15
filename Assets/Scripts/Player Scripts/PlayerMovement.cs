using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody2D myBody;
    private Animator anim;

    public Transform groundCheckPosition;
    public LayerMask groundLayer;

    private bool isGrounded;
    private bool jumped;

    private float jumpPower = 12f;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>(); // FIX: Properly assign Rigidbody2D
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        CheckIfGrounded(); // Ensure the player is grounded every frame
        PlayerJump(); // Check for jump input
    }

    void FixedUpdate()
    {
        PlayerWalk();
    }

    void PlayerWalk()
    {
        float h = Input.GetAxis("Horizontal"); // FIX: Correct input axis

        if (h > 0)
        {
            myBody.velocity = new Vector2(speed, myBody.velocity.y);
            ChangeDirection(1);
        }
        else if (h < 0)
        {
            myBody.velocity = new Vector2(-speed, myBody.velocity.y);
            ChangeDirection(-1);
        }
        else
        {
            myBody.velocity = new Vector2(0f, myBody.velocity.y);
        }

        anim.SetInteger("Speed", Mathf.Abs((int)myBody.velocity.x));
    }

    void ChangeDirection(int direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

    // Checking if the player is on the ground
    void CheckIfGrounded()
    {
        isGrounded = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 0.1f, groundLayer);

        if (isGrounded)
        {
            if (jumped)
            {
                jumped = false;
                anim.SetBool("Jump", false);
            }
        }
    }

    // Make the player jump
    void PlayerJump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space)) // FIX: Spacebar to jump
        {
            jumped = true;
            myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
            anim.SetBool("Jump", true);
        }
    }
}

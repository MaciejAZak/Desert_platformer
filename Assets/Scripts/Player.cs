using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField] float Speed = 5f;
    [SerializeField] float ClimbSpeed = 4f;
    [SerializeField] float jumpSpeed = 15f;
    bool isAlive = true;

    Rigidbody2D myRigidBody;
    Animator myAnimator;
    Collider2D myCollider2D;


    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider2D = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
        JumpValidation();
        Climb();
    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");

        if (Input.GetKey("left shift"))
        {
            Debug.Log("Shift");
            myRigidBody.velocity = new Vector2 (controlThrow * Speed * 1.5f, myRigidBody.velocity.y);
        }
        else
        {
            myRigidBody.velocity = new Vector2(controlThrow * Speed, myRigidBody.velocity.y);
        }

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playerHasHorizontalSpeed);


    }

    private void JumpValidation()
    {
        if (!myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            if (myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladders")))
            {
                Jump();
            }
            return;
        }
        Jump();

    }

    private void Jump()
    {
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Debug.Log("Jumped");
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidBody.velocity += jumpVelocityToAdd;
        }
    }

    private void Climb()
    {
        if (myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladders")))
        {
            Debug.Log("Touching ladder");
            float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
            if (controlThrow !=0)
            {
                Vector2 playerVelocity = new Vector2(myRigidBody.velocity.x, controlThrow * ClimbSpeed);
                myRigidBody.velocity = playerVelocity;
                bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
                myAnimator.SetBool("Climbing", playerHasVerticalSpeed);
            }
        }
        else
        {
            Debug.Log("Not touching ladder");
            myAnimator.SetBool("Climbing", false);
        }
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }
}

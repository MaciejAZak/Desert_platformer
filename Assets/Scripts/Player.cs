using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField] float Speed = 5f;
    [SerializeField] float ClimbSpeed = 4f;
    [SerializeField] float jumpSpeed = 15f;
    [SerializeField] Vector2 DeathKick = new Vector2(-2f, 200f);
    [SerializeField] AudioClip deathSFX;
    bool isAlive = true;
    bool climbingRightNow = false;

    Transform myTransform;
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    BoxCollider2D myCollider2D;
    CapsuleCollider2D myFeetCollider;
    float gravityAtStart;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<Transform>();
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider2D = GetComponent<BoxCollider2D>();
        myFeetCollider = GetComponent<CapsuleCollider2D>();
        gravityAtStart = myRigidBody.gravityScale;
        climbingRightNow = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { return;  }

        DieOnEnemyTouch();
        Spiked();
        Run();
        Climb();
        JumpValidation();
        FlipSprite();
        //IsClimbing();
        //ShowPosition();
    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float RT = Input.GetAxis("XboxRightTrigger");

        if (Input.GetKey("left shift"))
        {
            Debug.Log("Shift");
            myRigidBody.velocity = new Vector2(controlThrow * Speed * 1.5f, myRigidBody.velocity.y);
        }
        else if(Mathf.Round(RT)>0)
        {
            Debug.Log("RT clicked");
            myRigidBody.velocity = new Vector2(controlThrow * Speed * 1.5f, myRigidBody.velocity.y);
        }
        else
        {
            myRigidBody.velocity = new Vector2(controlThrow * Speed, myRigidBody.velocity.y);
        }

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playerHasHorizontalSpeed);


    }


    // TODO: NOT JUMPING WHILE MOVING LEFT (WORKING WHILE MOVING RIGHT)
    private void JumpValidation()
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            if (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ladders")))
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
            ReleaseLadder();
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidBody.velocity += jumpVelocityToAdd;
            
        }
    }

    private void DieOnEnemyTouch()
    {
        // TODO: DONT DIE WHEN FALL FROM ABOVE ON ENEMIES
        if (myCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemies", "Water")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            AudioSource.PlayClipAtPoint(deathSFX, FindObjectOfType<Camera>().transform.position);
            GetComponent<Rigidbody2D>().velocity = DeathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
    private void Spiked()
    {
        if (myCollider2D.IsTouchingLayers(LayerMask.GetMask("Hazards")) && GetComponent<Rigidbody2D>().velocity.y <0)
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            AudioSource.PlayClipAtPoint(deathSFX, FindObjectOfType<Camera>().transform.position);
            GetComponent<Rigidbody2D>().velocity = DeathKick * 0.5f;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }


    private void Climb()

        //TODO: STOP CLIMBING WHEN YOU REACH TOP OF A LADDER (because currently you fall down once you stop colliding with a ladder)
        //TODO: STOP MOVING LEFT AND RIGHT WHILE ON A LADDER
        //TODO: AUTOMATICALY RELEASE THE LADDER WHEN YOU REACH THE BOTTOM
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
        if (myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladders")) && controlThrow > 0)
        {
            climbingRightNow = true;
        }

        if (!myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladders")))
        {
            Debug.Log("Not touching ladder");
            ReleaseLadder();
        }
        if (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ladders")) && !myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladders")))
        {
            Debug.Log("Standing on top of a ladder");
            myRigidBody.gravityScale = 0f;
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, 0f);
        }
        if (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ladders")) && controlThrow < 0)
        {
            climbingRightNow = true;
        }

        if (climbingRightNow == true)
        {
            myTransform.transform.position = new Vector2 (Mathf.Round(myRigidBody.position.x), myTransform.transform.position.y);
            //Debug.Log("Touching ladder");
            if (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && controlThrow < 0)
            {
                ReleaseLadder();
            }
            else if (controlThrow != 0)
            {
                myRigidBody.gravityScale = 0f;
                Vector2 playerVelocity = new Vector2(myRigidBody.velocity.x, controlThrow * ClimbSpeed);
                myRigidBody.velocity = playerVelocity;
                bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
                myAnimator.SetBool("Climbing", playerHasVerticalSpeed);
            }
            else if (controlThrow == 0)
            {
                myRigidBody.gravityScale = 0f;
                Vector2 playerVelocity = new Vector2(myRigidBody.velocity.x, controlThrow * ClimbSpeed);
                myRigidBody.velocity = playerVelocity;
                return;
            }
            

        }
    }
    private void ReleaseLadder()
    {
        climbingRightNow = false;
        myRigidBody.gravityScale = gravityAtStart;
        myAnimator.SetBool("Climbing", false);
    }

    private void ShowPosition()
    {

        Debug.Log(myTransform.transform.position);
    }

    private void IsClimbing()
    {

        Debug.Log(climbingRightNow);
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

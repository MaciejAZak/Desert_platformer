using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{

    Rigidbody2D myRigidBody;
    [SerializeField] float Speed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        Run();
    }

    private void Run()
    {
        Animation.Running = true;
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * Speed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
    }
}

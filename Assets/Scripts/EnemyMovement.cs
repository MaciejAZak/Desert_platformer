using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    Rigidbody2D myRigidBody;
    BoxCollider2D myPeriscope;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myPeriscope = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFacingLeft())
        {
            myRigidBody.velocity = new Vector2(-moveSpeed, 0f);
        }
        else
        {
            myRigidBody.velocity = new Vector2(moveSpeed, 0f);
        }
    }

    bool IsFacingLeft()
    {
        return transform.localScale.x > 0;
    }

    //TODO: Apply triggercollider only to periscope
    private void OnTriggerExit2D(Collider2D myPeriscope)
    {
        Debug.Log("rotateEnemySprite");
        transform.localScale = new Vector2((Mathf.Sign(myRigidBody.velocity.x)), 1f);
    }
}

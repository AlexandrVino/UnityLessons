using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    // significant types
    public float jumpForce = 5.0f;
    public float speed = 10.0f;
    public int extraJumpsCount = 2;
    public bool currSide = true;

    // flags Interaction with some objects
    // for example Rope, surface, ets
    public bool onGround = false;
    public bool isRunning = false;

    // reference types of scripts
    private Rigidbody2D rigidBody;
    private HingeJoint2D hingleJoint;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        hingleJoint = GetComponent<HingeJoint2D>();
    }

    // reference types
    private void Update()
    {
        Move();
    }

    private void FixedUpdate()
    {
        FixedMove();
    }

    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Jump();
        if (
            Input.GetKeyDown(KeyCode.D) && !currSide ||
            Input.GetKeyDown(KeyCode.A) && currSide
        ) Flip();

        if (onGround) extraJumpsCount = 2;
    }

    private void FixedMove()
    {
        isRunning = Input.GetAxis("Horizontal") != 0.0f;
        transform.position = (
            rigidBody.position + Vector2.right * Input.GetAxis("Horizontal") *
            speed * Time.deltaTime
        );
    }

    private void Jump()
    {
        hingleJoint.enabled = false;
        if (extraJumpsCount > 0)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0.0f);
            rigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            extraJumpsCount--;
        }
    }

    private void Flip()
    {
        // False -> left; True -> right
        currSide = !currSide;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "surface":
                onGround = true; break;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "surface":
                onGround = false; break;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        switch (collider.gameObject.tag)
        {
            case "rope":
                onGround = false; break;
        }
    }
}

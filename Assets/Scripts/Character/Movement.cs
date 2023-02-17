using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    // significant types
    public float JumpForce = 15.0f;
    public float Speed = 10.0f;
    public int ExtraJumpsCount = 2;
    public bool CurrSide = true;

    // flags Interaction with some objects
    // for example Rope, surface, ets
    public bool OnGround = false;
    public bool IsRunning = false;

    // reference types of scripts
    private Rigidbody2D _rigidBody;
    private HingeJoint2D _hingleJoint;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _hingleJoint = GetComponent<HingeJoint2D>();
    }

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
            Input.GetKeyDown(KeyCode.D) && !CurrSide ||
            Input.GetKeyDown(KeyCode.A) && CurrSide
        ) Flip();

        if (OnGround) ExtraJumpsCount = 2;
    }

    private void FixedMove()
    {
        IsRunning = Input.GetAxis("Horizontal") != 0.0f;
        transform.position = (
            _rigidBody.position + Vector2.right * Input.GetAxis("Horizontal") * Speed * Time.deltaTime
        );
    }

    private void Jump()
    {
        _hingleJoint.enabled = false;
        if (ExtraJumpsCount > 0)
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, 0.0f);
            _rigidBody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            ExtraJumpsCount--;
        }
    }

    private void Flip()
    {
        // False -> left; True -> right
        CurrSide = !CurrSide;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Surface surface)) OnGround = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Surface surface)) OnGround = false;
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Rope rope)) OnGround = false; 
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // significant types
    public float jumpForce = 5.0f;
    public float speed = 10.0f;
    public int extraJumpsCount = 2;
    private string currAnimation = "default";
    public bool currSide = true;
    private float lastJoint;

    // flags Interaction with some objects
    // for example Rope, surface, ets
    public bool onGround = true;

    // reference types
    private Rigidbody2D rb;
    private CircleCollider2D cc;
    private Animator animator;
    private HingeJoint2D hj;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CircleCollider2D>();
        hj = GetComponent<HingeJoint2D>();
        animator = GetComponent<Animator>();
    }

    private void ChangeAnimation(string animation)
    {
        if (animation == currAnimation) return;
        animator.Play(animation);
        currAnimation = animation;
    }

    private void Update()
    {
        BehaviorController();
    }

    private void BehaviorController()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Jump();
        if (
            Input.GetKeyDown(KeyCode.D) && !currSide ||
            Input.GetKeyDown(KeyCode.A) && currSide
        ) Flip();

        // rb.MovePosition(...) doesn't work (not changes rb.position)
        // I also try to use FixedUpdate(), but where have the same problem

        Vector2 buffPosition = rb.position;

        buffPosition += Vector2.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.position = buffPosition;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "surface":
                onGround = true; break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.gameObject.tag)
        {
            case "rope":
                hj.enabled = true;
                hj.connectedBody = collider.gameObject.GetComponent<Rigidbody2D>();
                onGround = true; break;
        }
        if (onGround) extraJumpsCount = 2;
    }

    private void Jump()
    {
        if (hj.enabled) hj.enabled = false;

        if (extraJumpsCount > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0.0f);
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            extraJumpsCount--;
        }
    }

    private void Flip()
    {
        // False -> left; True -> right
        currSide = !currSide;
        Vector2 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

}

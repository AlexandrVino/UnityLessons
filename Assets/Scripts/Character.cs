using System;
using System.Threading;
using System.Threading.Tasks;

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
    private string? lastJoint;

    // flags Interaction with some objects
    // for example Rope, surface, ets
    public bool onGround = false;
    public bool onRope = false;

    // reference types
    private Rigidbody2D rb;
    private CircleCollider2D cc;
    private Animator animator;
    private HingeJoint2D hingleJoint;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CircleCollider2D>();
        hingleJoint = GetComponent<HingeJoint2D>();
        animator = GetComponent<Animator>();

        // Task for setting const each 1 seconds 
        ChangingConsts();
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

    private void FixedUpdate()
    {
        BehaviorControllerFixed();
    }

    private void BehaviorControllerFixed()
    {
        // rb.MovePosition(...) doesn't work (not changes rb.position)
        // I also try to use FixedUpdate(), but where have the same problem
        transform.position = rb.position + Vector2.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime;
    }

    private void BehaviorController()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Jump();
        if (
            Input.GetKeyDown(KeyCode.D) && !currSide ||
            Input.GetKeyDown(KeyCode.A) && currSide
        ) Flip();

        string newAnimation = "default";
        if (hingleJoint.enabled) newAnimation = "engagement";
        else if (!onGround) newAnimation = "jump";
        else if (Input.GetAxis("Horizontal") != 0.0f) newAnimation = "run";

        ChangeAnimation(newAnimation);

        if (onGround) extraJumpsCount = 2;
    }


    private void Jump()
    {
        if (hingleJoint.enabled) (onRope, hingleJoint.enabled) = (false, false);

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

    private void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.gameObject.tag)
        {
            case "rope":
                // check than the last connected item != current
                if (lastJoint == collider.gameObject.transform.parent.name) return;

                // enable and connect new item to character
                hingleJoint.enabled = true;
                hingleJoint.connectedBody = collider.gameObject.GetComponent<Rigidbody2D>();
                lastJoint = collider.gameObject.transform.parent.name;
                (onRope, onGround) = (true, true);
                break;

            case "deathZone":
                transform.position = new Vector2(-17.0f, -3.0f);
                break;
        }
        if (onGround) extraJumpsCount = 2;
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        switch (collider.gameObject.tag)
        {
            case "rope":
                onGround = false;
                break;
        }
        if (onGround) extraJumpsCount = 2;
    }

    private void ChangingConsts()
    {
        Task.Factory.StartNew(() =>
        {

            while (true)
            {
                if (!onRope) lastJoint = null;
                Thread.Sleep(1000);
            }
        });
    }

}

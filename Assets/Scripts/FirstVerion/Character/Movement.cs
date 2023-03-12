using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    // significant types
    [SerializeField] private float _speed = 10.0f;
    [SerializeField] private bool _currSide = false;

    // flags Interaction with some objects
    // for example Rope, surface, ets
    [SerializeField] internal bool _isRunning = false;

    // reference types of scripts
    private Rigidbody2D _rigidBody;
    private HingeJoint2D _hingleJoint;

    private void Start()
    {
        // initialize reference varibles 
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
        /*
         Method for changing character ortation (lefr|right)
        */

        if (
            Input.GetKeyDown(KeyCode.D) && !_currSide ||
            Input.GetKeyDown(KeyCode.A) && _currSide
        ) Flip();
    }

    private void FixedMove()
    {
        /*
         Method for changing character 
         position by the abscissa axis
        */

        _isRunning = Input.GetAxis("Horizontal") != 0.0f;
        transform.position = (
            _rigidBody.position + Vector2.right * Input.GetAxis("Horizontal") * _speed * Time.deltaTime
        );
    }

    private void Flip()
    {
        /*
         Method for changing character ortation (lefr|right)
        */

        // False -> left; True -> right
        _rigidBody.velocity = new Vector2(0.0f, _rigidBody.velocity.y);
        _currSide = !_currSide;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }
}

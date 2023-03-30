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
    [SerializeField] private float _jumpForce = 20.0f;
    [SerializeField] private int _extraJumpsCount = 2;
    [SerializeField] internal bool _onGround = false;
    [SerializeField] internal bool _grounded = false;
    [SerializeField] private Vector2? _velocity;

    // reference types of scripts
    private Vector2? _surfaceNormal;
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

        if (Input.GetKeyDown(KeyCode.Space)) Jump();

        if (
            Input.GetKeyDown(KeyCode.D) && !_currSide ||
            Input.GetKeyDown(KeyCode.A) && _currSide
        ) Flip();

        if (_onGround) _extraJumpsCount = 2;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Surface surface))
        {
            _onGround = true;
            _grounded = true;

            _surfaceNormal = CountSurfaceNormal(collision);
        }
        else if (collision.gameObject.TryGetComponent(out RopeBracing ropeBracing)) (_grounded, _onGround) = (true, true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Surface surface))
        {
            _onGround = false;
            _grounded = false;
            _surfaceNormal = null;
        }
        else if (collision.gameObject.TryGetComponent(out RopeBracing ropeBracing)) (_grounded, _onGround) = (true, true);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.transform?.parent?.TryGetComponent(out Rope rope) ?? false) (_grounded, _onGround) = (false, true); ;
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.transform?.parent?.TryGetComponent(out Rope rope) ?? false) _onGround = false;
    }

    private void Jump()
    {
        _hingleJoint.enabled = false;
        if (_extraJumpsCount > 0)
        {
            Vector2 forceDirection = _surfaceNormal ?? new Vector2(0.0f, 1.0f);

            _velocity = new Vector2(_rigidBody.velocity.x, 0.0f);
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, 0.0f);

            _rigidBody.AddForce(forceDirection * _jumpForce, ForceMode2D.Impulse);
            _extraJumpsCount--;
        }
    }

    private void FixedMove()
    {
        /*
         Method for changing character 
         position by the abscissa axis
        */

        _isRunning = Input.GetAxis("Horizontal") != 0.0f;

        if (_hingleJoint.enabled && _grounded) return;

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

    private Vector2 CountSurfaceNormal(Collision2D collision)
    {
        int n = 0;
        Vector2 surf = Vector2.zero;

        foreach (ContactPoint2D element in collision.contacts)
        {
            surf += element.normal;
            n++;
        }

        return surf.x >= -0.45f && surf.x <= 0.45f ? surf / n : Vector2.up;
    }
}

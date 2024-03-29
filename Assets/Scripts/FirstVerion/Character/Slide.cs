using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Slide : MonoBehaviour
{
    [SerializeField] private float _minGroundNormalY = .65f;
    [SerializeField] private float _gravityModifier = 1f;
    [SerializeField] private Vector2 _velocity;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _speed;

    // flags Interaction with some objects
    // for example Rope, surface, ets
    [SerializeField] internal bool _onGround = false;

    private Rigidbody2D _rigidBody;
    private HingeJoint2D _hingleJoint;

    private Vector2? _surfaceNormal;
    private Vector2 _groundNormal;
    private Vector2 _targetVelocity;
    private bool _grounded;
    private ContactFilter2D _contactFilter;
    private RaycastHit2D[] _hitBuffer = new RaycastHit2D[16];
    private List<RaycastHit2D> _hitBufferList = new List<RaycastHit2D>(16);

    private const float MinMoveDistance = 0.001f;
    private const float ShellRadius = 0.01f;

    void OnEnable()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _hingleJoint = GetComponent<HingeJoint2D>();
    }

    void Start()
    {
        _contactFilter.useTriggers = false;
        _contactFilter.SetLayerMask(_layerMask);
        _contactFilter.useLayerMask = true;
    }

    void Update()
    {
        Debug.DrawLine(
            transform.position,
            (Vector2)transform.position + (_surfaceNormal ?? new Vector2(0.0f, 0.0f)) * 2,
            Color.red
        );

    }

    void FixedUpdate()
    {
        _velocity = _gravityModifier * Physics2D.gravity * Time.deltaTime * (_grounded || _hingleJoint.enabled ? 0.0f : _speed);

        _grounded = false;

        Vector2 deltaPosition = _velocity * Time.deltaTime;
        Vector2 moveAlongGround = new Vector2(_groundNormal.y, -_groundNormal.x);
        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement(move, false);

        if (!_hingleJoint.enabled)
        {
            move = Vector2.up * deltaPosition.y;
            Movement(move, true);
        }
    }

    private void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > MinMoveDistance)
        {
            int count = _rigidBody.Cast(move, _contactFilter, _hitBuffer, distance + ShellRadius);

            _hitBufferList.Clear();

            for (int i = 0; i < count; i++) _hitBufferList.Add(_hitBuffer[i]);

            for (int i = 0; i < _hitBufferList.Count; i++)
            {
                Vector2 currentNormal = _hitBufferList[i].normal;
                if (currentNormal.y > _minGroundNormalY)
                {
                    _grounded = true;
                    if (yMovement)
                    {
                        _groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(_velocity, currentNormal);
                if (projection < 0) _velocity = _velocity - projection * currentNormal;

                float modifiedDistance = _hitBufferList[i].distance - ShellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        _rigidBody.position = _rigidBody.position + move.normalized * distance;
    }
}
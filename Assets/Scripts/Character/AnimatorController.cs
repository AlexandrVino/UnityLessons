using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    // Enum class with animation consts
    public static class Animations
    {
        public static readonly string Default = "default";
        public static readonly string Run = "run";
        public static readonly string Jump = "jump";
        public static readonly string Engagement = "engagement";
    }

    // significant types
    private string _currAnimation = Animations.Default;

    // reference types
    private Movement _movement;

    private Animator _animator;
    private HingeJoint2D _hingleJoint;

    private void Start()
    {
        _hingleJoint = GetComponent<HingeJoint2D>();
        _animator = GetComponent<Animator>();
        _movement = GetComponent<Movement>();
    }

    private void ChangeAnimation(string animation)
    {
        if (animation == _currAnimation) return;
        _animator.Play(animation);
        _currAnimation = animation;
    }

    private void FixedUpdate()
    {
        if (_hingleJoint.enabled) ChangeAnimation(Animations.Engagement);
        else if (!_movement.OnGround) ChangeAnimation(Animations.Jump);
        else if (_movement.IsRunning) ChangeAnimation(Animations.Run);
        else ChangeAnimation(Animations.Default);
    }
}

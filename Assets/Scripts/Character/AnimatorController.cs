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
    private string currAnimation = Animations.Default;

    // reference types
    private Movement movement;

    private Animator animator;
    private HingeJoint2D hingleJoint;

    private void Start()
    {
        hingleJoint = GetComponent<HingeJoint2D>();
        animator = GetComponent<Animator>();
        movement = GetComponent<Movement>();
    }

    private void ChangeAnimation(string animation)
    {
        if (animation == currAnimation) return;
        animator.Play(animation);
        currAnimation = animation;
    }

    private void FixedUpdate()
    {
        if (hingleJoint.enabled) ChangeAnimation(Animations.Engagement);
        else if (!movement.onGround) ChangeAnimation(Animations.Jump);
        else if (movement.isRunning) ChangeAnimation(Animations.Run);
        else ChangeAnimation(Animations.Default);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    /* 
       Class for managing diiferent aspects of character behaviour
   *//*

    // significant types
    internal string lastJoint = "";

    // reference types of scripts
    internal RopeJump ropeJump;
    internal CharacterMovement movement;
    internal CharacterAnimator animator;

    private void Update()
    {
        movement.Move();
    }

    private void FixedUpdate()
    {
        movement.FixedMove();

        if (movement.ropeJump.GetHingleEnabled()) animator.ChangeAnimation(CharacterAnimator.Animations.Engagement);
        else if (!movement.onGround) animator.ChangeAnimation(CharacterAnimator.Animations.Jump);
        else if (movement.isRunning) animator.ChangeAnimation(CharacterAnimator.Animations.Run);
        else animator.ChangeAnimation(CharacterAnimator.Animations.Default);
    }
*/
    
}

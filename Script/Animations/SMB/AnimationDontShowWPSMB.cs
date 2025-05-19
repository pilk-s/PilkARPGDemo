using System.Collections;
using System.Collections.Generic;
using GGG.Tool;
using UnityEngine;

public class AnimationDontShowWPSMB : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetBool(AnimationID.IsShowWeaponID))
        {
            if (!animator.AnimationAtTag("Motion",0)) return;
            if (animator.AnimationAtTag("Attack",0)) return;
            // if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < .25) return;
            if (animator.GetFloat(AnimationID.MovementID) > 0.15)
            {
                animator.SetBool(AnimationID.UnEquipWPID,true);
                animator.SetBool(AnimationID.IsShowWeaponID,false);
            }
            else
            {
                animator.SetBool(AnimationID.UnEquipWPID,false);
            }
        }
        else
        {
            animator.SetBool(AnimationID.UnEquipWPID,false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}

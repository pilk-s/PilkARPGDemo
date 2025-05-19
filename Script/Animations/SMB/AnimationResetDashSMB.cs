using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationResetDashSMB : StateMachineBehaviour
{
    private float _interval = .25f;
    private float _currentTime = 0f;

    private bool _canDashAttack;
     // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _currentTime=_interval;
        _canDashAttack = true; 
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetCurrentAnimatorStateInfo(layerIndex).normalizedTime < 0.25f) return;

        if (_canDashAttack)
        {
            if (GameInputManager.MainInstance.LAttack)
            {
             animator.CrossFadeInFixedTime("Combo_Attack_03_02",0.15f,0,0f);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameEventManager.MainInstance.CallEvent("ResetCharacterDash");
    }

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

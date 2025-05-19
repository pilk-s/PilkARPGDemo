using System.Collections;
using System.Collections.Generic;
using GGG.Tool;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AnimationRigMatchPositionSMB : StateMachineBehaviour
{

    [SerializeField,Header("骨骼约束的名称")]private string _rigName;
    private TwoBoneIKConstraint _rightHandRig;
    private bool _enableConstraint=false;
   
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_rightHandRig == null)
        {
            _rightHandRig=animator.gameObject.GetComponentInChildren<TwoBoneIKConstraint>();
            if (_rightHandRig != null && _rightHandRig.name == _rigName)
            {
                DevelopmentTools.WTF("卧槽你居然拿到你儿子的组件了");
                _enableConstraint = true;
            }
        }
        DevelopmentTools.WTF(_rightHandRig.weight);
        
    }

    //  OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // _rightHandRig.weight = animator.GetFloat(AnimationID.HandRigInfoID);
        // DevelopmentTools.WTF("当前右手权重:"+_rightHandRig.weight);
        _rightHandRig.weight = 1;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _enableConstraint=false;
        // DevelopmentTools.WTF("退出时右手权重:"+_rightHandRig.weight);
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

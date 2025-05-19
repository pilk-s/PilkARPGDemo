using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationID
{
    //动画参数
    public static readonly int MovementID = Animator.StringToHash("Movement");
    public static readonly int HasInputID= Animator.StringToHash("HasInput");
    public static readonly int DeltaAngleID=Animator.StringToHash("DeltaAngle");
    public static readonly int RunID=Animator.StringToHash("Run");
    public static readonly int DashVerticalID=Animator.StringToHash("DashVertical");
    public static readonly int DashHorizontalID=Animator.StringToHash("DashHorizontal");
    public static readonly int DashID=Animator.StringToHash("Dash");
    public static readonly int LockID=Animator.StringToHash("Lock");
    public static readonly int HorizontalID=Animator.StringToHash("Horizontal");
    public static readonly int VerticalID=Animator.StringToHash("Vertical");
    public static readonly int IsShowWeaponID = Animator.StringToHash("IsShowWeapon");
    public static readonly int DashAttackID = Animator.StringToHash("DashAttack");
    public static readonly int BlockID = Animator.StringToHash("Block");
    
    //动画名称
    public static readonly int EquipWPID=Animator.StringToHash("EquipWP");
    public static readonly int UnEquipWPID=Animator.StringToHash("UnEquipWP");
    public static readonly int Finish01ID=Animator.StringToHash("Finish01");
    public static readonly int Block01ID=Animator.StringToHash("Block_Hit");
    
    //Curves 
    public static readonly int HandRigInfoID=Animator.StringToHash("HandRigInfo");
    public static readonly int RightHandRigID=Animator.StringToHash("RightHandRig");
}


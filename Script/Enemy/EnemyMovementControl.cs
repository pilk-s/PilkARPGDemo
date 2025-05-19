using System.Collections;
using System.Collections.Generic;
using GGG.Tool;
using piiilk_ARPGDemo.Movement;
using UnityEngine;

public class EnemyMovementControl : CharacterMovementControlBase
{
   //TODO:移动标志位 到后面有攻击脚本访问 当攻击时就不再可以移动
   private bool _canMove;

   protected override void Update()
   {
      base.Update();
      MotionLookPlayer();
      DrawEnemyDirToPlayer();

      _canMove = true;
   }

   /// <summary>
   /// 移动时看向玩家
   /// </summary>
   private void MotionLookPlayer()
   {
      if (_animator.AnimationAtTag("Motion"))
      {
         transform.Look(GameManager.MainInstance.GetMainPlayer().position,500f);
      }
   }
   
   /// <summary>
   /// 设置敌人的移动动画参数值
   /// </summary>
   /// <param name="Horizontal">左右移动值</param>
   /// <param name="Vertical">前后移动值</param>
   public void SetAnimationParameter(float Horizontal, float Vertical)
   {
      //TODO:设置敌人的移动动画参数值
      if (_canMove)
      {
         _animator.SetBool(AnimationID.HasInputID,true);
         _animator.SetFloat(AnimationID.LockID,1);
         _animator.SetFloat(AnimationID.HorizontalID,Horizontal,0.2f,Time.deltaTime);
         _animator.SetFloat(AnimationID.VerticalID,Vertical,0.2f,Time.deltaTime);
      }
      else
      {
         _animator.SetBool(AnimationID.HasInputID,false);
         _animator.SetFloat(AnimationID.HorizontalID, 0f,0.2f,Time.deltaTime);
         _animator.SetFloat(AnimationID.VerticalID, 0f,0.2f,Time.deltaTime);
      }
   }

   private void DrawEnemyDirToPlayer()
   {
      Debug.DrawRay(transform.position,
         GameManager.MainInstance.GetMainPlayer().position-transform.position,
         Color.cyan);
   }
}

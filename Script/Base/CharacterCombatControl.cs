using System;
using System.Collections;
using System.Collections.Generic;
using GGG.Tool;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public abstract class CharacterCombatControl : MonoBehaviour
{
   [SerializeField,Header("ComboData")]protected CharacterComboDataSO _comboData;
   protected CharacterComboDataSO _firstcomboData;
   [SerializeField]protected CharacterComboDataSO _finishComboData;
   [SerializeField,Header("当前敌人")]protected Transform _currentEnemy;
   private float _attackColdTime;
   protected bool _applyAttackInput;
   protected Animator _animator;
   protected bool _hasLastComboAction;
   
   protected virtual void Awake()
   {
      _animator = GetComponent<Animator>();
      _applyAttackInput = true;
      _hasLastComboAction = false;

      _firstcomboData = Instantiate(_comboData);
   }

   protected virtual void Update()
   {
      LookAtEnemy();
      MatchPosition();
   }

   #region 攻击时看向敌人（测试）

   

   private void LookAtEnemy()
   {
      if (DevelopmentTools.DistanceForTarget(_currentEnemy, transform) > 5) return;
      if (_currentEnemy != null)
      {
         if (_animator.AnimationAtTag("Attack"))
         {
            transform.Look(_currentEnemy.position,500f);
         }
      }
   }
   #endregion
   
   #region 伤害触发

   private void ATK(int index)
   {
      //TODO:触发伤害
      TriggerDamaged(index);
      GamePoolManager.MainInstance.TryGetPoolItem("SwordSound",transform.position,transform.rotation);
   }

   private void TriggerDamaged(int index)
   {
      //TODO:敌人检测 条件判断 当前是否满足触发伤害的条件
     
      if (_currentEnemy == null) return;
      if (Vector3.Dot(transform.forward.normalized,
             DevelopmentTools.DirectionForTarget(transform, _currentEnemy)) < .8f) return;
      if (DevelopmentTools.DistanceForTarget(_currentEnemy, transform) > 2.5f) return;
      if (_animator.AnimationAtTag("Attack"))
      {
         GameEventManager.MainInstance.CallEvent<float,string,string,Transform,Transform>("TriggerDamage",
            _comboData.DamageInfos[index].Damage,
            _comboData.DamageInfos[index].HitName,
            _comboData.DamageInfos[index].ParryName,
            _currentEnemy,
            transform
            );
      }
      else
      {
         //TODO:处决的事件呼唤  ->注意要满足处决条件
         
      }
   }

   #endregion

   #region 执行攻击动画

   protected void ExecuteComboAction()
   {
      if (_comboData == null) return;
      _attackColdTime = _comboData.ActionColdTime;
      _animator.CrossFade(_comboData.ActionName, 0.025f, 0, _comboData.AnimationOffset);
      TimerManager.MainInstance.TryGetOneTimer(_comboData.ActionColdTime,ResetComboInfo);
      _applyAttackInput = false;
   }

   #endregion

   #region 设置

   private void ResetComboInfo()
   {
      _applyAttackInput = true;
   }

   protected  virtual void ResetAllComboInfo()
   {
      _applyAttackInput = true;
      _attackColdTime = 0;
      SetComboDate(_firstcomboData);
      
   }
   
   //更新当前的播放的动画
   protected void SetComboDate(CharacterComboDataSO comboData)
   {
      if (comboData != null)
      {
         _comboData = comboData;
      }
   }

   public void SetHasLastComboAction(bool hasLastComboAction)
   {
      _hasLastComboAction = hasLastComboAction;
   }
   #endregion

   #region 位置匹配
   
   //攻击动画的位置匹配
   protected virtual void MatchPosition()
   {
      if (_currentEnemy == null) return;
      if (_animator == null) return;
      if (_animator.AnimationAtTag("Dead")) return;
      if (DevelopmentTools.DistanceForTarget(_currentEnemy, transform) > 2) return;
      var time=_animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
      if (time > 0.35f) return;

      if (_animator.AnimationAtTag("Attack"))
      {
         if (!_animator.IsInTransition(0) && !_animator.isMatchingTarget)
         {
            _animator.MatchTarget(_currentEnemy.position-(transform.forward.normalized*_comboData.MatchPositionOffset),
               quaternion.identity, 
               AvatarTarget.Body,
               new MatchTargetWeightMask(Vector3.one,0f),
               0f,0.35f);
         }
      }
   }

   protected void FinishMatchPosition()
   {
      if (!_animator.isMatchingTarget && !_animator.IsInTransition(0))
      {
         _animator.MatchTarget(_currentEnemy.position-(transform.forward*_finishComboData.MatchPositionOffset),
            quaternion.identity,
            AvatarTarget.Body,
            new MatchTargetWeightMask(Vector3.one,0f),
            0f,0.35f);
      }
   }

   #endregion
  
}

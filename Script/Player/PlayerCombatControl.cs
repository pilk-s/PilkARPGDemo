using System;
using System.Collections;
using System.Collections.Generic;
using GGG.Tool;
using UnityEngine;

public class PlayerCombatControl : CharacterCombatControl
{
    // private bool _hasLastComboAction;
    [SerializeField]private bool _canFinish;
    private Collider[] _enemyColliders;
    [SerializeField,Header("敌人检测范围")]private float _detectionRadius;
    [SerializeField, Header("检测层级")] private LayerMask _layerMask;
    protected override void Awake()
    {
        base.Awake();
        _canFinish = false;
    }

    private void OnEnable()
    {
        GameEventManager.MainInstance.AddEventListening("SetCanFinish",SetCanFinish);
    }

    private void OnDisable()
    {
        GameEventManager.MainInstance.RemoveEvent("SetCanFinish",SetCanFinish);
    }

    protected override void Update()
    {
        base.Update();
        PlayerAttackInput();
        ExcuteFinishAction();

        GetNearEnemys();
        GetCurrentTargetEnemy();
    }
    
    private bool CanExcuteAttackAction()
    {
        //TODO:做更多的条件判断 如 Dash 处决 跳跃 
        if (_animator.AnimationAtTag("Equip")) return false;
        if (_animator.AnimationAtTag("Dash")) return false;
        if (_animator.AnimationAtTag("Finish")) return false;
        if (_animator.GetFloat(AnimationID.MovementID) > 0.15f) return false;
        return true;
    }

    private void PlayerAttackInput()
    {
        if(!CanExcuteAttackAction()) return;
        if (_applyAttackInput)
        {
            if (GameInputManager.MainInstance.LAttack)
            {
                if(!_animator.GetBool(AnimationID.IsShowWeaponID)) 
                    GameEventManager.MainInstance.CallEvent("ShowWeapon");
                GameEventManager.MainInstance.CallEvent<float>("SetUnEquipWeaponLayer",0);
                if (_hasLastComboAction)
                {
                    SetComboDate(_comboData.NextComboAction);
                }
                ExecuteComboAction();
                SetHasLastComboAction(true);
            }else if(GameInputManager.MainInstance.RAttack)
            {
                if (!_comboData._hasChildCombo) return;
                if(!_animator.GetBool(AnimationID.IsShowWeaponID)) 
                    GameEventManager.MainInstance.CallEvent("ShowWeapon");
                GameEventManager.MainInstance.CallEvent<float>("SetUnEquipWeaponLayer",0);
                SetComboDate(_comboData.ChildComboAction);
                ExecuteComboAction();
                SetHasLastComboAction(true);
            }
        }
    }

    #region 处决

    //判断能否处决
    private bool CanExcuteFinishAction()
    {
        if (_currentEnemy == null) return false;
        if(_animator.AnimationAtTag("Finish")) return false;
        if (_animator.AnimationAtTag("Attack") &&
            _animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.35f) return false;
        if (!_canFinish) return false;
        return true;
    }
    
    //执行处决输入行为
    private void ExcuteFinishAction()
    {
        if (!CanExcuteFinishAction()) return;
        if (GameInputManager.MainInstance.Finish)
        {
            if(!_animator.GetBool(AnimationID.IsShowWeaponID)) 
                GameEventManager.MainInstance.CallEvent("ShowWeapon");
            GameEventManager.MainInstance.CallEvent<float>("SetUnEquipWeaponLayer",0);
            _animator.CrossFadeInFixedTime(_finishComboData.ActionName,
                0.025f,0,0f);
            GameEventManager.MainInstance.CallEvent<string,Transform,Transform>("ExcuteBeFinishAction",
                _finishComboData.DamageInfos[0].HitName,_currentEnemy,this.transform);
            ResetAllComboInfo();
            _canFinish = false;
        }
    }
    
    //事件
    private void SetCanFinish()
    {
        if (_canFinish) return;
        _canFinish = true;
    }

    #endregion

    #region 位置匹配

    protected override void MatchPosition()
    {
        base.MatchPosition();
        if (_animator.AnimationAtTag("Finish") && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.25f)
        {
            transform.Look(_currentEnemy.position,500f);
            FinishMatchPosition();
        }
    }

    #endregion

    #region 范围检测敌人

    private void GetNearEnemys()
    {
        if (_currentEnemy != null && 
            DevelopmentTools.DistanceForTarget(_currentEnemy, transform) < 1.5f) return;
        _enemyColliders = Physics.OverlapSphere(transform.position + (transform.up * 0.7f), _detectionRadius,
            _layerMask,
            QueryTriggerInteraction.Ignore);

    }
    
    /// <summary>
    /// 在检测到的敌人集合中拿到一个最近的敌人
    /// </summary>
    private void GetCurrentTargetEnemy()
    {
        if (_enemyColliders.Length == 0) return;
        if (_currentEnemy != null && 
            DevelopmentTools.DistanceForTarget(_currentEnemy, transform) < 1.5f) return;
        var distance=Mathf.Infinity;
        Transform tempEnemy = null;
        foreach (var enemyCollider in _enemyColliders)
        {
            var enemyDistance = DevelopmentTools.DistanceForTarget(enemyCollider.transform, transform);
            if ( enemyDistance< distance)
            {
                distance = enemyDistance;
                tempEnemy = enemyCollider.transform;
            }
        }

        if (tempEnemy != null && tempEnemy != _currentEnemy)
        {
            _currentEnemy = tempEnemy;
            _canFinish = false;
        }
        
        
    }
    
    //Gizmos测试
    private void OnDrawGizmos()
    {
       Gizmos.color = Color.green;
       Gizmos.DrawWireSphere(transform.position + (transform.up * 0.7f), _detectionRadius);
    }

    #endregion
}

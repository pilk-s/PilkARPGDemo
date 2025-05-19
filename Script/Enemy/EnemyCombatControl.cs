using System;
using System.Collections;
using System.Collections.Generic;
using GGG.Tool;
using UnityEngine;

public class EnemyCombatControl : CharacterCombatControl
{
    private bool _applyAttackCommand;

    public bool GetAttackCommand() => _applyAttackCommand;
    

    private void Start()
    {
        _currentEnemy = GameManager.MainInstance.GetMainPlayer();
    }



    private void ResetAttackCommand()
    {
        _applyAttackCommand = false;
    }
    private bool CanReceptionAttackCommand()
    {
        if (_animator.AnimationAtTag("Attack")) return false;
        if (_animator.AnimationAtTag("FinishHti")) return false;
        if(_animator.AnimationAtTag("Hit")) return false;
        if (_animator.AnimationAtTag("Parry")) return false;
        
        return true;
    }
    
    public void SetAttackCommand(bool attackCommand)
    {
        if (!CanReceptionAttackCommand())
        {
            ResetAttackCommand();
            return;
        }

        _applyAttackCommand = attackCommand;
    }

    public void ExecuteEnemyAttack()
    {
        if (_applyAttackInput)
        {
            ExecuteComboAction();
            ResetAttackCommand();
        }
        
    }
    
    
}

using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class AICombatAction : Action
{
    private EnemyCombatControl _enemyCombatControl;

    public override void OnAwake()
    {
        _enemyCombatControl = GetComponent<EnemyCombatControl>();
    }

    public override TaskStatus OnUpdate()
    {
        if (_enemyCombatControl.GetAttackCommand())
        {
            _enemyCombatControl.ExecuteEnemyAttack();
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}

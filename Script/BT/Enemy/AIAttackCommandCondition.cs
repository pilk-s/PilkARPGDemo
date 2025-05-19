using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class AIAttackCommandCondition : Conditional
{
    private EnemyCombatControl _enemyCombatControl;

    public override void OnAwake()
    {
        _enemyCombatControl = GetComponent<EnemyCombatControl>();
    }

    public override TaskStatus OnUpdate()
    {
        if(_enemyCombatControl.GetAttackCommand())
            return TaskStatus.Success;
        else 
            return TaskStatus.Failure;
    }
}

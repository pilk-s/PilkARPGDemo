using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using GGG.Tool;
using UnityEngine;

public class AIJustMoveForward : Action
{
    private EnemyMovementControl _enemyMovementControl;
    private float DistanceToTarget() => DevelopmentTools.DistanceForTarget(GameManager.MainInstance.GetMainPlayer(),
        transform);

    public override void OnAwake()
    {
        _enemyMovementControl=GetComponent<EnemyMovementControl>();
    }

    public override TaskStatus OnUpdate()
    {
        if (DistanceToTarget() > 2.5)
        {
            _enemyMovementControl.SetAnimationParameter(0,1);
            return TaskStatus.Running;
        }
        else
        {
            return TaskStatus.Success;
        }
    }
}

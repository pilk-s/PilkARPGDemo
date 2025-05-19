using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using GGG.Tool;
using UnityEngine;

public class AIFreeMovementAction : Action
{
    private EnemyMovementControl _enemyMovementControl;
    private EnemyCombatControl _enemyCombatControl;
    private int _lastDirIndex;
    private int _dirIndex;
    private float _currentTime;
    private float _randomTime;
    private bool _isArrivalCenter;
   
    
    public override void OnAwake()
    {
        _enemyMovementControl = GetComponent<EnemyMovementControl>();
        _enemyCombatControl = GetComponent<EnemyCombatControl>();
        _currentTime = 0;
        _dirIndex = Random.Range(0, 7);
    }

    public override TaskStatus OnUpdate()
    {
        
        if (!_enemyCombatControl.GetAttackCommand())
        {
            
            if (DistanceTarget() > 8f)
            {
                _enemyMovementControl.SetAnimationParameter(0, 1);
            }else if (DistanceTarget() <= 8f && DistanceTarget() >= 1.5f) //3.75 2.75
            {
                if (!_isArrivalCenter)
                {
                    if (DistanceTarget() <= 3.75f && DistanceTarget() >= 2.75f)
                    {
                        _isArrivalCenter = true;
                    }
                    else
                    {
                        _enemyMovementControl.SetAnimationParameter(0, -1);
                    }
                }
                else
                {
                    AIFreeMovement();
                    RandomDirectionIndex();
                }
            }
            else
            {
                _isArrivalCenter = false;
                _enemyMovementControl.SetAnimationParameter(0, -1);
            }

            return TaskStatus.Running;
        }
        
        
        return TaskStatus.Success;
    }

    private float DistanceTarget() =>
        DevelopmentTools.DistanceForTarget(GameManager.MainInstance.GetMainPlayer(), transform);

    private void AIFreeMovement()
    {
        switch (_dirIndex)
        {
            case 0:
                _enemyMovementControl.SetAnimationParameter(0, 0);
                break;
            case 1:
                _enemyMovementControl.SetAnimationParameter(1, 0);
                break;
            case 2:
                _enemyMovementControl.SetAnimationParameter(-1, 0);
                break;
        }
    }
    
    //TODO:指定时间随机Dirindex

    private void RandomDirectionIndex()
    {
        if (_currentTime < 0)
        {
            _lastDirIndex=_dirIndex;
            _randomTime = Random.Range(4, 7);
            _currentTime = _randomTime;
            
            _dirIndex=Random.Range(0,3);
            
            if (_lastDirIndex == _dirIndex)
            {
                _dirIndex=Random.Range(0,3);
            }
        }
        else
        {
            _currentTime -= Time.deltaTime;
        }
    }
}

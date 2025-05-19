using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using GGG.Tool;
using UnityEngine;

public class AIAttackDistanceCondition : Conditional
{
   private float DistanceToTarget() => DevelopmentTools.DistanceForTarget(GameManager.MainInstance.GetMainPlayer(),
      transform);

   public override TaskStatus OnUpdate()
   {
       if (DistanceToTarget() > 2.5f)
       {
           return TaskStatus.Failure;
       }
       else
       {
           return TaskStatus.Success;
       }
   }
}

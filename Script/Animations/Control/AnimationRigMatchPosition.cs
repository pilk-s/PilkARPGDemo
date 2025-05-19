using System;
using System.Collections;
using System.Collections.Generic;
using GGG.Tool;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AnimationRigMatchPosition : MonoBehaviour
{
   private Animator _animator;
   [SerializeField,Header("骨骼约束的名称"),Space(10)]private string _rigName;
   private TwoBoneIKConstraint _rightHandRig;

   private void Awake()
   {

      _animator = GetComponent<Animator>();
      InitRig();

   }

   private void Update()
   {
      UpdateRightHandRigWeight();
   }

   private void InitRig()
   {
      if (_rightHandRig == null)
      {
         var cacheRig=_animator.gameObject.GetComponentInChildren<TwoBoneIKConstraint>();
         if (cacheRig != null && cacheRig.name == _rigName)
         {
            _rightHandRig = cacheRig;
         }
      }
   }

   private void UpdateRightHandRigWeight()
   {
      _rightHandRig.weight = _animator.GetFloat(AnimationID.RightHandRigID);
   }
}

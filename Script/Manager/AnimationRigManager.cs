using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;



public class AnimationRigManager : MonoBehaviour
{
   
    [System.Serializable]
    private class RigItem
    {
        public string _rigName;
        public Rig _rig;    
    }
    
    [SerializeField,Header("约束骨骼列表")]private List<RigItem> _rigItems = new List<RigItem>();
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        UpdateRigWeightFromCurves();
    }

    private void UpdateRigWeightFromCurves()
    {
        if (_rigItems.Count == 0) return;
        foreach (var rigItem in _rigItems)
        {
            rigItem._rig.weight=_animator.GetFloat(rigItem._rigName);
        }
    }
    
}

using System;
using System.Collections;
using System.Collections.Generic;
using GGG.Tool;
using UnityEngine;

namespace piiilk_ARPGDemo.Animation
{
    public enum HoldState
    {
        HoldSword,
        UnHoldSword,
    }

    public class CharacterAnimationControl : MonoBehaviour
    {
        private Animator _animator;
        
        [SerializeField,Header("角色当前的持剑状态"),Space(10)]private HoldState _holdState = HoldState.UnHoldSword;
        [SerializeField,Header("背后的剑"),Space(10)]private Transform _behindSword;
        [SerializeField,Header("手上的剑"),Space(10)]private Transform _HandsSword;
        
        /// <summary>
        /// 动画约束
        /// </summary>
        

        private void Awake()
        {
            _animator=GetComponent<Animator>();
            _holdState=HoldState.UnHoldSword;
            
            //设置初始人物剑的显示状态
            _behindSword.gameObject.SetActive(true);
            _HandsSword.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (GameInputManager.MainInstance.TakeOut)
            {
                SwitchHoldState();
            }
        }

        private bool EnableSwitchHoldState()
        {
            if (_animator.AnimationAtTag("SwitchWeapon",1)) return false;
            if (_animator.GetCurrentAnimatorStateInfo(1).normalizedTime < .35f) return false;
            return true;
        }

        private void SwitchHoldState()
        {
            if (!EnableSwitchHoldState()) return;

            _holdState = ((_holdState == HoldState.UnHoldSword) ? HoldState.HoldSword : HoldState.UnHoldSword);
            
            
            // switch (_holdState)
            // {
            //     case HoldState.HoldSword:
            //         _animator.SetLayerWeight(1,1);
            //         _animator.CrossFadeInFixedTime(AnimationID.PullOutSwordID,.25f,
            //             GameConstant.TakeOutSwordLayer,0);
            //         
            //         //TODO:将但动画层级过渡到持剑的动画层级 将当前的BaseLayer权重调整到最低 
            //         
            //         break;
            //     case HoldState.UnHoldSword:
            //         _animator.SetLayerWeight(1,1);
            //         _animator.CrossFadeInFixedTime(AnimationID.PullBackSwordID,.25f,
            //             GameConstant.TakeOutSwordLayer,0);
            //         
            //         //TODO:将但前持剑的动画层级权重降低 过渡到BaseLayer层级
            //         break;
            // }
        }

        #region 事件

        private void SwitchHandsSword()
        {
            switch (_holdState)
            {
                case HoldState.HoldSword:
                    _behindSword.gameObject.SetActive(false);
                    _HandsSword.gameObject.SetActive(true);
                    break;
                case HoldState.UnHoldSword:
                    _behindSword.gameObject.SetActive(true);
                    _HandsSword.gameObject.SetActive(false);
                    break;
            }
        }

        #endregion
    }

}
using System;
using System.Collections;
using System.Collections.Generic;
using GGG.Tool;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
  private Animator _animator;
  [SerializeField,Header("背后的剑"),Space(10)]private Transform _behindSword;
  [SerializeField,Header("手上的剑"),Space(10)]private Transform _HandsSword;
  private void Awake()
  {
    _animator=GetComponent<Animator>();
    
    //初始不显示武器
    _animator.SetBool(AnimationID.IsShowWeaponID,false);
    
    //设置初始人物剑的显示状态
    _behindSword.gameObject.SetActive(true);
    _HandsSword.gameObject.SetActive(false);
  }

  private void OnEnable()
  {
    GameEventManager.MainInstance.AddEventListening("ShowWeapon",ShowWeapon);
    GameEventManager.MainInstance.AddEventListening("SwitchWeaponShow",SwitchWeaponShow);
    GameEventManager.MainInstance.AddEventListening<float>("SetUnEquipWeaponLayer",SetUnEquipWeaponLayer);
  }

  private void OnDisable()
  {
    GameEventManager.MainInstance.RemoveEvent("ShowWeapon",ShowWeapon);
    GameEventManager.MainInstance.RemoveEvent("SwitchWeaponShow",SwitchWeaponShow);
    GameEventManager.MainInstance.RemoveEvent<float>("SetUnEquipWeaponLayer",SetUnEquipWeaponLayer);
  }

  private void Update()
  {
    SwtichHoldState();
  }

  private bool EnableSwitchHoldState()
  {
    if (!_animator.AnimationAtTag("Motion")) return false;
    if (_animator.GetFloat(AnimationID.MovementID) >0.15f) return false;
    if (_animator.AnimationAtTag("Equip")) return false;
    // if (_animator.GetCurrentAnimatorStateInfo(1).normalizedTime < .35f) return false;
    return true;
  }

  private void SwtichHoldState()
  {
    if (!EnableSwitchHoldState()) return;
    if (!_animator.GetBool(AnimationID.IsShowWeaponID))
    {
      if (GameInputManager.MainInstance.TakeOut)
      {
        _animator.SetBool(AnimationID.IsShowWeaponID,true);
        _animator.CrossFadeInFixedTime(AnimationID.EquipWPID,.5f,0,0f);
      }
    }
    else
    {
      if (GameInputManager.MainInstance.TakeOut)
      {
        _animator.SetBool(AnimationID.IsShowWeaponID,false);
        _animator.CrossFadeInFixedTime(AnimationID.UnEquipWPID,.5f,0,0f);
      }
     
    }
  }
  
  
  
  #region 事件

  private void SwitchWeaponShow()
  {
    if (_animator.GetBool(AnimationID.IsShowWeaponID))
    {
      _behindSword.gameObject.SetActive(false);
      _HandsSword.gameObject.SetActive(true);
    }
    else
    {
      _behindSword.gameObject.SetActive(true);
      _HandsSword.gameObject.SetActive(false);
    }
  }

  private void ShowWeapon()
  {
    if (_animator.GetBool(AnimationID.IsShowWeaponID) == false)
    {
      _behindSword.gameObject.SetActive(false);
      _HandsSword.gameObject.SetActive(true);
      _animator.SetBool(AnimationID.IsShowWeaponID,true);
    }
  }
  #endregion


  #region  攻击时不再播放受到动画
  
  //设置收刀动画层的权重
  private void SetUnEquipWeaponLayer(float weight)
  {
    _animator.SetLayerWeight(1,weight);
  }

  #endregion
}

using System;
using System.Collections;
using System.Collections.Generic;
using GGG.Tool;
using piiilk_ARPGDemo.HealthBase;
using UnityEngine;

public class CharacterHealthBase : MonoBehaviour
{
  private Transform _currentTarget;
  protected Animator _animator;
  [SerializeField,Header("生命值信息模板")]private CharacterHealthInfo _characterHealthInfoTemplate;
  protected CharacterHealthInfo _characterHealthInfo;

  private void Awake()
  {
    _characterHealthInfo=Instantiate(_characterHealthInfoTemplate);
    _characterHealthInfo.InitCharacterHealthInfo();
    _animator=GetComponent<Animator>();
  }

  protected virtual void Update()
  {
    //测试
    LookAtTarget();
  }

  private void OnEnable()
  {
    GameEventManager.MainInstance.AddEventListening<float,string,string,Transform,Transform>("TriggerDamage",TriggerDamage);
    GameEventManager.MainInstance.AddEventListening<string,Transform,Transform>("ExcuteBeFinishAction",ExcuteBeFinishAction);
  }

  private void OnDisable()
  {
    GameEventManager.MainInstance.RemoveEvent<float,string,string,Transform,Transform>("TriggerDamage",TriggerDamage);
    GameEventManager.MainInstance.RemoveEvent<string,Transform,Transform>("ExcuteBeFinishAction",ExcuteBeFinishAction);
  }

  protected virtual void CharacterHitAction(float  damage,string hitName,string parryName)
  {
    
  }
  
  protected  virtual void TriggerDamage(float  damage,string hitName,string parryName,Transform self,Transform target)
  {
    if (self != this.transform) return;
    _currentTarget = target;
    
    CharacterHitAction(damage,hitName,parryName);
  }

  #region 被处决

  private void ExcuteBeFinishAction(string finishName,Transform self,Transform target)
  {
    if (self != this.transform) return;
    _currentTarget = target;
    _animator.Play(finishName);
  }

  #endregion
  
  //测试
  //被攻击是看向玩家
  private void LookAtTarget()
  {
    if (_animator.AnimationAtTag("Hit")||_animator.AnimationAtTag("Block"))
    {
      transform.Look(_currentTarget.position,500f);
    }
  }
  
  public CharacterHealthInfo GetCharacterHealthInfo()=>_characterHealthInfo;
}

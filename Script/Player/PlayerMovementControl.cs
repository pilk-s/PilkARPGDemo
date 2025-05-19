using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  GGG.Tool;
using UnityEngine.Serialization;


public enum MovementState
{
    Lock,
    UnLock,
}

namespace piiilk_ARPGDemo.Movement
{
   public class PlayerMovementControl : CharacterMovementControlBase
   {
       private Transform _mainCmera;
       [SerializeField]private float mouseDelta;
       [SerializeField] private float _characterCornerSpeed=1.5f;
       
       
       private float _rotationAngle;
       private float _angleVelocity;
       [SerializeField,Header("角色转角平滑时间")]private float _rotationSmoothTime=0.2f;
       private Vector3 _moveDirection;
       private Vector3 _characterTargetDirection;
       [SerializeField]private bool _canSpinning=true;
       private float _smoothAngle;
       private float _inputX;
       private float _inputY;
       
       
       [SerializeField,Header("当前角色控制器的状态")]private MovementState _currentMovementState = MovementState.UnLock;

       
       /// <summary>
       /// 脚步声
       /// </summary>
       private float _nextFootTime;
       [SerializeField] private float _slowFootTime;
       [SerializeField] private float _fastFootTime;

       /// <summary>
       /// 测试
       /// </summary>
       [SerializeField]private Transform _lockTarget;
       
       
       
       protected override void Awake()
       {
           base.Awake();
           _mainCmera = Camera.main.transform;
           
           _currentMovementState=MovementState.UnLock; //这种默认状态移动状态为不锁定
       }

       protected override void Update()
       {
           base.Update();
           UpdateLock();

           //测试
           {
                SetLockStatus();
                if (_lockTarget != null)
                {
                    UpdatecCharacterForwardToTarget(_lockTarget);
                }
              
           }
           //测试
          
       }

       protected override void OnEnable()
       {
           base.OnEnable();
           GameEventManager.MainInstance.AddEventListening<bool>("SetCanSpinning",SetCanSpinning);
           GameEventManager.MainInstance.AddEventListening("ResetCharacterDash",ResetCharacterDash);
       }

       protected override void OnDisable()
       {
           base.OnDisable();
           GameEventManager.MainInstance.RemoveEvent<bool>("SetCanSpinning",SetCanSpinning);
           GameEventManager.MainInstance.RemoveEvent("ResetCharacterDash",ResetCharacterDash);
       }

       private void LateUpdate()
       {
           UpdateCharacterAnimation();
           CharacterRotationControl();
       }


       #region 角色旋转控制

       private void CharacterRotationControl()
       {
           if (!_isDetectGround) return;
           if (!_canSpinning) return;
           if (_currentMovementState==MovementState.Lock) return;
           
           if (_animator.GetBool(AnimationID.HasInputID) && _animator.AnimationAtTag("Motion"))
           {
               var cameraForward = _mainCmera.forward;
               cameraForward.Set(cameraForward.x,0,cameraForward.z);
               var cameraRight = _mainCmera.right;
               cameraRight.Set(cameraRight.x,0,cameraRight.z);

               _inputX = GameInputManager.MainInstance.Movement.x;
               _inputY = GameInputManager.MainInstance.Movement.y;

               // {//测试
               //     DevelopmentTools.WTF("Inputx:"+_inputX+" Inputy:"+_inputY);
               // }
               
               
               
               _moveDirection=cameraForward*_inputY+cameraRight*_inputX;
               float targetAngle = Mathf.Atan2(_moveDirection.x, _moveDirection.z) * Mathf.Rad2Deg;
               
               _characterTargetDirection=Quaternion.Euler(0f,targetAngle,0f)*Vector3.forward;
               if (!_animator.AnimationAtTag("TurnRun"))
               {
                   _smoothAngle = targetAngle;
                   _animator.SetFloat(AnimationID.DeltaAngleID,DevelopmentTools.GetDeltaAngle(transform,_characterTargetDirection.normalized));
               }
               
               _smoothAngle = Mathf.SmoothDampAngle(
                   transform.eulerAngles.y,
                   targetAngle,
                   ref _angleVelocity,
                   _rotationSmoothTime
               );
               transform.rotation = Quaternion.Euler(0, _smoothAngle, 0);
               
           }
           
       } 

       #endregion

       #region 更新人物移动动画

       private void UpdateCharacterAnimation()
       {
           if (!_isDetectGround) return; 
           
           _animator.SetBool(AnimationID.HasInputID,GameInputManager.MainInstance.Movement!=Vector2.zero);

           if (_animator.GetBool(AnimationID.HasInputID))
           {
               switch (_currentMovementState)
               {
                   case MovementState.UnLock:
                       UpdateUnLockCharacteraAnimation();
                       break;
                   
                   case MovementState.Lock:
                       ExcuteLockCharacterAnimation();
                       break;
               }
           }
           else
           {
               _animator.SetFloat(AnimationID.MovementID,0f,0.25f,Time.deltaTime);

               if (_animator.GetFloat(AnimationID.MovementID) < 0.2f)
               {
                   _animator.SetBool(AnimationID.RunID,false);
               }
           }
       }

       #endregion

       #region 事件
       
       //设置当前人物能否旋转
       private void SetCanSpinning(bool value)
       {
           _canSpinning = value;
       }
       
        
       //重置Dash参数
       private void ResetCharacterDash()
       {
           _animator.SetBool(AnimationID.DashID,false);
           _animator.SetFloat(AnimationID.DashVerticalID,0f);
           _animator.SetFloat(AnimationID.DashHorizontalID,0f);
       }

       #endregion

       #region Dash冲刺

       private bool EnableCharacterDash()
       {
           if(!_animator.GetBool(AnimationID.HasInputID)) return false;
           if (_animator.AnimationAtTag("Dash")) return false;
           // if(_currentMovementState==MovementState.UnLock) return false;
           
           return true;
       }
       
       private void ExcuteLockCharacterDash()
       {
           if (GameInputManager.MainInstance.Dash)
           {
               _animator.SetFloat(AnimationID.DashVerticalID,_inputY);
               _animator.SetFloat(AnimationID.DashHorizontalID,_inputX);
               
               _animator.SetBool(AnimationID.DashID,true);
           }
       }

       private void ExcuteUnLockCharacterDash()
       {
           if(!_animator.GetBool(AnimationID.HasInputID)) return;
           if (_animator.AnimationAtTag("Dash")) return;
           if (_currentMovementState==MovementState.Lock) return;

           if (GameInputManager.MainInstance.Dash)
           {
               _animator.SetBool(AnimationID.DashID,true);
           }
       }
       #endregion

       private void UpdateLock()
       {
           switch (_currentMovementState)
           {
               case MovementState.UnLock:
                   _animator.SetFloat(AnimationID.LockID,GameConstant.UnLockEnemy);
                   break;
               case MovementState.Lock:
                   _animator.SetFloat(AnimationID.LockID,GameConstant.LockEnemy);
                   break;
           }
       }

       #region 人物锁敌状态切换(测试)

       //判断当前是否能切换移动动画的动画形式  
       private bool EnableSwitchMovementState()
       {
           if(!_animator.AnimationAtTag("Motion")) return false;
           if (GameInputManager.MainInstance.Movement != Vector2.zero) return false;
           
           return true;
       }
       
       //切换移动动画的动画形式
       private void SetLockStatus()
       {
           if (EnableSwitchMovementState())
           {
               if (GameInputManager.MainInstance.Lock)
               {    
                   _currentMovementState=(_currentMovementState==MovementState.UnLock)?MovementState.Lock:MovementState.UnLock;
                   DevelopmentTools.WTF("当前动画状态为:"+_currentMovementState);
                   ResetStatusParameter();
               }
           }
       }

       private void ResetStatusParameter()
       {
           _inputX = 0f;
           _inputY = 0f;
           
           _animator.SetBool(AnimationID.RunID,false);
           _animator.SetBool(AnimationID.DashID,false);
           
       }

       #endregion

       #region UnLock及Lock的按键输入的动画状态的切换   (测试)

       private void UpdateUnLockCharacteraAnimation()
       {
           if (GameInputManager.MainInstance.Run)
           {
               _animator.SetBool(AnimationID.RunID,true);
           }
           else
           {
               _animator.SetBool(AnimationID.RunID,false);
           }
           _animator.SetFloat(AnimationID.MovementID,_animator.GetBool(AnimationID.RunID)?
               2f:1f,0.25f,Time.deltaTime);
               
           SetCharacterFootTime();
           if (EnableCharacterDash())
           {
                ExcuteUnLockCharacterDash();
           }
           
       }

       private void ExcuteLockCharacterAnimation()
       {
           _inputX = GameInputManager.MainInstance.Movement.x;
           _inputY = GameInputManager.MainInstance.Movement.y;
           
           _animator.SetFloat(AnimationID.HorizontalID,_inputX,0.25f,Time.deltaTime);
           _animator.SetFloat(AnimationID.VerticalID,_inputY,0.25f,Time.deltaTime);
           SetCharacterFootTime();
           ExcuteLockCharacterDash();
       }

       #endregion

       #region Lock时时刻看向敌人(测试)

       private void UpdatecCharacterForwardToTarget(Transform target)
       {
           if(_currentMovementState==MovementState.UnLock) return;
           transform.Look(target.position,500f);
       }

       #endregion

       #region 脚步声

       /// <summary>
       /// 设置并执行播放脚步声
       /// </summary>
       private void SetCharacterFootTime()
       {
           if (_isDetectGround && _animator.GetFloat(AnimationID.MovementID) > .5f &&
               _animator.AnimationAtTag("Motion"))
           {
               _nextFootTime-=Time.deltaTime;

               if (_nextFootTime < 0f)
               {
                   PlayFootSound();
               }
           }
           else
           {
               _nextFootTime=0f;
           }
       }
       
       private void PlayFootSound()
       {
           GamePoolManager.MainInstance.TryGetPoolItem("FootSound",transform.position,Quaternion.identity);
           _nextFootTime = _animator.GetFloat(AnimationID.MovementID) > 1.1f ? _fastFootTime :_slowFootTime;
       }

       #endregion

       #region 回复收刀动画权重

       private void ResetUnEquipWeaponLayerWeight()
       {
           
       }

       #endregion
       
   }

}
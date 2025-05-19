using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace piiilk_ARPGDemo.Movement
{
    public class CharacterMovementControlBase : MonoBehaviour
    {
        protected Animator _animator;
        protected bool _isDetectGround;
        private CharacterController _control;
        protected Vector3 _basemMoveDirection;
        
        
        [SerializeField, Header("地面检测偏移量"), Space(10)]
        private float _detectGroundOffset;
        [SerializeField, Header("球形地面检测范围"), Space(10)]
        private float _detectGroundRadius;
        [SerializeField,Header("地面检测层级")]
        private LayerMask _groundLayerMask;

        
        private  readonly float _gravity=-9.81f;
        private bool _EnableGravity;
        private float _characterVerticalVelocity;
        private float _characterVerticalMaxVelocity=45;
        private float _fallOutDeltaTime=0.15f;  
        private float _fallOutTime;
        private Vector3 _characterVerticalDirection;
        
        
        protected virtual void Awake()
        {
            _animator = GetComponent<Animator>();
            _control = GetComponent<CharacterController>();
            _EnableGravity = true;
        }

        protected void Start()
        {
            //隐藏鼠标
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        protected virtual void OnEnable()
        {
            
        }

        protected virtual void OnDisable()
        {
            
        }
        
        

        protected virtual void Update()
        {
            SetCharacterVerticalVelocity();
            UpdateCharacterVerticalVelocity();
        }

        protected virtual void OnAnimatorMove()
        {
            _animator.ApplyBuiltinRootMotion();
            UpdateCharacterMoveDirection(_animator.deltaPosition);
        }

        #region 地面检测

        private bool GroundDetection()
        {
            var detectionPosition=new Vector3(transform.position.x,
                transform.position.y-_detectGroundOffset,transform.position.z);
            return Physics.CheckSphere(detectionPosition,_detectGroundRadius,
                _groundLayerMask,QueryTriggerInteraction.Ignore);
        }
        
        #endregion

        #region 重力
        
        //设置角色重力
        private void SetCharacterVerticalVelocity()
        {
            _isDetectGround=GroundDetection();
            if (_isDetectGround)
            {
                _fallOutTime = _fallOutDeltaTime;
                if (_characterVerticalVelocity < 0)
                {
                    _characterVerticalVelocity = -2;
                }
            }
            else
            {
                if (_fallOutTime > 0)
                {
                    _fallOutTime-=Time.deltaTime;
                }
                else
                {
                    //TODO:下落动画
                }

                if (_characterVerticalVelocity < _characterVerticalMaxVelocity && _EnableGravity)
                {
                    _characterVerticalVelocity+=_gravity*Time.deltaTime;
                }
            }
        }
        
        //更新角色重力
        private void UpdateCharacterVerticalVelocity()
        {
            if (!_EnableGravity) return;
            _characterVerticalDirection.Set(0,_characterVerticalVelocity,0); 
            _control.Move(_characterVerticalDirection*Time.deltaTime);
        }
        #endregion
        
        #region 坡道检测

        //坡道检测
        private Vector3 SlopResetDirection(Vector3 moveDirection)
        {
            if (Physics.Raycast(transform.position + (transform.up * 0.5f)
                    , Vector3.down, out var hit, _control.height, _groundLayerMask, QueryTriggerInteraction.Ignore))
            {
                if (Vector3.Dot(Vector3.up, hit.normal) != 0)
                {
                    return Vector3.ProjectOnPlane(moveDirection, hit.normal);
                }
            }
            
            return moveDirection;
        }

        #endregion

        #region Gizmos

        private void OnDrawGizmos()
        {
            var detectionPosition = new Vector3(transform.position.x,
                transform.position.y - _detectGroundOffset, transform.position.z);
            Gizmos.color=Color.green;
            Gizmos.DrawWireSphere(detectionPosition,_detectGroundRadius);
        }

        #endregion

        #region 更新移动方向

        protected void UpdateCharacterMoveDirection(Vector3 direction)
        {
            _basemMoveDirection=SlopResetDirection(direction);
            _control.Move(_basemMoveDirection*Time.deltaTime);
        }

        #endregion
    }

}
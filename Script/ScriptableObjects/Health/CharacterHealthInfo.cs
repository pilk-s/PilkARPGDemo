using System.Collections;
using System.Collections.Generic;
using GGG.Tool;
using UnityEngine;

namespace piiilk_ARPGDemo.HealthBase
{
    [CreateAssetMenu(fileName = "New Character Health Info", menuName = "ScriptableObjects/Character Health")]
    public class CharacterHealthInfo : ScriptableObject
    {   
        private float _maxHP;
        private float _maxStrength;
        private float _currentHP;
        private float _currentStrength;
        private bool _strenthFull;
        private bool _isDie => (_currentHP <= 0);
        
        public float MaxHP=>_maxHP;
        public float MaxStrength => _maxStrength;
        public float CurrentHP => _currentHP;
        public float CurrentStrength => _currentStrength; 
        public bool StrenthFull => _strenthFull;
        public bool IsDie => _isDie;
        
        [SerializeField]private CharacterHealthBaseData  _characterHealthBase;
        
        public void InitCharacterHealthInfo()
        {
            _maxHP=_characterHealthBase.MaxHealth;
            _maxStrength=_characterHealthBase.MaxStrength;
            _currentHP = _maxHP;
            _currentStrength = _maxStrength;
            _strenthFull = true;
        }
        
        #region 扣除

        //扣除伤害
        public void Damage(float damage,bool hasParry=false)
        {
            if (_strenthFull&&hasParry)
            {
                _currentStrength=Clmap(_currentStrength, damage,0f,_maxStrength);
    
                if (_currentStrength <= 0)
                {
                    _strenthFull = false;
                }
            }
            else
            {
                _currentHP = Clmap(_currentHP, damage,0f,_maxHP);
                //测试
                DevelopmentTools.WTF("但前敌人血量:"+_currentHP);
            }
            
         
        }
        
        //扣除体力值
        public void DamageToStrenth(float damage)
        {
            if (_strenthFull)
            {
                _currentStrength=Clmap(_currentStrength, damage,0f,_maxStrength);
                //测试
                DevelopmentTools.WTF("当前敌人体力值:"+_currentStrength);
                if (_currentStrength <= 0)//体力值小于0时 将体力充沛状态置为false
                {
                    _strenthFull = false;
                }
            }
        }

        #endregion
        
        #region 回复
        
        //回复实生命值
        public void AddHp(float hp)
        {
            _currentHP=Clmap(_currentHP,hp,0f,_maxHP,true);
        }
        
        //回复体力值
        public void AddStrength(float strength)
        {
            _currentStrength=Clmap(_currentStrength,strength,0f,_maxStrength,true);
            
            if(Mathf.Approximately(_currentStrength, _maxStrength))
                _strenthFull = true;
        }

        #endregion

        #region 工具

        private float Clmap(float value,float offsetValue,float minValue,float maxValue,bool add=false)
        {
            return Mathf.Clamp(add?value+offsetValue:value-offsetValue, minValue, maxValue);
        }

        #endregion
    }
}

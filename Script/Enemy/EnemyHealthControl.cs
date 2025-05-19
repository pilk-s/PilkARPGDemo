using System.Collections;
using System.Collections.Generic;
using GGG.Tool;
using UnityEngine;

namespace piiilk_ARPGDemo.Health
{
    public class EnemyHealthControl : CharacterHealthBase
    {
        protected override void CharacterHitAction(float damage, string hitName, string parryName)
        {
            //当攻击力伤害小于一定值时 且 体力充沛时 敌人格挡
            if (_characterHealthInfo.StrenthFull&&damage<30)
            {
                if (!_animator.AnimationAtTag("Attack"))
                {
                    _animator.Play(parryName,0,0f);
                    //TODO:播放格挡音乐
                    _characterHealthInfo.DamageToStrenth(damage);
                }
            }
            else
            {
                _animator.Play(hitName,0,0f);
                //TODO:播放受击打音乐
                _characterHealthInfo.Damage(damage);

                if (_characterHealthInfo.CurrentHP < 20)
                {
                    GameEventManager.MainInstance.CallEvent("SetCanFinish");
                }
            }
        }
    }

}
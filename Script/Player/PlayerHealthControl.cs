using System.Collections;
using System.Collections.Generic;
using GGG.Tool;
using UnityEngine;

public class PlayerHealthControl : CharacterHealthBase
{
    protected override void Update()
    {
        base.Update();
        PlayerBlockInput();
    }

    //TODO:伤害触发 
    
    //TODO:受伤行为
    protected override void CharacterHitAction(float damage, string hitName, string parryName)
    {
        if (_animator.AnimationAtTag("Finish")) return;
        if (_animator.GetBool(AnimationID.BlockID) && damage < 30)
        {
            _animator.CrossFadeInFixedTime(parryName,0.025f,0,0f);
            _characterHealthInfo.DamageToStrenth(damage);
            //TODO:播放格挡音效 及格挡粒子
        }
        else
        {
            _animator.CrossFadeInFixedTime(hitName,0.025f,0,0f);
            _characterHealthInfo.Damage(damage);
            //TODO:播放受击音效 及受击打粒子
        }
    }
    //TODO:格挡输入

    private void PlayerBlockInput()
    {
        if (!CanBlock()) return;
        _animator.SetBool(AnimationID.BlockID, GameInputManager.MainInstance.Block);
    }

    private bool CanBlock()
    {
        if (_animator.AnimationAtTag("Attack") &&
            _animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.35f) return false;
        if (_animator.AnimationAtTag("Block")) return false;

        return true;
    }
}

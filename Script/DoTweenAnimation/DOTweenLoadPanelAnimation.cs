using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
public class DOTweenLoadPanelAnimation : BasePanel
{
    [SerializeField] private TextMeshProUGUI loadingText;
    [SerializeField]private RectTransform starImage;
    private DOTweenTMPAnimator dotweenTmpAnimator;
    private Sequence sequence;
    private void Awake()
    {
        // loadingText.alpha = 0;
        dotweenTmpAnimator = new DOTweenTMPAnimator(loadingText);
    }

    private void OnEnable()
    {
        GameEventManager.MainInstance.AddEventListening<float>("SetStarImageSizeDelta",SetStarImageSizeDelta);
    }
    
    private void OnDisable()
    {
        GameEventManager.MainInstance.RemoveEvent<float>("SetStarImageSizeDelta",SetStarImageSizeDelta);
        DOTween.KillAll();
        sequence.Kill();
    }

    private void Start()
    {
        ExcuteAniamtion();
    }

    private void ExcuteAniamtion()
    {
        // for (int i = 0; i < dotweenTmpAnimator.textInfo.characterCount; i++)
        // {
        //     sequence
        //         .Append(dotweenTmpAnimator.DOFadeChar(i, 1f, 0.2f))
        //         .Join(dotweenTmpAnimator.DOPunchCharScale(i, 1f, 0.2f));
        // }
        // sequence
        //     .SetLoops(-1, LoopType.Yoyo)
        //     .Play();
        
        Debug.Log("DoTween动画被执行");
    }
 

    /// <summary>
    /// 设置星星图片的大小来实现动效
    /// </summary>
    /// <param name="value"></param>
    private void SetStarImageSizeDelta(float value)
    {
        if (starImage != null)
        {
            starImage.localScale=Vector3.one*0.5f*value;
        }
    }
}

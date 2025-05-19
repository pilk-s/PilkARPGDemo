using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Playables;
using UnityEngine.UI;
public class DOTweenAnimationTest : BasePanel
{
   private Sequence sequence;
   [SerializeField,Header("项目名称")]private TextMeshProUGUI textMeshProUGUI;
   [SerializeField,Header("目标颜色")]private Color targetColor;
   [SerializeField,Header("Timeline导演")]private PlayableDirector playableDirector;
   [SerializeField,Header("背景图片")]private Image backgroundImage;
   [SerializeField,Header("介绍信息的Canvas Group")]private CanvasGroup introduceCanvasGroup;
   [SerializeField,Header("刀1的图片")]private RectTransform knifeImage_1;
   [SerializeField,Header("刀2的图片")]private RectTransform knifeImage_2;
   [SerializeField,Header("按下任意键提醒的文本")]private TextMeshProUGUI enterGameRemindText;
   [SerializeField,Header("TopPanel")]private RectTransform TopPanel;
   [SerializeField,Header("CenterPanel")]private RectTransform CenterPanel;
   [SerializeField,Header("Button列表")]private List<Button> _buttons = new List<Button>();
   [SerializeField,Header("Button出来的起始位置")]private Transform startPosition;
   [SerializeField,Header("作者Info")]private TextMeshProUGUI authorInfo;
   
   private DOTweenTMPAnimator dotweenTmpAnimator;
   private bool isPlaying;
   private bool isMove;
   private void Start()
   {
      InitDoTweenAnimation();
      isPlaying = false;
      isMove = false;
   }

   private void OnDisable()
   {
      DOTween.KillAll();
      sequence.Kill();
   }


   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.Space)&&!isPlaying)
      {
         isPlaying = true;
         sequence = DOTween.Sequence();
         sequence.SetAutoKill(false);
         sequence
            .Append(textMeshProUGUI.DOFade(1, 1f))
            .AppendCallback(() => playableDirector.Play());

         sequence
            .Append(backgroundImage.DOFade(0f, 2f))
            .Join(textMeshProUGUI.DOColor(targetColor, 2f));

         sequence
            .Append(introduceCanvasGroup.DOFade(1f, 1.5f))
            .Join(authorInfo.DOFade(1f, 1.5f));
            
         knifeImage_1
            .DOScale(Vector2.one*1.25f, 1f)
            .SetLoops(-1, LoopType.Yoyo)
            .Play();
         
         knifeImage_2
            .DOScale(Vector2.one*1.25f, 1f)
            .SetLoops(-1, LoopType.Yoyo)
            .Play();

         for (int i = 0; i < dotweenTmpAnimator.textInfo.characterCount; i++)
         {
            if (i == dotweenTmpAnimator.textInfo.characterCount - 1)
            {
               sequence
                  .Append(dotweenTmpAnimator.DOFadeChar(i, 1f, 0.2f))
                  .Join(dotweenTmpAnimator.DOPunchCharScale(i, 1f, 0.2f))
                  .AppendInterval(0.2f)
                  .AppendCallback(() =>
                  {
                     enterGameRemindText.DOFade(0.5f,1f)
                        .SetLoops(-1, LoopType.Yoyo)
                        .Play();
                     isMove = true;
                     Debug.Log("当前可以移动UI");
                  });
            }
            else
            {
               sequence
                  .Append(dotweenTmpAnimator.DOFadeChar(i, 1f, 0.2f))
                  .Join(dotweenTmpAnimator.DOPunchCharScale(i,1f, 0.2f));
            }
            
         }
      }
      
      //按下任意键后移动UI  introduceCanvasGroup textMeshProUGUI
      if (Input.anyKeyDown&&isMove)
      {
         Debug.Log("移动UI被执行");
         isMove = false;
         // introduceCanvasGroup.transform.DOMove(TopPanel.transform.position, 0.5f).SetRelative();
         // textMeshProUGUI.transform.DOMove(TopPanel.transform.position, 0.5f).SetRelative();

         enterGameRemindText.gameObject.SetActive(false);
         CenterPanel.GetComponent<CanvasGroup>().alpha = 1;
         sequence
            .Append(introduceCanvasGroup.transform.parent.DOMove(TopPanel.transform.position, 1f))
            .Join(introduceCanvasGroup.transform.parent.DOScale(Vector3.one * 0.85f, 1f))
            .Join(CenterPanel.DOMoveY(transform.position.y - 500, 1f).From());

         foreach (var button in _buttons)
         {
            sequence
               .Append(button.GetComponent<CanvasGroup>().DOFade(1f, 1f));

         }

         
      }
   }
   
   

   private void InitDoTweenAnimation()
   {
      textMeshProUGUI.alpha = 0;
      introduceCanvasGroup.alpha = 0;
      dotweenTmpAnimator = new DOTweenTMPAnimator(enterGameRemindText);
      enterGameRemindText.alpha = 0;
      CenterPanel.GetComponent<CanvasGroup>().alpha = 0;
      foreach (var button in _buttons)
      {
         button.GetComponent<CanvasGroup>().alpha = 0f;
      }

      authorInfo.alpha = 0;
   }

   private void ActivationButton()
   {
      foreach (var button in _buttons)
      {
         button.gameObject.SetActive(true);
      }
   }
}

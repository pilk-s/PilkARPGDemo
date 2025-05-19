using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ButtonTest : MonoBehaviour
{
   [SerializeField]private Button _but1;
   [SerializeField]private CanvasGroup _mainMenuCanvasGroup;
   [SerializeField]private RectTransform _closing1;
   [SerializeField]private RectTransform _closing2;
   private void Start()
   {
      _but1.onClick.AddListener(StartGame);
 
   }
     
   private void StartGame()
   {
      Debug.Log("StartGame");
   
      // _mainMenuCanvasGroup.DOFade(0.5f, 0.5f);
      _closing1.DOMoveY(_closing1.position.y-600, 3f);

      _closing2
         .DOMoveY(_closing2.position.y + 600, 3f)
         .OnComplete(() =>
         {
            //TODO:开启场景加载过渡的Panel
            
            UIManager.Instance.ClosePanel(UIManager.UIconst.MainMenuPanel);
            UIManager.Instance.OpenPanel(UIManager.UIconst.LoadingPanel);
            //TODO:调用场景管理器异步加载场景
            SceneMgr.MainInstance.LoadSceneAsync("SampleScene",NullFunctionTest);
           Debug.Log("场景加载中");
         });

      // // _closing1
   }

   private void NullFunctionTest()
   {
      Debug.Log("空函数被执行");
      // UIManager.Instance.ClosePanel(UIManager.UIconst.LoadingPanel);
   }
}

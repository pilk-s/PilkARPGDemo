using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitMainMenuScene : MonoBehaviour
{
    private void Awake()
    {
            UIManager.Instance.OpenPanel(UIManager.UIconst.MainMenuPanel);
            Debug.Log("打开主主菜单被执行");
    }
}

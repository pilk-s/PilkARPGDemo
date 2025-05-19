using System;
using System.Collections;
using System.Collections.Generic;
using GGG.Tool.Singleton;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]private Transform _mainPlayer;
    protected override void Awake()
    {
      
        _mainPlayer = GameObject.FindGameObjectWithTag("Player").transform;
      
    }
    private void Start()
    {
       
        
    }

    public void SetMainPlayer(Transform player)
    {
        _mainPlayer = player;
    }

    #region 共享参数

    public Transform GetMainPlayer()=>_mainPlayer;

    #endregion
}

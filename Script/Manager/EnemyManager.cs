using System;
using System.Collections;
using System.Collections.Generic;
using GGG.Tool.Singleton;
using piiilk_ARPGDemo.Health;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    [SerializeField,Header("当前管理的敌人")] private Transform _currrentEnemy;
    
    private EnemyCombatControl _enemyCombatControl;
    private EnemyHealthControl _enemyHealthControl;
    [SerializeField]private bool _toAttackPlayer;
    private WaitForSeconds _waitForSeconds;
    protected override void Awake()
    {
        base.Awake();
        _enemyCombatControl =_currrentEnemy.GetComponent<EnemyCombatControl>();
        _enemyHealthControl = _currrentEnemy.GetComponent<EnemyHealthControl>();
        _waitForSeconds=new WaitForSeconds(8f);
    }

    private void Start()
    {
        StartCoroutine(SendAttackCommand());
    }

    private void Update()
    {
        if (_enemyHealthControl.GetCharacterHealthInfo().IsDie)
        {
            StopAllAttackCommand();
        }
    }
    
    IEnumerator SendAttackCommand()
    {
        while (_currrentEnemy != null)
        {
            _enemyCombatControl.SetAttackCommand(true);
            yield return  _waitForSeconds;
        }
        yield break;
    }

    private void  StopAllAttackCommand()
    {
        StopAllCoroutines();
        _enemyCombatControl.SetAttackCommand(false);
    }
}

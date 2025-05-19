using System;
using System.Collections;
using System.Collections.Generic;
using GGG.Tool;
using GGG.Tool.Singleton;
using UnityEngine;
using UnityEngine.InputSystem;
public class GameInputManager : Singleton<GameInputManager>
{
  
  
  private GameInputAction _gameInputAction;




  public Vector2 Movement => _gameInputAction.GameInputBase.Movement.ReadValue<Vector2>();
  public bool Run => _gameInputAction.GameInputBase.Run.phase==InputActionPhase.Performed;
  public float MouseDeltaX=>_gameInputAction.GameInputBase.MouseDelta.ReadValue<Vector2>().x;
  public bool Dash=>_gameInputAction.GameInputBase.Dash.phase==InputActionPhase.Performed;
  public bool Lock=>_gameInputAction.GameInputBase.Lock.triggered;
  public bool TakeOut => _gameInputAction.GameInputBase.TakeOutSword.triggered;
  public bool LAttack=>_gameInputAction.GameInputBase.LAttack.triggered;
  public bool RAttack=>_gameInputAction.GameInputBase.RAttack.triggered;
  public bool Finish=>_gameInputAction.GameInputBase.Finish.triggered;
  public bool Block=>_gameInputAction.GameInputBase.Block.phase==InputActionPhase.Performed;
  public bool Trail => _gameInputAction.GameInputBase.TrailTest.triggered;
  
  private void Update()
  {
  
  }
  
  private void Awake()
  {
    _gameInputAction ??= new GameInputAction();
  }
  
  private void OnEnable()
  {
    _gameInputAction.Enable();
  }

  private void OnDisable()
  {
    _gameInputAction.Disable();
  }
}

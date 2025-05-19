using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 计时器的工作状态
/// </summary>
public enum TimerState
{
    NOTWORKERE,//不工作
    WORKERING,//工作
    DONE,//工作完成
}

/// <summary>
/// 此类为计时器类，计时器类当外部拿到计时器后就初始化计时器，计时器类提供一个启用和重置计时器的接口
/// 具体的启用或重置计时器由计时管理器来完成，它只封装
/// 计时器的集体应用就是：将一些任务函数加入计时器，计时器管理器启用工作队列中所有需要处于WORKERING
/// 状态的计时器，到达计时器设置时间后就开始执行任务函数
/// </summary>
public class GameTimer
{
    private float _startTime;
    private Action _task;
    private bool _isStopTimer;
    private TimerState _timerState;

    public GameTimer()
    {
        ResetTimer();
    }

        
    /// <summary>
    /// 开启计时器
    /// </summary>
    /// <param name="计时时长"></param>
    /// <param name="计时完成后执行的任务"></param>
    public void StartTimer(float time, Action task)
    {
        _startTime = time;
        _task = task;
        _isStopTimer = false;
        _timerState = TimerState.WORKERING;
    }

    
    /// <summary>
    /// 计时器开始运行
    /// </summary>
    public void UpdateTimer()
    {
        if (_isStopTimer) return;

        _startTime-=Time.deltaTime;

        if (_startTime < 0)
        {
            _task?.Invoke();
            _timerState = TimerState.DONE;
            _isStopTimer = true;
        }
        
    }
    
    /// <summary>
    /// 返回计时器状态
    /// </summary>
    /// <returns></returns>
    public TimerState GetTimerSatate() => _timerState;
    
    /// <summary>
    /// 重置计时器
    /// </summary>
    public void ResetTimer()
    {
        _startTime = 0f;
        _task = null;
        _isStopTimer = true;
        _timerState = TimerState.NOTWORKERE;
    }
}
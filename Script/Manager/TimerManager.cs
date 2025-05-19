using System;
using System.Collections;
using System.Collections.Generic;
using GGG.Tool.Singleton;
using UnityEngine;

/// <summary>
/// 对于此类计时器管理器的浅显理解:
/// 计时器管理器通过一个闲置队列和一个工作集合来组织管理计时器，
/// 当外部需要启用即使器时，就从闲置计时器队列中拿到一个计时器
/// 供外部使用，计时器管理器内置函数用于遍历所有的工作计时器集
/// 合，当计时器状态量被置为工作就开始启用计时器，当计时器工作
/// 完成就从工作集合回到闲置队列
/// </summary>
public class TimerManager : Singleton<TimerManager>
{
   [SerializeField] private int _intMaxTimerCount;
   
   private Queue<GameTimer> _notWorkerTimer=new Queue<GameTimer>();//闲置计时器队列
   private List<GameTimer> _workeringTimer=new List<GameTimer>(); //工作计时器集合

   private void Start()
   {
      InitTimerManager();
   }

   private void Update()
   {
      UpdateWorkeringTimer();
   }

   /// <summary>
   /// 初始化闲置计时器队列
   /// </summary>
   private void InitTimerManager()
   {
      for (int i = 0; i < _intMaxTimerCount; i++)
      {
         CreateTimer();
      }
   }

   /// <summary>
   /// 创建计时器
   /// </summary>
   private void CreateTimer()
   {
      var timer = new GameTimer();
      _notWorkerTimer.Enqueue(timer);
   }


   /// <summary>
   /// 提供一个外部接口使用闲置队列中的计时器
   /// </summary>
   /// <param name="time"></param>
   /// <param name="task"></param>
   public void TryGetOneTimer(float time, Action task)
   {
      if (_notWorkerTimer.Count == 0)
      {
         CreateTimer();
         var timer=_notWorkerTimer.Dequeue();
         timer.StartTimer(time, task);
         _workeringTimer.Add(timer);
      }
      else
      {
         var timer = _notWorkerTimer.Dequeue();
         timer.StartTimer(time, task);
         _workeringTimer.Add(timer);
      }
   }

   
   /// <summary>
   /// 更新工作集合
   /// </summary>
   public void UpdateWorkeringTimer()
   {
      if (_workeringTimer.Count == 0) return;
      for (int i = 0; i < _workeringTimer.Count; i++)
      {
         if (_workeringTimer[i].GetTimerSatate() == TimerState.WORKERING)
         {
            _workeringTimer[i].UpdateTimer();
         }
         else
         {
            _workeringTimer[i].ResetTimer();//计时器用完，将计时器重置
            _notWorkerTimer.Enqueue(_workeringTimer[i]); //将当前使用完的计时器重新加入到闲置队列
            _workeringTimer.Remove(_workeringTimer[i]);//移除工作集合
         }
      }
      
      
   }
}

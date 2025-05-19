using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GGG.Tool;
using GGG.Tool.Singleton;
using UnityEditor.Build.Reporting;

public class GameEventManager : SingletonNonMono<GameEventManager>
{
   private interface IEventHelp{}

   //无参可订阅事件
   private class EventHelp:IEventHelp
   {
      private event Action _action;
      
      //初始化当前事件
      public EventHelp(Action action)
      {
         _action = action;
      }
      
      //添加订阅
      public void AddCall(Action action)
      {
         _action += action;
      }
      
      //移除订阅
      public void RemoveCall(Action action)
      {
         _action -= action;
      }
      
      //调用事件
      public void Call()
      {
         _action?.Invoke();
      }
   }
   //一个参数
   private class EventHelp<T>:IEventHelp
   {
      private event Action<T> _action;
      
      //初始化当前事件
      public EventHelp(Action<T> action)
      {
         _action = action;
      }
      
      //添加订阅
      public void AddCall(Action<T> action)
      {
         _action += action;
      }
      
      //移除订阅
      public void RemoveCall(Action<T> action)
      {
         _action -= action;
      }
      
      //调用事件
      public void Call(T value)
      {
         _action?.Invoke(value);
      }
   }
   //两个参数
   private class EventHelp<T1,T2>:IEventHelp
   {
      private event Action<T1,T2> _action;
      
      //初始化当前事件
      public EventHelp(Action<T1,T2> action)
      {
         _action = action;
      }
      
      //添加订阅
      public void AddCall(Action<T1,T2> action)
      {
         _action += action;
      }
      
      //移除订阅
      public void RemoveCall(Action<T1,T2> action)
      {
         _action -= action;
      }
      
      //调用事件
      public void Call(T1 value1,T2 value2)
      {
         _action?.Invoke(value1,value2);
      }
   }
   
   private class EventHelp<T1,T2,T3>:IEventHelp
   {
      private event Action<T1,T2,T3> _action;
      
      //初始化当前事件
      public EventHelp(Action<T1,T2,T3> action)
      {
         _action = action;
      }
      
      //添加订阅
      public void AddCall(Action<T1,T2,T3> action)
      {
         _action += action;
      }
      
      //移除订阅
      public void RemoveCall(Action<T1,T2,T3> action)
      {
         _action -= action;
      }
      
      //调用事件
      public void Call(T1 value1,T2 value2,T3 value3)
      {
         _action?.Invoke(value1,value2,value3);
      }
   }
   
   private class EventHelp<T1,T2,T3,T4,T5>:IEventHelp
   {
      private event Action<T1,T2,T3,T4,T5> _action;
      
      //初始化当前事件
      public EventHelp(Action<T1,T2,T3,T4,T5> action)
      {
         _action = action;
      }
      
      //添加订阅
      public void AddCall(Action<T1,T2,T3,T4,T5> action)
      {
         _action += action;
      }
      
      //移除订阅
      public void RemoveCall(Action<T1,T2,T3,T4,T5> action)
      {
         _action -= action;
      }
      
      //调用事件
      public void Call(T1 value1,T2 value2,T3 value3,T4 value4,T5 value5)
      {
         _action?.Invoke(value1,value2,value3,value4,value5);
      }
   }
   
   private Dictionary<string,IEventHelp> _eventCenter=new Dictionary<string, IEventHelp>();
   
   /// <summary>
   /// 添加事件监听
   /// </summary>
   /// <param name="事件名称"></param>
   /// <param name="回调函数"></param>
   public void AddEventListening(string eventName, Action action)
   {
      if (_eventCenter.TryGetValue(eventName, out var eventHelp))
      {
         (eventHelp as EventHelp).AddCall(action);
      }
      else
      {
         _eventCenter.Add(eventName,new EventHelp(action));
      }
   }
   
   public void AddEventListening<T>(string eventName, Action<T> action)
   {
      if (_eventCenter.TryGetValue(eventName, out var eventHelp))
      {
         (eventHelp as EventHelp<T>).AddCall(action);
      }
      else
      {
         _eventCenter.Add(eventName,new EventHelp<T>(action));
      }
   }
   
   public void AddEventListening<T1,T2>(string eventName, Action<T1,T2> action)
   {
      if (_eventCenter.TryGetValue(eventName, out var eventHelp))
      {
         (eventHelp as EventHelp<T1,T2>).AddCall(action);
      }
      else
      {
         _eventCenter.Add(eventName,new EventHelp<T1,T2>(action));
      }
   }
   
   public void AddEventListening<T1,T2,T3>(string eventName, Action<T1,T2,T3> action)
   {
      if (_eventCenter.TryGetValue(eventName, out var eventHelp))
      {
         (eventHelp as EventHelp<T1,T2,T3>).AddCall(action);
      }
      else
      {
         _eventCenter.Add(eventName,new EventHelp<T1,T2,T3>(action));
      }
   }
   
   public void AddEventListening<T1,T2,T3,T4,T5>(string eventName, Action<T1,T2,T3,T4,T5> action)
   {
      if (_eventCenter.TryGetValue(eventName, out var eventHelp))
      {
         (eventHelp as EventHelp<T1,T2,T3,T4,T5>).AddCall(action);
      }
      else
      {
         _eventCenter.Add(eventName,new EventHelp<T1,T2,T3,T4,T5>(action));
      }
   }
   /// <summary>
   /// 回调事件函数
   /// </summary>
   /// <param name="事件名称"></param>
   public void CallEvent(string eventName)
   {
      if (_eventCenter.TryGetValue(eventName, out var eventHelp))
      {
         (eventHelp as EventHelp).Call();
      }
      else
      {
         DevelopmentTools.WTF($"当前未找到==>{eventName}<==的事件,无法执行");
      }
   }
   
   public void CallEvent<T>(string eventName, T value)
   {
      if (_eventCenter.TryGetValue(eventName, out var eventHelp))
      {
         (eventHelp as EventHelp<T>).Call(value);
      }
      else
      {
         DevelopmentTools.WTF($"当前未找到==>{eventName}<==的事件,无法执行");
      }
   }
   
   public void CallEvent<T1,T2>(string eventName, T1 value1, T2 value2)
   {
      if (_eventCenter.TryGetValue(eventName, out var eventHelp))
      {
         (eventHelp as EventHelp<T1,T2>).Call(value1,value2);
      }
      else
      {
         DevelopmentTools.WTF($"当前未找到==>{eventName}<==的事件,无法执行");
      }
   }
   
   public void CallEvent<T1,T2,T3>(string eventName, T1 value1, T2 value2, T3 value3)
   {
      if (_eventCenter.TryGetValue(eventName, out var eventHelp))
      {
         (eventHelp as EventHelp<T1,T2,T3>).Call(value1,value2,value3);
      }
      else
      {
         DevelopmentTools.WTF($"当前未找到==>{eventName}<==的事件,无法执行");
      }
   }
   
   public void CallEvent<T1,T2,T3,T4,T5>(string eventName, T1 value1, T2 value2, T3 value3, T4 value4, T5 value5)
   {
      if (_eventCenter.TryGetValue(eventName, out var eventHelp))
      {
         (eventHelp as EventHelp<T1,T2,T3,T4,T5>).Call(value1,value2,value3,value4,value5);
      }
      else
      {
         DevelopmentTools.WTF($"当前未找到==>{eventName}<==的事件,无法执行");
      }
   }
   
   /// <summary>
   /// 事件中的回调函数移除
   /// </summary>
   /// <param name="事件名称"></param>
   /// <param name="回调函数"></param>
   public void RemoveEvent(string eventName, Action action)
   {
      if (_eventCenter.TryGetValue(eventName, out var eventHelp))
      {
         (eventHelp as EventHelp).RemoveCall(action);
      }
      else
      {
         DevelopmentTools.WTF($"当前未找到==>{eventName}<==的事件,无法移除");
      }
   }
   
   public void RemoveEvent<T>(string eventName, Action<T> action)
   {
      if (_eventCenter.TryGetValue(eventName, out var eventHelp))
      {
         (eventHelp as EventHelp<T>).RemoveCall(action);
      }
      else
      {
         DevelopmentTools.WTF($"当前未找到==>{eventName}<==的事件,无法移除");
      }
   }
   
   public void RemoveEvent<T1,T2>(string eventName, Action<T1,T2> action)
   {
      if (_eventCenter.TryGetValue(eventName, out var eventHelp))
      {
         (eventHelp as EventHelp<T1,T2>).RemoveCall(action);
      }
      else
      {
         DevelopmentTools.WTF($"当前未找到==>{eventName}<==的事件,无法移除");
      }
   }
   
   public void RemoveEvent<T1,T2,T3>(string eventName, Action<T1,T2,T3> action)
   {
      if (_eventCenter.TryGetValue(eventName, out var eventHelp))
      {
         (eventHelp as EventHelp<T1,T2,T3>).RemoveCall(action);
      }
      else
      {
         DevelopmentTools.WTF($"当前未找到==>{eventName}<==的事件,无法移除");
      }
   }
   
   public void RemoveEvent<T1,T2,T3,T4,T5>(string eventName, Action<T1,T2,T3,T4,T5> action)
   {
      if (_eventCenter.TryGetValue(eventName, out var eventHelp))
      {
         (eventHelp as EventHelp<T1,T2,T3,T4,T5>).RemoveCall(action);
      }
      else
      {
         DevelopmentTools.WTF($"当前未找到==>{eventName}<==的事件,无法移除");
      }
   }
}

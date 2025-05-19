using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolItem
{
    void Spawn();
    void Recycle();
}
/// <summary>
/// 此类为对象池的基类，对象池本身就是内置接口启用对象池对象，
/// 这个基类就是封装了两个函数当对象池中的对象被启用是调用的
/// 方法，实现对象池对象启用和禁用的实现不同的效果
/// </summary>
public abstract class PoolItemBase : MonoBehaviour,IPoolItem
{
    private void OnEnable()
    {
        Spawn();
    }

    private void OnDisable()
    {
        Recycle();
    }

    public virtual void Spawn()
    {
        
    }

    public virtual void Recycle()
    {
        
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using GGG.Tool;
using GGG.Tool.Singleton;
using UnityEngine;
/// <summary>
/// 此类为对象池单例，用于配置对象，存储对象，供后续使用
/// </summary>
public class GamePoolManager : Singleton<GamePoolManager>
{
    [System.Serializable]
    private class PoolItem
    {
        public string itemName;
        public GameObject item;
        public int initMaxCount;
    }
    
    [SerializeField]
    private List<PoolItem> _configPoolItems = new List<PoolItem>();
    private Dictionary<string,Queue<GameObject>> _poolCenter=new Dictionary<string, Queue<GameObject>>();
    private GameObject _poolItemParent;

    private void Start()
    {
        _poolItemParent = new GameObject("对象池item的父对象");
        _poolItemParent.transform.SetParent(this.transform);
        InitPool();
    }

    /// <summary>
    /// 初始化对象池中心
    /// </summary>
    private void InitPool()
    {
        if (_configPoolItems.Count == 0) return;
        for (int i = 0; i < _configPoolItems.Count; i++)
        {
            for (int j = 0; j < _configPoolItems[i].initMaxCount; j++)
            {
                var item = Instantiate(_configPoolItems[i].item);
                item.SetActive(false);
                item.transform.SetParent(_poolItemParent.transform);
                if (!_poolCenter.ContainsKey(_configPoolItems[i].itemName))
                {
                    _poolCenter.Add(_configPoolItems[i].itemName,new Queue<GameObject>());
                    _poolCenter[_configPoolItems[i].itemName].Enqueue(item);
                }
                else
                {
                    _poolCenter[_configPoolItems[i].itemName].Enqueue(item);
                }
            }
        }
    }
    
    /// <summary>
    /// 拿到对象池中的一个对象，设置其位置及旋转
    /// </summary>
    /// <param name="对象名"></param>
    /// <param name="对象设置位置"></param>
    /// <param name="对象设置旋转"></param>
    public void TryGetPoolItem(string name, Vector3 position, Quaternion quaternion)
    {
        if (_poolCenter.ContainsKey(name))
        {
            var item = _poolCenter[name].Dequeue();
            item.transform.position = position;
            item.transform.rotation = quaternion;
            item.SetActive(true);
            _poolCenter[name].Enqueue(item);
        }
        else
        {
            DevelopmentTools.WTF("当前对象池不存在，无法拿到对象。申请池子名为："+name);
        }
    }
    
    /// <summary>
    /// 拿到对象池中的一个对象
    /// </summary>
    /// <param name="对象名称"></param>
    /// <returns></returns>
    public GameObject TryGetPoolItem(string name)
    {
        if (_poolCenter.ContainsKey(name))
        {
            var item = _poolCenter[name].Dequeue();
            item.SetActive(true);
            _poolCenter[name].Enqueue(item);
            return item;
        }
        
        DevelopmentTools.WTF("当前对象池不存在，无法拿到对象。申请池子名为："+name);
        return null;
    }
}

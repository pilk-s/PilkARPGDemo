using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//学习总结：
/*关于这个类的理解：
 *
 *
 */

//学习总结：
/*关于这个类的理解：
 * 这一个UI框架类用来管理所有的UI界面，这是架构模式可以适配任意UI窗口，比如：背包界面，抽卡界面，主界面，商城界面，凡是UGUI中的要创建在Canvas画布上的UI界面都可以用这套框架
 * 这个框架的设计方式就是 一：单一模式 二：通过三个字典对UI窗口进行统一管理，pathDict：用来存储管理UI窗口的预制体的文件路径（注意这个字典是需要配置的） prefabDict：缓存UI
 * 预制体，以便下次使用，减少电脑能耗 PanelDict：缓存储当前已打开的界面  三：通过一个根节点游戏对象（Canvas）来创建各各UI界面
 * 
 */
public class UIManager 
{
   private static UIManager _instance;
   public Dictionary<string, string> pathDict;
   //缓存预制件
   public Dictionary<string,GameObject> prefabDict;
   //存储当前已经打开的界面
   public Dictionary<string,BasePanel> PanelDict;
   private Transform _uiRoot;

   public Transform UIRoot
   {
      get
      {
         if (_uiRoot == null)
         {
            if (GameObject.Find("Canvas"))
            {
               _uiRoot = GameObject.Find("Canvas").transform;
            }
            else
            {
               _uiRoot = new GameObject("Canvas").transform;
            }
         }
         return _uiRoot;
      }
   }
   public static UIManager Instance
   {
      get
      {
         if (_instance == null)
         {
            _instance = new UIManager();
         }
         return _instance;
      }
   }

   private UIManager()
   {
      InitDicts();
   }

   private void InitDicts()
   {
      prefabDict=new Dictionary<string,GameObject>();
      PanelDict=new Dictionary<string,BasePanel>();
      pathDict = new Dictionary<string, string>()
      {
         {UIconst.MainMenuPanel,"MainMenuPanel"},
         {UIconst.LoadingPanel,"LoadPanel"}
      };
   }

   public BasePanel GetPanel(string name)
   {
      BasePanel panel=null;
      if (PanelDict.TryGetValue(name, out panel))
      {
         return panel;
      }
      return null;
   }

   public BasePanel OpenPanel(string name)
   {
      BasePanel panel = null;
      //检测是否已经打开
      if (PanelDict.TryGetValue(name, out  panel))
      {
        Debug.Log("界面已打开:"+name);
        return null;
      }
      //检测路径是否已经配置了
      string path = "";
      if (!pathDict.TryGetValue(name, out path))
      {
         Debug.Log("界面名称错误，或者未配置路径："+name);
         return null;
      }
      //使用缓存的预制件
      GameObject panelPrefab = null;
      if (!prefabDict.TryGetValue(name, out panelPrefab))
      {
         string realPath = "Prefabs/Panel/" + path;
         panelPrefab = Resources.Load<GameObject>(realPath);
         prefabDict.Add(name, panelPrefab);
      }
      //打开界面
      GameObject panelObject=GameObject.Instantiate(panelPrefab,UIRoot);
      panel = panelObject.GetComponent<BasePanel>();
      PanelDict.Add(name,panel);
      panel.OpenPanel(name);
      return panel;

   }

   public bool ClosePanel(string name)
   {
      BasePanel panel = null;
      if (!PanelDict.TryGetValue(name, out panel))
      {
         Debug.Log("界面未被打开"+name);
         return false;
      }
      panel.ClosePanel();
      return true;
   }
   public class UIconst
   {
      public const string MainMenuPanel="MainMenuPanel";
      public const string LoadingPanel = "LoadPanel";
   }
   
   
}

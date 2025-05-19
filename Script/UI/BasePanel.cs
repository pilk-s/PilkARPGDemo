using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//学习总结：
/*关于这个类的理解：
 *这个类是全体UI界面的父类，封装了每个UI窗口的属性，以及共有的方法
 */
public class BasePanel : MonoBehaviour
{
    protected new string name;
    protected bool isRemove=false;
    
    public virtual void OpenPanel(string name)
    {
        this.name = name;
        gameObject.SetActive(true);
    }

    public virtual void ClosePanel()
    {
        isRemove = true;
        gameObject.SetActive(false);
        
        //移除缓存,表示界面关闭
        if (UIManager.Instance.PanelDict.ContainsKey(name))
        {
            UIManager.Instance.PanelDict.Remove(name);
        } 
        Debug.Log(gameObject+"界面已被关闭");
        Destroy(gameObject);
    }
}
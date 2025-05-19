using System.Collections;
using System.Collections.Generic;
using GGG.Tool.Singleton;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneMgr : Singleton<SceneMgr>
{
    public void LoadSceneAsync(string sceneName,UnityAction function)
    {
       StartCoroutine(IELoadSceneAsync(sceneName, function));
    }

    private IEnumerator IELoadSceneAsync(string sceneName, UnityAction function)
    {
        AsyncOperation ao=SceneManager.LoadSceneAsync(sceneName);
        while (!ao.isDone)
        {
            //TODO:通过事件中心去实现进度条的加载
            var value = ao.progress;
            GameEventManager.MainInstance.CallEvent<float>("SetStarImageSizeDelta",value);
           yield return null;
        }
        yield return ao;
        function?.Invoke();
        yield break;
    }
}

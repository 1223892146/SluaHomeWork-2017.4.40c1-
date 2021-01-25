using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleAutoMono<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T GetInstance()
    {
        if (instance == null) {
            GameObject obj = new GameObject();
            //设置对象的名字为脚本名
            obj.name = typeof(T).ToString();
            GameObject.DontDestroyOnLoad(obj);      //防止切换场景把该物体销毁了
            obj.AddComponent<T>();
        }
        return instance;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T GetInstance (){
        return instance;
    }

    protected virtual void Awake()          //重写是需要加base.Awake()，不然不能执行instance = this as T;
    {
        instance = this as T;
    }
}

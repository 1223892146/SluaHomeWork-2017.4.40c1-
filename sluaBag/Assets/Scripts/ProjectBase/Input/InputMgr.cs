using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 1、input类
/// 2、事件中心模块
/// 3、公共Mono模块
/// </summary>
public class InputMgr : BaseManager<InputMgr>
{
    private bool isStart = false;
    /// <summary>
    /// 构造函数中添加updata监听
    /// </summary>
    public InputMgr() {
        MonoManager.GetInstance().controller.AddUpdateListener(MyUpdate);
    }


    /// <summary>
    /// 是否开启或者关闭输入检测
    /// </summary>
    public void StartOrEndCheck(bool isOpen) {
        isStart = isOpen;
    
    }

    private void CheckKeyCode(KeyCode key) {

        if (Input.GetKeyDown(key)) {
            EventCenter.GetInstance().EventTrigger("某键按下",key);
        }
        if (Input.GetKeyUp(key)) {
            EventCenter.GetInstance().EventTrigger("某键抬起",key);
        }

    }

    private void MyUpdate() {
        if (!isStart) 
            return;

        CheckKeyCode(KeyCode.W);
        CheckKeyCode(KeyCode.A);
        CheckKeyCode(KeyCode.S);
        CheckKeyCode(KeyCode.D);
    }

}

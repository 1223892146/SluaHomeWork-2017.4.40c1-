using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 面板基类
/// 找到所有自己面板下的空间对象
/// 她应该提供显示后者隐藏行为
/// 帮我们通过代码快速找到所有的子控件，方便我们在子类中处理逻辑，节约找空间的工作量
/// </summary>
public class BasePanel : MonoBehaviour
{
    //通过里式转换原则(基类装子类) 来存储所有的控件
    private Dictionary<string, List<UIBehaviour>> controlDic = new Dictionary<string,List<UIBehaviour>>();

    // Start is called before the first frame update
    protected virtual void Awake()      //可以写自己的逻辑，但是base.Awake()，不能少
    {
        FindChildrenControl<Button>();
        FindChildrenControl<Image>();
        FindChildrenControl<Text>();
        FindChildrenControl<Toggle>();
        FindChildrenControl<Slider>();
        FindChildrenControl<ScrollRect>();
        FindChildrenControl<InputField>();

    }

    /// <summary>
    /// 显示自己
    /// </summary>
    public virtual void ShowMe() { 
        
    
    }

    /// <summary>
    /// 隐藏自己
    /// </summary>
    public virtual void HidenMe() { 
    
    }

    protected virtual void OnClick(string btnName)
    {
        //子类通过判断名字直接调用相应函数即可，可以不用监听事件了


    }

    protected virtual void OnValueChanged(string toggleName, bool value) { 
        
        //同上
    }

    protected T GetControl<T>(string controlName)where T : UIBehaviour {

        if (controlDic.ContainsKey(controlName)) {
            for (int i = 0; i < controlDic[controlName].Count; ++i) {
                if (controlDic[controlName][i] is T)
                    return controlDic[controlName][i] as T;
            
            }
        
        }
        return null;
    }

    /// <summary>
    /// 找到子对象的对应控件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    private void FindChildrenControl<T>() where T : UIBehaviour {
        T[] controls = this.GetComponentsInChildren<T>();       
        for (int i = 0; i < controls.Length; ++i) {
            string objName = controls[i].gameObject.name;
            if (controlDic.ContainsKey(objName))
                controlDic[objName].Add(controls[i]);
            else
                controlDic.Add(objName,new List<UIBehaviour>() { controls[i] });

            //如果是按钮控件
            if (controls[i] is Button)
            {
                (controls[i] as Button).onClick.AddListener(() =>
                {
                    OnClick(objName);
                    //有一说一，这里很妙

                });
            }
            //如果是单选框或者多选框
            else if (controls[i] is Toggle) {
                (controls[i] as Toggle).onValueChanged.AddListener((value) =>
                {
                    OnValueChanged(objName,value);
                    //有一说一，这里很妙

                });

            }
        }
    
    }


}

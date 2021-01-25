using SLua;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CustomLuaClass]
public class Test
{
    public void test() {
        Debug.Log("··");
    }
    public ABMgr GetABMgr() {
        return ABMgr.GetInstance();
    }
}

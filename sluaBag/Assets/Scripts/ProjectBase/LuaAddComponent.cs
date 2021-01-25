using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SLua;
using System.IO;

[CustomLuaClass]
public class LuaAddComponent : MonoBehaviour
{

    // Start is called before the first frame update
    public string luaName;

    LuaSvr luaSvr;
    LuaTable self;
    LuaFunction update;
    LuaFunction fixUpdate;

    [CustomLuaClass]
    public delegate void UpdateDelegate(object self);
    public delegate void FixUpdateDelegate(object self);

    UpdateDelegate ud;
    FixUpdateDelegate fud;
    
    //LuaFunction function;
    void Start()
    {
        luaSvr= LuaMgr.GetInstance().Init();
        //luaSvr.start("Test");
        //luaSvr.init(null, LuaCompleteFunc);
        
        LuaCompleteFunc();
        
    }

    void LuaCompleteFunc() {
        //string path = Application.dataPath + "/Lua/" + luaName;
        //Debug.Log(path);
        //self = (LuaTable)luaSvr.start(luaName);
        self = (LuaTable)luaSvr.start(luaName);
        // Debug.Log(self == null);

        try
        {
            update = (LuaFunction)self["update"];
        }
        catch (System.Exception)
        {   
        }

        try
        {
            fixUpdate = (LuaFunction)self["fixUpdate"];
        }
        catch (System.Exception)
        {
        }
        

        if (update != null)
            ud = update.cast<UpdateDelegate>();

        if (fixUpdate != null)
            fud = fixUpdate.cast<FixUpdateDelegate>();

        AddListenerToMono();


    }

    void MyUpdate() {
        if (ud != null) ud(self);
    }

    void MyFixUpdate() {
        if (fud != null) fud(self);
    }

    void AddListenerToMono() {
        if (ud != null) {
            MonoManager.GetInstance().AddUpdateListener(MyUpdate);
        }
        if (fud != null) {
            MonoManager.GetInstance().AddFixUpdateListener(MyFixUpdate);
        }
        
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    public void test() {
        Debug.Log("≤‚ ‘");
    }




}

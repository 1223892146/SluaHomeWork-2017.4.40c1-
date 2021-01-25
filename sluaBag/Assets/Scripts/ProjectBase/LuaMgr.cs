using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SLua;

/// <summary>
/// Lua管理器
/// </summary>
public class LuaMgr : BaseManager<LuaMgr>
{
    //private static LuaState luaState;
    private static LuaSvr luaSvr;
    private LuaFunction luaFunction;

    public LuaSvr Init()
    {
        if (luaSvr != null) {
            return luaSvr;
        }
        //唯一的解析器
        luaSvr = new LuaSvr();
        LuaSvr.mainState.loaderDelegate = MyCustomLoader;
        
        luaSvr.init(null,()=> {
            Debug.Log("初始化");
        });
        //luaState = new LuaState();
        // luaSvr.init(null, complete, LuaSvrFlag.LSF_BASIC | LuaSvrFlag.LSF_EXTLIB);
        //添加重定向委托函数
        
        //luaState.loaderDelegate = MyCustomLoaderFormAB;
        return luaSvr;

    }

    //void InitLuaLibary() {
      //  luaSvr.start("");
    
   // }

    //Lua总表
    //用于之后 lua访问C#时 使用 通过总表获取lua中各种内容
   /* public LuaTable Global
    {
        get
        {
            return luaSvr.getTable("_G");
        }
    }  */

    private byte[] MyCustomLoader(string filepath,ref string ab)
    {
        //测试传入的参数是什么
        Debug.Log(filepath);
        //决定Lua文件所在路径
        string path = Application.dataPath + "/Lua/" + filepath + ".lua";
        //C#自带的文件读取类
        if (File.Exists(path))
        {
            return File.ReadAllBytes(path);
        }
        else
            Debug.Log("MyCustomLoader重定向失败");

        return null;
    }

    //再写一个Load 用于从AB包加载Lua文件
    private byte[] MyCustomLoaderFormAB(ref string filepath)
    {
        //改为我们的AB包管理器加载
        TextAsset file2 = ABMgr.GetInstance().LoadRes<TextAsset>("lua", filepath + ".lua");
        if (file2 != null)
            return file2.bytes;
        else
            Debug.Log("MyCustomLoaderFormAB重定向失败");
        return null;
    }



    /// <summary>
    /// 执行lua文件
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="formWhere"></param>
    /*public void DoLuaFile(string fileName, string formWhere = null)
    {
        string str = string.Format("require('{0}')", fileName);
        luaSvr.doString(str);
    }*/

    //执行Lua脚本
   /* public void DoString(string luaScript, string fromWhere = null)
    {
        luaSvr.doString(luaScript, fromWhere);
    }

    public void CallFunction(string funcName) {
        luaFunction = luaSvr.getFunction(funcName);
        luaFunction.call();
    }*/

    //释放垃圾
    /*public void Tick()
    {
        state.Tick();
    }

    //销毁
    public void Dispose()
    {
        state.Tick();
        state.Dispose();
    }*/
}

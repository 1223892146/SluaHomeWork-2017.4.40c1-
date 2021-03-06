﻿using UnityEditor;
using System.Collections;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class AssetBundle
{
    [MenuItem("Custom Editor/Build Asset Bundles")]
    static void BuildABs()
    {
        string path = "Assets/StreamingAssets";
        if (Directory.Exists(path) == false)
        {
            //判断文件路径是否存在，若不存在，创建文件
            Directory.CreateDirectory(path);
        }
        BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
    }
}

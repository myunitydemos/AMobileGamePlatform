using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AssetsLoader : Singleton<AssetsLoader>
{
    public GameObject LoadPrefab(string path)
    {
        return AssetDatabase.LoadAssetAtPath<GameObject>(path + ".prefab");
    }
}

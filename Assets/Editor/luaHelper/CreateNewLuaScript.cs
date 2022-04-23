using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;

public class CreateNewLuaScript : MonoBehaviour
{
    [MenuItem("Assets/Create/Lua Script", false)]
    public static void CreateNewLua()
    {
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,
            ScriptableObject.CreateInstance<CreateScriptAssetAction>(),
            GetSelectedPathOrFallback() + "/New Lua.lua",
            null,
            "Assets/Editor/Template/LuaTemplate.lua");
    }

    // 获取创建文件的路径
    public static string GetSelectedPathOrFallback()
    {
        string path = "Assets";
        foreach(Object obj in Selection.GetFiltered<Object>(SelectionMode.Assets))
        {
            path = AssetDatabase.GetAssetPath(obj);
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                path = Path.GetDirectoryName(path);
                break;
            }
        }
        return path;
    }
}

class CreateScriptAssetAction: EndNameEditAction
{
    public override void Action(int instanceId, string pathName, string resourceFile)
    {
        // 创建资源
        Object obj = CreateAssetFromTemplate(pathName, resourceFile);
        // 高亮显示资源
        ProjectWindowUtil.ShowCreatedAsset(obj);
    }

    // 通过模板创建文件
    internal static Object CreateAssetFromTemplate(string pathName, string resourceFile)
    {
        // 获取资源绝对路径
        string fullName = Path.GetFullPath(pathName); 

        // 读取本地模板文件
        StreamReader reader = new StreamReader(resourceFile);
        string content = reader.ReadToEnd();
        reader.Close();

        // 获取新文件的文件名
        string fileName = Path.GetFileNameWithoutExtension(pathName);

        content = content.Replace("#SCRIPTNAME#", fileName);

        // 写入新文件
        StreamWriter writer = new StreamWriter(fullName, false, new System.Text.UTF8Encoding(false));
        writer.Write(content);
        writer.Close();

        // 刷新本地资源
        AssetDatabase.ImportAsset(pathName);
        AssetDatabase.Refresh();
        return AssetDatabase.LoadAssetAtPath<Object>(pathName);
    }
}
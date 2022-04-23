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

    // ��ȡ�����ļ���·��
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
        // ������Դ
        Object obj = CreateAssetFromTemplate(pathName, resourceFile);
        // ������ʾ��Դ
        ProjectWindowUtil.ShowCreatedAsset(obj);
    }

    // ͨ��ģ�崴���ļ�
    internal static Object CreateAssetFromTemplate(string pathName, string resourceFile)
    {
        // ��ȡ��Դ����·��
        string fullName = Path.GetFullPath(pathName); 

        // ��ȡ����ģ���ļ�
        StreamReader reader = new StreamReader(resourceFile);
        string content = reader.ReadToEnd();
        reader.Close();

        // ��ȡ���ļ����ļ���
        string fileName = Path.GetFileNameWithoutExtension(pathName);

        content = content.Replace("#SCRIPTNAME#", fileName);

        // д�����ļ�
        StreamWriter writer = new StreamWriter(fullName, false, new System.Text.UTF8Encoding(false));
        writer.Write(content);
        writer.Close();

        // ˢ�±�����Դ
        AssetDatabase.ImportAsset(pathName);
        AssetDatabase.Refresh();
        return AssetDatabase.LoadAssetAtPath<Object>(pathName);
    }
}
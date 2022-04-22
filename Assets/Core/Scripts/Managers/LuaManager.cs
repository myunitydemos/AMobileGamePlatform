using UnityEngine;
using XLua;
using System.IO;

public class LuaManager : UnitySingleton<LuaManager>
{
    private LuaEnv _env = null;
    private bool _isGameStarted = false;
    public override void Awake()
    {
        base.Awake();
        this._initLuaEnv();
    }

    private void _initLuaEnv()
    {
        _env = new LuaEnv();
        // 添加自定义Lua装载器
        _env.AddLoader(_luaScriptLoader);
    }

    private byte[] _luaScriptLoader(ref string filepath)
    {
        string path = string.Empty;
        filepath = filepath.Replace(".", "/") + ".lua";
#if UNITY_EDITOR
        path = Path.Combine(Application.dataPath, filepath);
        byte[] data = IOHelper.SafeReadAllBytes(ref path);
        return data;
#else
        return null;
#endif
    }

    public void InitLua()
    {
        _isGameStarted = true;
        _env.DoString("require('Core.LuaScripts.main')");
        _env.DoString("main.init()");
    }
    
    // Update is called once per frame
    void Update()
    {
        if (_isGameStarted) _env.DoString("main.update()");
    }

    private void FixedUpdate()
    {
        if (_isGameStarted) _env.DoString("main.fixedUpdate()");
    }

    private void LateUpdate()
    {
        if (_isGameStarted) _env.DoString("main.lateUpdate()");
    }
}

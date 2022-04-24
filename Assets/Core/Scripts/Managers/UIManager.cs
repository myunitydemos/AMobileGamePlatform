using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : UnitySingleton<UIManager>
{
    GameObject _uiRoot;
    GameObject _canvas;
    public override void Awake()
    {
        base.Awake();
        _initUI();
    }
    void _initUI()
    {
        GameObject rootPrefab = AssetsLoader.Instance.LoadPrefab("Assets/Core/Prefabs/RootUI");
        _uiRoot = Instantiate(rootPrefab, transform.parent);
        _canvas = _uiRoot.transform.Find("Canvas").gameObject;
        GameObject pagePrefab = AssetsLoader.Instance.LoadPrefab("Assets/Core/Prefabs/Launcher");
        _crtPage = Instantiate(pagePrefab, _canvas.transform);
    }

    GameObject _crtPage = null;
    public void EnterGame(string game)
    {
        if (_crtPage != null) Destroy(_crtPage);
        GameObject pagePrefab = AssetsLoader.Instance.LoadPrefab("Assets/Games/" + game + "/Prefabs/IndexUI");
        _crtPage = Instantiate(pagePrefab, _canvas.transform);
    }
}

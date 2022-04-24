using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamesManager : UnitySingleton<GamesManager>
{
    GameObject _gameRoot;
    public override void Awake()
    {
        base.Awake();
        _initGameRoot();
    }

    private void _initGameRoot()
    {
        GameObject rootPrefab = AssetsLoader.Instance.LoadPrefab("Assets/Core/Prefabs/RootGame");
        _gameRoot = Instantiate(rootPrefab, transform.parent);
    }

    GameObject _crtGame = null;
    public void EnterGame(string game)
    {
        if (_crtGame != null) Destroy(_crtGame);
        GameObject gamePrefab = AssetsLoader.Instance.LoadPrefab("Assets/Games/" + game + "/Prefabs/IndexGame");
        _crtGame = Instantiate(gamePrefab, _gameRoot.transform);
    }
}

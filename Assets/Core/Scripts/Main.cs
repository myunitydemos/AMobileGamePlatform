using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : UnitySingleton<Main>
{
    public override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        gameObject.AddComponent<LuaManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        this.StartCoroutine(this.LaunchGame());
    }

    IEnumerator CheckUpdate()
    {
        yield return null;
    }

    IEnumerator LaunchGame()
    {
        yield return this.StartCoroutine(this.CheckUpdate());
        LuaManager.Instance.InitLua();
    }
}

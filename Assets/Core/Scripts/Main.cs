using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : UnitySingleton<Main>
{
    public override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        gameObject.AddComponent<UIManager>();
        gameObject.AddComponent<GamesManager>();
        gameObject.AddComponent<LuaManager>();
        gameObject.AddComponent<MessageManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        this.StartCoroutine(this.Launch());
    }

    IEnumerator CheckUpdate()
    {
        yield return new WaitForSeconds(2);
    }

    IEnumerator Launch()
    {
        yield return this.StartCoroutine(this.CheckUpdate());
        LuaManager.Instance.InitLua();
        UIManager.Instance.EnterGame("Lobby");
        MessageManager.AddHandler<string>("EnterGame", EnterGame);
    }

    public void EnterGame(string game)
    {
        UIManager.Instance.EnterGame(game);
        GamesManager.Instance.EnterGame(game);
    }
}

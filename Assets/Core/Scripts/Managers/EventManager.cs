using System;
using System.Collections.Generic;
using UnityEngine;

public enum GlobalEvent
{
    EnterGame,
}

public class EventManager : UnitySingleton<EventManager>
{

    private EventEmitter<GlobalEvent, string> globalEmitter = new EventEmitter<GlobalEvent, string>();
    public override void Awake()
    {
        base.Awake();
    }

    public void InitEvent()
    {
    }

    public void AddListener(GlobalEvent eventType, Action<string> arg)
    {
        globalEmitter.AddListener(eventType, arg);
    }

    public void Emit(GlobalEvent eventType, string arg)
    {
        globalEmitter.Emit(eventType, arg);
    }
}

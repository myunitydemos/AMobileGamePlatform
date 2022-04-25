using System.Collections.Generic;
using System;
using UnityEngine.Events;

public interface IMessage { }
public class Message<T>: IMessage
{
    public UnityAction<T> actions;
}

public class Message: IMessage
{
    public UnityAction actions;
}

public class MessageManager : UnitySingleton<MessageManager>
{
    Dictionary<string, IMessage> dict = new Dictionary<string, IMessage>();

    void addHandler(string eventType, UnityAction handler)
    {
        if (dict.ContainsKey(eventType))
        {
            (dict[eventType] as Message).actions += handler;
        }
        else
        {
            dict.Add(eventType, new Message() { actions = handler });
        }
    }
    void addHandler<T>(string eventType, UnityAction<T> handler)
    {
        if (dict.ContainsKey(eventType))
        {
            (dict[eventType] as Message<T>).actions += handler;
        }
        else
        {
            dict.Add(eventType, new Message<T>() { actions = handler });
        }
    }

    void removeHandler(string eventType, UnityAction handler)
    {
        if (dict.ContainsKey(eventType))
        {
            UnityAction actions = (UnityAction)Delegate.RemoveAll((dict[eventType] as Message).actions, handler);
            if (actions == null)
            {
                dict.Remove(eventType);
            }
        }
    }
    void removeHandler<T>(string eventType, UnityAction<T> handler)
    {
        if (dict.ContainsKey(eventType))
        {
            UnityAction<T> actions = (UnityAction<T>)Delegate.RemoveAll((dict[eventType] as Message<T>).actions, handler);
            if (actions == null)
            {
                dict.Remove(eventType);
            }
        }
    }

    void dispatch(string eventType)
    {
        if (dict.ContainsKey(eventType))
        {
            (dict[eventType] as Message).actions?.Invoke();
        }
    }
    void dispatch<T>(string eventType, T arg)
    {
        if (dict.ContainsKey(eventType))
        {
            (dict[eventType] as Message<T>).actions?.Invoke(arg);
        }
    }

    void clear()
    {
        dict.Clear();
    }

    bool hasListener(string eventType)
    {
        return dict.ContainsKey(eventType);
    }
    
    public static void AddHandler(string eventType, UnityAction handler)
    {
        Instance.addHandler(eventType, handler);
    }
    public static void AddHandler<T>(string eventType, UnityAction<T> handler)
    {
        Instance.addHandler<T>(eventType, handler);
    }
    public static void RemoveHandler(string eventType, UnityAction handler)
    {
        Instance.removeHandler(eventType, handler);
    }
    public static void RemoveHandler<T>(string eventType, UnityAction<T> handler)
    {
        Instance.removeHandler<T>(eventType, handler);
    }

    public static void Dispatch(string eventType)
    {
        Instance.dispatch(eventType);
    }
    public static void Dispatch<T>(string eventType, T arg)
    {
        Instance.dispatch<T>(eventType, arg);
    }
    public static void Clear()
    {
        Instance.clear();
    }

    public static bool HasListener(string eventType)
    {
        return Instance.hasListener(eventType);
    }
}

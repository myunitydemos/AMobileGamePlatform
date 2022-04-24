using System;
using System.Collections.Generic;

public class EventEmitter<TKey, TValue>
{
    Dictionary<TKey, Action<TValue>> dict = new Dictionary<TKey, Action<TValue>>();
    
    public void AddListener(TKey eventType, Action<TValue> eventHandler)
    {
        Action<TValue> callbacks;
        if (dict.TryGetValue(eventType, out callbacks))
        {
            dict[eventType] = callbacks + eventHandler;
        }
        else
        {
            dict.Add(eventType, eventHandler);
        }
    }

    public void RemoveListener(TKey eventType, Action<TValue> eventHandler)
    {
        Action<TValue> callbacks;
        if (dict.TryGetValue(eventType, out callbacks))
        {
            callbacks = (Action<TValue>)Delegate.RemoveAll(callbacks, eventHandler);
            if (callbacks == null)
            {
                dict.Remove(eventType);
            }
            else
            {
                dict[eventType] = callbacks;
            }
        }
    }

    public bool HasListener(TKey eventType)
    {
        return dict.ContainsKey(eventType);
    }

    public void Emit(TKey eventType, TValue eventArg)
    {
        Action<TValue> callbacks;
        if (dict.TryGetValue(eventType, out callbacks))
        {
            callbacks.Invoke(eventArg);
        }
    }

    public void Clear()
    {
        dict.Clear();
    }
}

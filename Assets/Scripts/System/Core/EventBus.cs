using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEventBus
{
    IDisposable Subscribe<T>(Action<T> handler);
    void Publish<T>(T evt);
    void Clear();
}

public class EventBus : IEventBus
{
    private readonly Dictionary<Type, List<Delegate>> _eventMap = new();
    
    //  Inner class
    private class Subscription : IDisposable
    {
        private Action _action;

        public Subscription(Action action)
        {
            _action = action;
        }

        //  Unsubscription 실행
        public void Dispose()
        {
            _action?.Invoke();
            _action = null;
        }
    }
    
    public IDisposable Subscribe<T>(Action<T> handler)
    {
        var type = typeof(T);
        if (!_eventMap.TryGetValue(type, out var list))
        {
            list = new List<Delegate>();
            _eventMap[type] = list;
        }
        
        list.Add(handler);

        //  For Unsubscription - Lambda로 Capture됨
        return new Subscription(() => Unsubscribe(handler));
    }

    //  외부에서 Call X
    private void Unsubscribe<T>(Action<T> handler)
    {
        var type = typeof(T);
        if (_eventMap.TryGetValue(type, out var list))
        {
            list.Remove(handler);
            if (list.Count == 0) _eventMap.Remove(type);
        }
    }

    public void Publish<T>(T evt)
    {
        var type = typeof(T);

        if (!_eventMap.TryGetValue(type, out var list)) return;

        //  Copy
        var temp = list.ToArray();
        for (int i = 0; i < temp.Length; i++)
        {
            ((Action<T>)temp[i]).Invoke(evt);       
        }
    }

    /* 사용 시 주의 */
    public void Clear()
    {
        _eventMap.Clear();
    }
}

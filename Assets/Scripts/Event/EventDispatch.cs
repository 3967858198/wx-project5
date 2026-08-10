using System;
using System.Collections.Generic;
using UnityEngine;

public class EventDispatch : MonoBehaviour
{
    private static EventDispatch _instance;
    public static EventDispatch GetInstance()
    {
        return _instance; 
    }

    void Awake()
    {
        _instance = this;
    }

    private Dictionary<DefineProject_Editor.Event_ID, List<Action<object>>> _subscribers = new Dictionary<DefineProject_Editor.Event_ID, List<Action<object>>>();

    public void Subscribe(DefineProject_Editor.Event_ID messageType, Action<object> callback)
    {
        if (!_subscribers.ContainsKey(messageType))
        {
            _subscribers[messageType] = new List<Action<object>>();
        }
        _subscribers[messageType].Add(callback);
    }

    public void UnSubscribe(DefineProject_Editor.Event_ID messageType, Action<object> callback)
    {
        if (_subscribers.ContainsKey(messageType))
        {
            _subscribers[messageType].Remove(callback);
        }
    }

    public void Publish(DefineProject_Editor.Event_ID messageType, object data = null)
    {
        if (_subscribers.ContainsKey(messageType))
        {
            List<Action<object>> callbacks = _subscribers[messageType];
            for (int i = 0; i < callbacks.Count; i++)
            {
                callbacks[i].Invoke(data);
            }
        }
    }
}
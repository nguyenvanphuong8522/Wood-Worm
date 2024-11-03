using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventKey
{
    RESTART
}
public static class EventManager<T>
{
    private static Dictionary<EventKey, Action<T>> eventDictionary = new Dictionary<EventKey, Action<T>>();

    public static void Register(EventKey key, Action<T> action)
    {
        if(!eventDictionary.ContainsKey(key))
        {
            eventDictionary.Add(key, action);
        }
        else
        {
            eventDictionary[key] += action;
        }
    }

    public static void Unregister(EventKey key, Action<T> action)
    {
        if(eventDictionary.ContainsKey(key))
        {
            eventDictionary[key] -= action;
        }
    }

    public static void Trigger(EventKey key, T args)
    {
        if(eventDictionary.ContainsKey(key))
        {
            eventDictionary[key]?.Invoke(args);
        }
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventBroadcaster", menuName = "ScriptableObjects/Managers/EventBroadcaster")]
public class EventBroadcaster : SingletonSO<EventBroadcaster>, ISingleton
{
    #region ISingleton Variables
    private bool isDone = false;
    public bool IsDoneInitializing
    {
        get { return isDone; }
    }
    #endregion

    public delegate void ObserverAction(EventParameters param);

    private Dictionary<string, ObserverAction> _observers;

    public void Initialize()
    {
        if (_observers is null)
            _observers = new Dictionary<string, ObserverAction>();
        else
            _observers.Clear();

        isDone = true;
    }
    public void AddObserver(string eventName, ObserverAction observerAction)
    {
        if (this._observers.ContainsKey(eventName))
            this._observers[eventName] += observerAction;
        else
            this._observers.Add(eventName, observerAction);
    }
    public void RemoveObserver(string eventName)
    {
        if (this._observers.ContainsKey(eventName))
        {
            this._observers[eventName] = null;
            this._observers.Remove(eventName);
        }
    }

    public void RemoveObserverAtAction(string eventName, ObserverAction action)
    {
        if (this._observers.ContainsKey(eventName))
            this._observers[eventName] -= action;
    }

    public void PostEvent(string eventName, EventParameters eventParam)
    {
        //Debug.Log("Posted Event: " + eventName);
        if (this._observers.ContainsKey(eventName))
        {
            ObserverAction action = this._observers[eventName];
            action?.Invoke(eventParam);
        }
    }
}

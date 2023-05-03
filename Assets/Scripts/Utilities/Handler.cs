using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Handler: MonoBehaviour, ISingleton, IEventObserver
{
    protected virtual void OnAwake() { } // optional to use to use an an Init method;

    public abstract void Initialize();
    public abstract void AddEventObservers();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UIManager", menuName = "ScriptableObjects/Managers/UIManager")]
public class UIManager : SingletonSO<UIManager>, IInitializable, IEventObserver
{
    public override void Initialize()
    {
        AddEventObservers();
        
    }


    public override void AddEventObservers()
    {
        ;
    }
}

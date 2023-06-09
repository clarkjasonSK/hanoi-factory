using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelHandler : Handler
{
    [SerializeField] private PanelRefs _panel_refs;

    public override void Initialize()
    {
        _panel_refs = GetComponent<PanelRefs>();
        AddEventObservers();
    }
    public override void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.PANEL_DROP, OnPanelDrop);
        EventBroadcaster.Instance.AddObserver(EventKeys.PANEL_RISE, OnPanelRise);
    }


    #region Event Broadcaster Notifications

    public void OnPanelDrop(EventParameters param = null)
    {
        _panel_refs.Panel.MovePanel(_panel_refs._panel_low_pos.transform);
    }
    public void OnPanelRise(EventParameters param = null)
    {
        _panel_refs.Panel.MovePanel(_panel_refs._panel_top_pos.transform);
    }

    #endregion
}


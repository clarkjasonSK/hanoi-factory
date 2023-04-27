using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverHandler : Singleton<LeverHandler>, ISingleton, IEventObserver
{
    #region ISingleton Variables
    private bool isDone = false;
    public bool IsDoneInitializing
    {
        get { return isDone; }
    }
    #endregion

    [SerializeField] private LeverRefs _lever_refs;

    #region Event Parameters
    private EventParameters leverParam;
    #endregion

    #region Cache Params
    private Lever leverKey;
    private LeverPosition leverPosKey;
    #endregion

    public void Initialize()
    {
        if (_lever_refs is null)
            _lever_refs = GetComponent<LeverRefs>();

        leverParam = new EventParameters();
        AddEventObservers();

        isDone = true;
    }

    public void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.GAME_START, OnGameStart);
        EventBroadcaster.Instance.AddObserver(EventKeys.LEVER_POS_HOVER, OnLeverPosHover);
        EventBroadcaster.Instance.AddObserver(EventKeys.LEVER_POS_CHOSEN, OnLeverPosChosen);

        // band aid fix, change later PLEASE
        EventBroadcaster.Instance.AddObserver(EventKeys.PANEL_DROP, OnPanelDrop);
        EventBroadcaster.Instance.AddObserver(EventKeys.POLE_MOVE_FINISH, OnPoleMoveFinish);
    }
    private void setLeverKey(EventParameters param)
    {
        leverKey = param.GetParameter<Lever>(EventParamKeys.LEVER, null);
    }
    private void setPosKey(EventParameters param)
    {
        leverPosKey = param.GetParameter<LeverPosition>(EventParamKeys.LEVER_POS, null);
    }

    #region Event Broadcaster Notifications
    public void OnGameStart(EventParameters param = null)
    {

    }
    public void OnLeverPosHover(EventParameters param)
    {
        if (!_lever_refs.Lever.LeverSelected)
            return;

        setPosKey(param);
        if (_lever_refs.Lever.LeverPos == leverPosKey.LeverPosNum)
            return;

        _lever_refs.Lever.RotateLever(leverPosKey.LeverPosNum, leverPosKey.LeverPosRotationX);
    }
    public void OnLeverPosChosen(EventParameters param)
    {
        setLeverKey(param);

        if (leverKey.LeverPos == GameManager.Instance.RingAmount)
            return;

        GameManager.Instance.SetRingsAmount(leverKey.LeverPos);
        EventBroadcaster.Instance.PostEvent(EventKeys.GAME_RESET, null);
    }

    public void OnPanelDrop(EventParameters param = null)
    {
        _lever_refs.Lever.ToggleHandle(false);
    }
    public void OnPoleMoveFinish(EventParameters param = null)
    {
        _lever_refs.Lever.ToggleHandle(true);
    }

    #endregion


    #region Helper Functions
    public float ConvertEulerAngle(float angle)
    {
        // if angle is greater than left side of lever, subtact full circle angle to get right side
        return angle > 90 ? angle - 360 : angle;
    }
    #endregion
}
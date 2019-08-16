using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLoader : EventBase, ICheckable
{
    [SerializeField, EnumFlags, Space(3)]
    protected GimmickFlag standgimmickFlag;
    [SerializeField]
    private CallType callType;
    public EventName eventName;

    //System.Action 

    private void Start()
    {
        switch (callType)
        {
            case CallType.MiniGame1Clear_Map1:
                EventManager.Instance.TypeinGameMap1ClearedFunc += () => EventManager.Instance.PlayEvent(eventName, gameObject);
                break;
            case CallType.MiniGame1Clear_Map2_FirstHalf:
                EventManager.Instance.TypeinGameMap2FirstClearedFunc += () => EventManager.Instance.PlayEvent(eventName, gameObject);
                break;
            case CallType.MiniGame1Clear_Map2_LatterHalf:
                EventManager.Instance.TypeinGameMap2LetterClearedFunc += () => EventManager.Instance.PlayEvent(eventName, gameObject);
                break;
            case CallType.MiniGame2Clear:
                EventManager.Instance.PieceGameClearedFunc += () => EventManager.Instance.PlayEvent(eventName, gameObject);
                break;
        }
    }


    public GimmickFlag StandgimmickFlag
    {
        get { return standgimmickFlag; }
    }

    public void Check()
    {
        if (FlagManager.Instance.CheckGimmickFlag(needGimmickFlag))
        {
            EventManager.Instance.PlayEvent(eventName, gameObject);
            switch (callType)
            {
                case CallType.OnlyOnce:
                    return;
                case CallType.Always:
                    gameObject.SetActive(false);
                    Finished();
                    break;
            }
        }
    }

    private enum CallType
    {
        OnlyOnce,
        Always,
        MiniGame1Clear_Map1,
        MiniGame1Clear_Map2_FirstHalf,
        MiniGame1Clear_Map2_LatterHalf,
        MiniGame2Clear,
    }
}

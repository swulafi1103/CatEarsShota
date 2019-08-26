using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLoader : EventBase, ICheckable
{
    [SerializeField, EnumFlags, Space(3)]
    protected GimmickFlag standgimmickFlag;
    [SerializeField, EnumFlags]
    protected GimmickFlag_Map2 standgimmickFlag_Map2;

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
    public GimmickFlag_Map2 StandgimmickFlag_Map2
    {
        get { return standgimmickFlag_Map2; }
    }

    public void Check()
    {
        if (FlagManager.Instance.CheckGimmickFlag(needGimmickFlag) && FlagManager.Instance.CheckGimmickFlag(needGimmickFlag_Map2))
        {
            EventManager.Instance.PlayEvent(eventName, gameObject);
            switch (callType)
            {
                case CallType.OnlyOnce:
                    gameObject.SetActive(false);
                    Finished();
                    return;
                case CallType.Always:
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

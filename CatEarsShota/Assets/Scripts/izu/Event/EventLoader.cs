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

    public GimmickFlag StandgimmickFlag
    {
        get { return standgimmickFlag; }
    }

    public void Check()
    {
        if (FlagManager.Instance.CheckGimmickFlag(needGimmickFlag))
        {
            EventManager.Instance.PlayEvent(eventName, gameObject);
            if (callType == CallType.Always)
            {
                return;
            }
            else if (callType == CallType.OnlyOnce)
            {
                gameObject.SetActive(false);
                Finished();
            }            
        }
    }

    private enum CallType
    {
        OnlyOnce,
        Always,
    }
}

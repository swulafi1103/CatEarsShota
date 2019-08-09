using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLoader : EventBase, ICheckable
{

    [SerializeField, EnumFlags, Space(3)]
    protected GimmickFlag standgimmickFlag;

    public EventName eventName;

    public void Check()
    {
        if (FlagManager.Instance.CheckGimmickFlag(needGimmickFlag))
        {
            EventManager.Instance.PlayEvent(eventName);
            gameObject.SetActive(false);
        }
    }
}

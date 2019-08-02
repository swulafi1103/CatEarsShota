using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLoader : EventBase, ICheckable
{
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

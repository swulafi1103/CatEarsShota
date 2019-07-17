using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPastDoor : GimmickEvent, ICheckable
{

    void Start()
    {
        CheckFlag();   
    }


    public override void Check()
    {
        if (FlagManager.Instance.CheckGimmickFlag(needGimmickFlag))
        {
            Debug.Log("TouchDoor");
            FlagManager.Instance.SetGimmickFlag(standgimmickFlag);
            BubbleEvent.Instance.DisplayBubbles(BubbleEvent.BubbleType.Power);
            gameObject.GetComponent<TouchPastDoor>().enabled = false;
        }
    }
}

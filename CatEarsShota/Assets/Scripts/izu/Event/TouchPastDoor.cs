using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPastDoor : GimmickEvent, ICheckable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Check()
    {
        if (FlagManager.Instance.CheckGimmickFlag(needGimmickFlag))
        {
            Debug.Log("TouchDoor");
            FlagManager.Instance.SetGimmickFlag(GimmickFlag.G_09_Minigame1_0);
            BubbleEvent.Instance.DisplayBubbles(BubbleEvent.BubbleType.Power);
            gameObject.GetComponent<TouchPastDoor>().enabled = false;
        }
    }
}

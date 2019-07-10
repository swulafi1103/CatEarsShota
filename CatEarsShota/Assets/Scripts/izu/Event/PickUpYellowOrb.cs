using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpYellowOrb : ItemEvent, ICheckable
{
    
    void Start()
    {
        CheckFlag();
    }

    
    public override bool CheckFlag()
    {
        bool flag = FlagManager.Instance.CheckGimmickFlag(needGimmickFlag);
        if (flag)
        {
            isDisplayBubble = flag;
        }
        DisplayBubble();
        gameObject.SetActive(flag);
        return flag;
    }

    public override void Check()
    {
        FlagManager.Instance.SetItemFlag(standItemFlag);
        ItemManager.Instance.SetItemData(ItemManager.ItemNum.Yerrow_Orb);
        gameObject.SetActive(false);
    }

}

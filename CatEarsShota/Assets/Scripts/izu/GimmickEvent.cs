using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class GimmickEvent : EventBase, ICheckable
{
    [SerializeField, EnumFlags]
    private GimmickFlag standgimmickFlag;

    void Start()
    {
        CheckFlag();
    }
    
    //  プレイヤーから呼ばれる
    public void Check()
    {
        if (FlagManager.Instance.CheckGimmickFlag(needGimmickFlag))
        {
            FlagManager.Instance.SetGimmickFlag(standgimmickFlag);
            gameObject.SetActive(false);
        }        
    }
}

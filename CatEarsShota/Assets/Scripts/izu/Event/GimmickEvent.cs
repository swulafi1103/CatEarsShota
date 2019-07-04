using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(BoxCollider2D))]
public class GimmickEvent : EventBase, ICheckable
{
    [SerializeField, EnumFlags]
    protected GimmickFlag standgimmickFlag;

    void Start()
    {
        CheckFlag();
    }
    
    //  プレイヤーから呼ばれる
    public virtual void Check()
    {
        if (FlagManager.Instance.CheckGimmickFlag(needGimmickFlag))
        {
            Debug.Log(needGimmickFlag + " : TRUE");
            gameObject.SetActive(false);
        }        
    }    
}

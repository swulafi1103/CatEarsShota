using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(BoxCollider2D))]
public class GimmickEvent : EventBase, ICheckable
{
    [SerializeField, EnumFlags]
    protected GimmickFlag standgimmickFlag;
    [SerializeField]
    protected bool isComplete = false;

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
    
    public virtual void CompleteGimmick()
    {
        isComplete = true;
    }

    public override bool CheckFlag()
    {
        bool flag = FlagManager.Instance.CheckGimmickFlag(needGimmickFlag);
        if (flag)
        {
            isDisplayBubble = flag;
        }
        if (isComplete)
        {
            isDisplayBubble = false;
        }
        DisplayBubble();
        return flag;
    }

}

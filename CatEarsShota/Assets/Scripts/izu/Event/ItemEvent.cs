using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ItemEvent : EventBase, ICheckable
{
    [SerializeField, EnumFlags]
    protected ItemFlag standItemFlag = default;

    void Start()
    {
        CheckFlag();
    }


    public virtual void Check()
    {
        FlagManager.Instance.SetItemFlag(standItemFlag);
        gameObject.SetActive(false);
    }
}

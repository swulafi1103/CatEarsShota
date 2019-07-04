using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ItemEvent : EventBase, ICheckable
{
    [SerializeField, EnumFlags]
    private ItemFlag standItemFlag;

    void Start()
    {
        CheckFlag();
    }


    public void Check()
    {
        FlagManager.Instance.SetItemFlag(standItemFlag);
    }
}

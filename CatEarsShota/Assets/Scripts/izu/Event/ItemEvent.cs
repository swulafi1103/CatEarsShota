using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ItemEvent : EventBase, ICheckable
{
    [SerializeField, EnumFlags]
    protected ItemFlag standItemFlag = default;
    [SerializeField]
    ItemManager.ItemNum itemNum;

    void Start()
    {
        CheckFlag();
    }


    public virtual void Check()
    {
        ItemManager.Instance.SetItemData(itemNum);
        FlagManager.Instance.SetItemFlag(standItemFlag);
        gameObject.SetActive(false);
    }
}

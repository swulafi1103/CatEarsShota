using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBase : MonoBehaviour
{
    [SerializeField, HeaderAttribute("↓↓プランナーが編集する箇所↓↓")]
    protected Vector2 bubblePos = default;
    [SerializeField]
    protected bool flipBubble = default;
    protected bool isDisplayBubble;

    [SerializeField, EnumFlags]
    protected ItemFlag needItemFlag;
    [SerializeField, EnumFlags]
    protected GimmickFlag needGimmickFlag;
    [SerializeField, EnumFlags]
    protected GimmickFlag_Map2 needGimmickFlag_Map2;

    [HideInInspector]
    public bool isFinish = false;

    void Update()
    {
        
    }

    public virtual void Finished()
    {
        isFinish = true;
    }

    public virtual void Missed()
    {
        isFinish = false;
    }

    public virtual bool CheckFlag()
    {
        bool flag = FlagManager.Instance.CheckGimmickFlag(needGimmickFlag);
        isDisplayBubble = flag;
        if (isFinish)
        {
            isDisplayBubble = false;
        }
        DisplayBubble();
        return flag;
    }

    //  吹き出しの表示・非表示
    public virtual void DisplayBubble()
    {
        GameObject childObj;
        //  子の数が0だったらReturn
        if (transform.childCount == 0)
        {
            return;
        }
        childObj = transform.GetChild(0).gameObject;
        //  子の名前がBubbleじゃなかったらReturn
        if (childObj.name != "Bubble")
        {
            return;
        }
        //  表示
        if (isDisplayBubble)
        {
            childObj.GetComponent<SpriteRenderer>().sprite = BubbleEvent.Instance.ExclamationSprite;
            childObj.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y) + bubblePos;
            childObj.GetComponent<SpriteRenderer>().flipX = flipBubble;
            childObj.GetComponent<SpriteRenderer>().enabled = true;
        }
        //  非表示
        else
        {
            childObj.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

}

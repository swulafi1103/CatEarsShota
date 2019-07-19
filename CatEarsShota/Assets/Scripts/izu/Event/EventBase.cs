using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBase : MonoBehaviour
{
    [SerializeField, EnumFlags]
    protected ItemFlag needItemFlag;
    [SerializeField, EnumFlags]
    protected GimmickFlag needGimmickFlag;

    [SerializeField, Space(10)]
    private Sprite bubbleImage = default;
    [SerializeField]
    private Vector2 bubblePos = default;
    [SerializeField]
    private bool flipBubble = default;
    [SerializeField]
    protected bool isDisplayBubble;



    public virtual bool CheckFlag()
    {
        bool flag = FlagManager.Instance.CheckGimmickFlag(needGimmickFlag);
        if (flag)
        {
            isDisplayBubble = flag;
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
            //Debug.Log("吹き出し無し : " + gameObject.name);
            return;
        }
        childObj = transform.GetChild(0).gameObject;
        if (childObj.name != "Bubble")
        {
            //Debug.Log("吹き出し無しの名前 : " + childObj.name);
            return;
        }
        //  表示
        if (isDisplayBubble)
        {
            if (bubbleImage != null)
            {
                childObj.GetComponent<SpriteRenderer>().sprite = bubbleImage;
            }
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

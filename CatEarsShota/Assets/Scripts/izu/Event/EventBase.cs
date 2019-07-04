using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EventBase : MonoBehaviour
{
    [SerializeField, EnumFlags]
    protected ItemFlag needItemFlag;
    [SerializeField, EnumFlags]
    protected GimmickFlag needGimmickFlag;

    [SerializeField, Space(10)]
    private Sprite bubbleImage;
    [SerializeField]
    private Vector2 bubblePos;
    [SerializeField]
    private bool flipBubble;
    [SerializeField]
    protected bool isDisplayBubble;


    void OnValidate()
    {
        DisplayBubble();
    }

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
        if (transform.GetChild(0).name != "Bubble")
        {
            Debug.Log("吹き出しのエラー");
            return;
        }

        GameObject childObj = transform.GetChild(0).gameObject;
        
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
        else
        {
            childObj.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

}

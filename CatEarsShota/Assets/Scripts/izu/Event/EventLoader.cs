using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLoader : EventBase, ICheckable
{
    [SerializeField, EnumFlags, Space(3)]
    protected GimmickFlag standgimmickFlag;
    [SerializeField, EnumFlags]
    protected GimmickFlag_Map2 standgimmickFlag_Map2;

    [SerializeField]
    private CallType callType;
    public EventName eventName;

    //System.Action 

    private void Start()
    {
        switch (callType)
        {
            case CallType.MiniGame1Clear_Map1:
                EventManager.Instance.TypeinGameMap1ClearedFunc += () => EventManager.Instance.PlayEvent(eventName, gameObject);
                break;
            case CallType.MiniGame1Clear_Map2_FirstHalf:
                EventManager.Instance.TypeinGameMap2FirstClearedFunc += () => EventManager.Instance.PlayEvent(eventName, gameObject);
                break;
            case CallType.MiniGame1Clear_Map2_LatterHalf:
                EventManager.Instance.TypeinGameMap2LetterClearedFunc += () => EventManager.Instance.PlayEvent(eventName, gameObject);
                break;
            case CallType.MiniGame2Clear:
                EventManager.Instance.PieceGameClearedFunc += () => EventManager.Instance.PlayEvent(eventName, gameObject);
                break;
        }
    }

    public GimmickFlag NeedGimmickFlag { get { return needGimmickFlag; } }
    public GimmickFlag_Map2 NeedGimmickFlag_Map2 { get { return needGimmickFlag_Map2; } }

    public GimmickFlag StandgimmickFlag { get { return standgimmickFlag; } }
    public GimmickFlag_Map2 StandgimmickFlag_Map2 { get { return standgimmickFlag_Map2; } }

    public void Check()
    {
        if (FlagManager.Instance.CheckGimmickFlag(needGimmickFlag) && FlagManager.Instance.CheckGimmickFlag(needGimmickFlag_Map2))
        {
            EventManager.Instance.PlayEvent(eventName, gameObject);
            switch (callType)
            {
                case CallType.OnlyOnce:
                    gameObject.SetActive(false);
                    Finished();
                    return;
                case CallType.Always:
                    break;
            }
        }
    }

    //public override bool CheckFlag()
    //{
    //    bool flag = FlagManager.Instance.CheckGimmickFlag(needGimmickFlag);
    //    isDisplayBubble = flag;
    //    if (isFinish)
    //    {
    //        isDisplayBubble = false;
    //    }
    //    DisplayBubble();
    //    return flag;
    //}

    public override void DisplayBubble()
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
            BubbleResize(childObj);
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

    private void BubbleResize(GameObject childObj)
    {
        Vector2 lossScale = childObj.transform.lossyScale;
        Vector2 localScale = childObj.transform.localScale;
        childObj.transform.localScale = new Vector3(localScale.x / lossScale.x * 0.5f, localScale.x / lossScale.x * 0.5f, 1);        
    }

    private enum CallType
    {
        OnlyOnce,
        Always,
        MiniGame1Clear_Map1,
        MiniGame1Clear_Map2_FirstHalf,
        MiniGame1Clear_Map2_LatterHalf,
        MiniGame2Clear,
    }
}

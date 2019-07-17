using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchGeneretor_after : GimmickEvent
{
    private GameObject map;

    void Start()
    {
        CheckFlag();
        map = gameObject.transform.root.gameObject;
    }

    public override void Check()
    {
        if (FlagManager.Instance.CheckGimmickFlag(needGimmickFlag) && !isComplete)
        {
            FlagManager.Instance.SetGimmickFlag(standgimmickFlag);
            Fade.Instance.StartFade(0.5f, Color.black, () => MainCamera.Instance.TriggeredVideo(2));
            StartCoroutine(ChangeYellowColor(2));
        }
    }

    public override bool CheckFlag()
    {
        bool flag = FlagManager.Instance.CheckGimmickFlag(needGimmickFlag);
        if (flag)
        {
            isDisplayBubble = flag;
            gameObject.SetActive(flag);
        }
        if (isComplete)
        {
            isDisplayBubble = false;
        }
        DisplayBubble();
        return flag;
    }

    IEnumerator ChangeYellowColor(float delay)
    {
        yield return new WaitForSeconds(delay);
        map.GetComponent<MapStatus>().ChangeColorObj();
        CompleteGimmick();
        yield break;
    }


}

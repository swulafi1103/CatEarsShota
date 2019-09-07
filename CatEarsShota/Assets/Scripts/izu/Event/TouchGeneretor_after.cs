using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchGeneretor_after : GimmickEvent
{
    private GameObject map;

    [SerializeField]
    private GameObject limitObj = default;
    [SerializeField]
    private GameObject moyaObj = default;

    void Start()
    {
        CheckFlag();
        map = gameObject.transform.root.gameObject;
    }

    public override void Check()
    {
        if (FlagManager.Instance.CheckGimmickFlag(needGimmickFlag) && !isComplete)
        {
            SoundManager.Instance.PlayBGM(SoundManager.BGM_Name.BGM_02_Yellow);
            Fade.Instance.StartFade(0.5f, Color.black, () => MainCamera.Instance.TriggeredVideo(2));
            StartCoroutine(ChangeYellowColor(2));
        }
    }

    public override bool CheckFlag()
    {
        bool flag = FlagManager.Instance.CheckGimmickFlag(needGimmickFlag);
        gameObject.SetActive(flag);
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

    IEnumerator ChangeYellowColor(float delay)
    {
        yield return new WaitForSeconds(delay);
        map.GetComponent<MapStatus>().ChangeColorObj(0);
        limitObj.SetActive(false);
        moyaObj.SetActive(false);
        CompleteGimmick();
        FlagManager.Instance.SetGimmickFlag(standgimmickFlag);
        map.GetComponent<MapStatus>().UpdateGimmick(1, true);
        yield break;
    }


}

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
            StartCoroutine(ChangeYellowColor(0));
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
        //  フェード後、回想シーン動画の再生
        Fade.Instance.StartFade(0.5f, Color.black, () => MainCamera.Instance.TriggeredVideo(15));
        //Fade.Instance.StartFade(0.25f, Color.black);
        yield return new WaitForSeconds(1f);
        while (FlagManager.Instance.IsMovie == true)
            yield return null;
        yield return new WaitForSeconds(2f);
        //  フェード中か
        while (!Fade.Instance.Fading == false)
            yield return null;
        Debug.Log("Movie");
        MainCamera.Instance.TriggeredVideo(2);
        SoundManager.Instance.PlayBGM(SoundManager.BGM_Name.BGM_02_Yellow);
        
        map.GetComponent<MapStatus>().ChangeColorObj(0);
        
        limitObj.SetActive(false);
        moyaObj.SetActive(false);
        CompleteGimmick();
        FlagManager.Instance.SetGimmickFlag(standgimmickFlag);
        map.GetComponent<MapStatus>().UpdateGimmick(1, true);
        FlagManager.Instance.IsEventing = false;
        yield break;
    }


}

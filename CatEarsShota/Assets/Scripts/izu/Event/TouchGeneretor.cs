using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchGeneretor : GimmickEvent
{
    
    void Start()
    {
        CheckFlag();
    }

    
    void Update()
    {
        
    }

    public override void Check()
    {
        if (FlagManager.Instance.CheckGimmickFlag(needGimmickFlag) && !isComplete)
        {
            FlagManager.Instance.SetGimmickFlag(standgimmickFlag);
            Debug.Log("AAA");
            StartCoroutine(ChengeFran());
            CompleteGimmick();
        }
    }

    public override bool CheckFlag()
    {
        //  黄色のもやを表示
        bool flag = FlagManager.Instance.CheckGimmickFlag(needGimmickFlag);
        if (flag)
        {
            Debug.Log("黄色");
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            isDisplayBubble = flag;
        }
        if (isComplete)
        {
            isDisplayBubble = false;
        }
        DisplayBubble();
        return flag;
    }

    IEnumerator ChengeFran()
    {
        //  暗転
        bool flag = false;
        System.Action callback = () => flag = true;
        Fade.Instance.StartFade(1, Color.black, callback);
        yield return new WaitUntil(() => flag == true);        
        Debug.Log("暗転終了");
        FlagManager.Instance.IsEventing = true;
        yield return new WaitForSeconds(2f);
        //  アラーム音再生(3回)
        SoundManager.Instance.PlaySE(SoundManager.SE_Name.SE_00_Alerm, 0.25f);
        yield return new WaitForSeconds(0.25f);
        //  赤い演出(3回)
        Fade.Instance.CallFadeIO(3);
        //  ガラスが割れる音再生
        SoundManager.Instance.PlaySE(SoundManager.SE_Name.SE_01_BreakWin, 0.25f);
        yield return new WaitForSeconds(0.25f);
        //  水の流れる音再生
        SoundManager.Instance.PlaySE(SoundManager.SE_Name.SE_02_Wator, 0.25f);
        yield return new WaitForSeconds(3);
        //  時間軸変更
        FlagManager.Instance.ChegeFranPero();
        flag = false;
        //  明転
        Fade.Instance.StartFade(1, Color.clear, callback);
        yield return new WaitUntil(() => flag == true);
        Debug.Log("明転終了");
        //  フランの左右キョロキョロ
        PlayerManager.Instance.Fran.GetComponent<SpriteRenderer>().flipX = true;
        yield return new WaitForSeconds(0.5f);
        PlayerManager.Instance.Fran.GetComponent<SpriteRenderer>().flipX = false;
        yield return new WaitForSeconds(0.5f);
        PlayerManager.Instance.Fran.GetComponent<SpriteRenderer>().flipX = true;
        yield return new WaitForSeconds(0.5f);
        PlayerManager.Instance.Fran.GetComponent<SpriteRenderer>().flipX = false;
        yield return new WaitForSeconds(0.5f);
        FlagManager.Instance.IsEventing = false;
        yield return new WaitForSeconds(0.5f);
        BubbleEvent.Instance.DisplayBubbles(BubbleEvent.BubbleType.Escape);
        yield break;
    }

    IEnumerator subCoroutin(System.Action callback)
    {
        Debug.Log("aaa");
        yield return new WaitForSeconds(0.1f);
        Debug.Log("bbb");
        yield return new WaitForSeconds(0.1f);
        Debug.Log("ccc");
        yield return new WaitForSeconds(0.1f);
        callback();
        yield return null;
    }

}

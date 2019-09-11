﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchGeneretor : GimmickEvent
{

    [SerializeField]
    private GameObject moyaObj;

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
            StartCoroutine(ChengeFran());
        }
    }

    public override bool CheckFlag()
    {
        //  黄色のもやを表示
        bool flag = FlagManager.Instance.CheckGimmickFlag(needGimmickFlag);
        if (flag && !isComplete)
        {
            Debug.Log("黄色");
            moyaObj.SetActive(true);
            isDisplayBubble = flag;
        }
        if (isComplete)
        {
            isDisplayBubble = false;
            gameObject.SetActive(!flag);
        }
        DisplayBubble();
        return flag;
    }

    IEnumerator ChengeFran()
    {
        //  暗転
        bool flag = false;
        //  BGMのストップ
        SoundManager.Instance.StopBGM();
        System.Action callback = () => flag = true;
        Fade.Instance.StartFade(1, Color.black, callback);
        yield return new WaitUntil(() => flag == true);
        Debug.Log("暗転終了");
        FlagManager.Instance.IsEventing = true;
        //  チュートリアル発生
        TutorialContriller.Instance.ChangeModeTuto();
        //  チュートリアルが終わるまで待機
        yield return new WaitUntil(() => FlagManager.Instance.CheckGimmickFlag(GimmickFlag.G_05_Tuto_TimeChenge) == true);
        yield return new WaitForSeconds(2f);
        //  アラーム音再生(3回)
        SoundManager.Instance.PlaySE(SoundManager.SE_Name.SE_00_Alerm, 0.05f);
        //  オペレーターのボイス再生
        SoundManager.Instance.PlaySE(SoundManager.SE_Name.SE_21_Operetor);
        //  赤い演出(3回)
        Fade.Instance.CallFadeIO(3);
        yield return new WaitForSeconds(1);
        SoundManager.Instance.PlaySE(SoundManager.SE_Name.SE_00_Alerm, 0.05f);
        yield return new WaitForSeconds(1);
        SoundManager.Instance.PlaySE(SoundManager.SE_Name.SE_00_Alerm, 0.05f);
        yield return new WaitForSeconds(1);
        //  ガラスが割れる音再生
        SoundManager.Instance.PlaySE(SoundManager.SE_Name.SE_01_BreakWin, 0.05f);
        yield return new WaitForSeconds(0.25f);
        //  水の流れる音再生
        SoundManager.Instance.PlaySE(SoundManager.SE_Name.SE_02_Wator, 1f);
        yield return new WaitForSeconds(3);
        //  時間軸変更
        FlagManager.Instance.ChegeFranPero();
        flag = false;
        //  明転
        Fade.Instance.StartFade(1, Color.clear, callback);
        SoundManager.Instance.FadeInBGM(SoundManager.BGM_Name.BGM_06_Past1);
        yield return new WaitUntil(() => flag == true);
        Debug.Log("明転終了");
        FlagManager.Instance.IsEventing = true;
        //  フランの左右キョロキョロ
        PlayerManager.Instance.Fran.GetComponent<SpriteRenderer>().flipX = true;
        //  フランのボイス再生
        yield return new WaitForSeconds(0.5f);
        PlayerManager.Instance.Fran.GetComponent<SpriteRenderer>().flipX = false;
        yield return new WaitForSeconds(0.5f);
        PlayerManager.Instance.Fran.GetComponent<SpriteRenderer>().flipX = true;
        yield return new WaitForSeconds(0.5f);
        PlayerManager.Instance.Fran.GetComponent<SpriteRenderer>().flipX = false;
        yield return new WaitForSeconds(0.25f);
        BubbleEvent.Instance.DisplayBubbles(BubbleEvent.BubbleType.Escape);
        FlagManager.Instance.SetGimmickFlag(GimmickFlag.G_06_EscapeAlarm);
        SoundManager.Instance.PlaySE(SoundManager.SE_Name.SE_22_Fran_1);
        FlagManager.Instance.IsEventing = false;
        CompleteGimmick();
        yield break;
    }

}

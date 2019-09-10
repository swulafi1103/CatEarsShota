using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPastDoor_after : GimmickEvent, ICheckable
{

    void Start()
    {
        CheckFlag();   
    }


    public override void Check()
    {
        if (FlagManager.Instance.CheckGimmickFlag(needGimmickFlag))
        {
            //  ペローにチェンジ
            Debug.Log("EXIT");
            CompleteGimmick();
            FlagManager.Instance.SetGimmickFlag(standgimmickFlag);
            StartCoroutine(ChangePero());            
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Fran" && !FlagManager.Instance.IsEventing)
        {
            Check();
        }
    }

    IEnumerator ChangePero()
    {
        //  暗転
        bool flag = false;
        System.Action callback = () => flag = true;
        SoundManager.Instance.StopBGM();
        Fade.Instance.StartFade(1, Color.black, callback);
        yield return new WaitUntil(() => flag == true);
        FlagManager.Instance.IsEventing = true;
        yield return new WaitForSeconds(1);
        //  時間軸変更
        FlagManager.Instance.ChegeFranPero();
        flag = false;
        //  明転
        Fade.Instance.StartFade(1, Color.clear, callback);
        yield return new WaitUntil(() => flag == true);
        FlagManager.Instance.IsEventing = false;
        SoundManager.Instance.TimeChangeStartBGM();
        yield break;
    }

}

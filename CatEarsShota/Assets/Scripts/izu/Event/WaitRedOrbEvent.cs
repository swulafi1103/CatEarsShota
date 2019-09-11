﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WaitRedOrbEvent : MonoBehaviour
{
    [SerializeField]
    private GimmickFlag_Map2 needFlag = GimmickFlag_Map2.G_25_RedOrbAnimation;
    [SerializeField]
    private GimmickFlag_Map2 standFlag = GimmickFlag_Map2.G_26_LevingMovie;
    [SerializeField]
    private EventName selectEvent = EventName.E24_Alone_start_past;

    void Start()
    {
        EventManager.Instance._callBack_useHashigo += ForceToPero;
    }
    public void ForceToPero()
    {
        //  キノコの元を埋めずに進もうとしたら
        if (!FlagManager.Instance.CheckGimmickFlag(GimmickFlag_Map2.G_21_SetMushroomNoMoto_1))
        {
            StartCoroutine(ReversePos());
            return;
        }
        //  置き去りにされる動画の再生
        if (FlagManager.Instance.CheckGimmickFlag(needFlag) == true)
        {
            EventManager.Instance.PlayEvent(selectEvent, gameObject);
            FlagManager.Instance.SetGimmickFlag(standFlag);
            EventManager.Instance._callBack_useHashigo = null;
            gameObject.SetActive(false);
            return;
        }
        //  現在に強制的に戻され、過去に飛べなくする
        if (FlagManager.Instance.CheckGimmickFlag(needFlag) == false)
        {
            StartCoroutine(ChangePero());
            return;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ForceToPero();
    }
    IEnumerator ReversePos()
    {
        FlagManager.Instance.IsEventing = true;
        Fade.Instance.StartFade(0.5f, Color.black);
        while (!Fade.Instance.Fading == false)
            yield return null;
        PlayerManager.Instance.Fran.transform.position = gameObject.transform.position + new Vector3(3, -1, 0);
        yield return new WaitForSeconds(0.5f);
        Fade.Instance.ClearFade(0.5f, Color.clear);
        yield return new WaitForSeconds(1f);
        FlagManager.Instance.IsEventing = false;
        yield break;
    }
    IEnumerator ChangePero()
    {
        FlagManager.Instance.IsEventing = true;
        SoundManager.Instance.StopBGM();
        Fade.Instance.StartFade(1f, Color.black);
        while (!Fade.Instance.Fading == false)
            yield return null;
        yield return new WaitForSeconds(0.5f);
        FlagManager.Instance.IsLockPast = true;
        yield return null;
        FlagManager.Instance.ChageFranPero(true);
        Fade.Instance.ClearFade(0.5f, Color.clear);
        EventManager.Instance._callBack_useHashigo = null;
        SoundManager.Instance.TimeChangeStartBGM();
        FlagManager.Instance.IsEventing = false;
        yield break;
    }
}
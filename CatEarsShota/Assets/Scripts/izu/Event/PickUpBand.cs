using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBand : GimmickEvent, ICheckable
{
    //[SerializeField]
    //private GameObject videoStrage;

    void Start()
    {
        CheckFlag();
    }

    public override void Check()
    {
        if (FlagManager.Instance.CheckGimmickFlag(needGimmickFlag))
        {
            //bgm
            //SoundManager.Instance.PlayBGM(SoundManager.BGM_Name.BGM_01_Gray);
            //  バンドをつける動画の再生
            Fade.Instance.StartFade(0.5f, Color.black, () => MainCamera.Instance.TriggeredVideo(1));
            StartCoroutine(WaitMovieFinish());
            //  動画の長さを計算
            //float Duration = (float)videoStrage.GetComponent<VideoStorage>().VideoStore[1].frameCount / (float)videoStrage.GetComponent<VideoStorage>().VideoStore[1].frameRate;
            //  吹き出し表示(ドア)
            //BubbleEvent.Instance.DisplayBubbles(BubbleEvent.BubbleType.Door, Duration);
            //  フラグSET
            //FlagManager.Instance.SetGimmickFlag(standgimmickFlag);
            //gameObject.SetActive(false);
            Finished();
        }
    }

    IEnumerator WaitMovieFinish()
    {
        yield return new WaitForSeconds(0.6f);
        while (FlagManager.Instance.IsMovie == true)
            yield return null;
        //  吹き出し表示(ドア)
        BubbleEvent.Instance.DisplayBubbles(BubbleEvent.BubbleType.Door, 0.5f);
        //  フラグSET
        FlagManager.Instance.SetGimmickFlag(standgimmickFlag);
        gameObject.SetActive(false);
        yield break;
    }
}

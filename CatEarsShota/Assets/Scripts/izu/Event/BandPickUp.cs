using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandPickUp : GimmickEvent, ICheckable
{
    [SerializeField]
    private GameObject videoStrage;

    void Start()
    {
        CheckFlag();
    }

    public override void Check()
    {
        if (FlagManager.Instance.CheckGimmickFlag(needGimmickFlag))
        {
            Fade.Instance.StartFade(0.5f, Color.black, () => MainCamera.Instance.TriggeredVideo(1));
            float Duration = (float)videoStrage.GetComponent<VideoStorage>().VideoStore[1].frameCount / (float)videoStrage.GetComponent<VideoStorage>().VideoStore[1].frameRate;
            BubbleEvent.Instance.DisplayBubbles(BubbleEvent.BubbleType.Door, Duration);
            Debug.Log("Duration = " + Duration);
            FlagManager.Instance.SetGimmickFlag(standgimmickFlag);

            gameObject.SetActive(false);
        }
    }
}

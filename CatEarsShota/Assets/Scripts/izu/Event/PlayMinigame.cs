using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMinigame : GimmickEvent, ICheckable
{
    [HideInInspector]
    public bool isOpenMinigame = false;
    [SerializeField]
    private GameObject FocusDoor;

    void Start()
    {
        CheckFlag();
    }

    public override void Check()
    {
        if (FlagManager.Instance.CheckGimmickFlag(needGimmickFlag))
        {
            if (!isOpenMinigame)
            {
                //  ミニゲームの説明表示
                isOpenMinigame = true;
                MiniGameManager.Instance.TouchGenerator(0);
                MiniGameManager.Instance.generetor1 = gameObject;
            }            
        }
        if (!FlagManager.Instance.CheckGimmickFlag(needGimmickFlag))
        {
            Debug.Log("False");
        }
        
    }

    public void MiniGameClear()
    {
        StartCoroutine(DoorOpenEvent());
    }

    IEnumerator DoorOpenEvent()
    {
        Debug.Log("MiniGameClear");
        //  ドアのほうにカメラを移動する
        MainCamera.Instance.T_ChangeFocus(FocusDoor);
        //  ドアを開ける
        //transform.root.gameObject.GetComponent<MapStatus>().MapObjectState[1] = true;
        //  ドアのコライダーの無効化
        FocusDoor.GetComponent<BoxCollider2D>().enabled = false;
        FlagManager.Instance.SetGimmickFlag(GimmickFlag.G_10_OpenDoor);
        yield break;
    }
}

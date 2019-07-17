using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMinigame : GimmickEvent, ICheckable
{
    [SerializeField]
    private GameObject miniGameManager = default;
    [SerializeField]
    private GameObject rangeObject = default;
    public bool isOpenMinigame = false;
    private bool isFirstMinigame = false;

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
                //  最初だけ説明を表示
                if (!isFirstMinigame)
                {
                    isOpenMinigame = true;
                    miniGameManager.GetComponent<MiniGameManager>().TouchGenerator();
                    miniGameManager.GetComponent<MiniGameManager>().generetor = gameObject;
                    isFirstMinigame = true;
                    return;
                }
                //
                isOpenMinigame = true;
                miniGameManager.GetComponent<MiniGameManager>().StartMiniGame();
                miniGameManager.GetComponent<MiniGameManager>().generetor = gameObject;
                //Debug.Log(needGimmickFlag + " : TRUE");
                //rangeObject.SetActive(false);
            }            
        }
        if (!FlagManager.Instance.CheckGimmickFlag(needGimmickFlag))
        {
            Debug.Log("False");
        }
        
    }

    public void MiniGameCler()
    {

    }
}

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
                miniGameManager.GetComponent<MiniGameManager>().TouchGenerator();
                isOpenMinigame = true;
                Debug.Log(needGimmickFlag + " : TRUE");
                rangeObject.SetActive(false);
            }
            
            //gameObject.SetActive(false);
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

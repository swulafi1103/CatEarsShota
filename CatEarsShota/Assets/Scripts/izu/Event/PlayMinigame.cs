using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMinigame : GimmickEvent, ICheckable
{
    [SerializeField]
    private GameObject miniGameManager;
    [SerializeField]
    private GameObject rangeObject;
    public bool isOpenMinigame = false;
    void Start()
    {
        CheckFlag();
    }

    //private void Update()
    //{

    //}


    public override void Check()
    {
        //base.Check();
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
}

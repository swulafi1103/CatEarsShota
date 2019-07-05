using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMinigame : GimmickEvent, ICheckable
{
    [SerializeField]
    private GameObject miniGameManager;
    void Start()
    {
        CheckFlag();
    }

    private void Update()
    {

        Debug.Log("GENERETOR" + GetComponent<BoxCollider2D>().enabled);
        //if (Input.GetKeyDown(KeyCode.Return))
        //{
        //    Debug.Log("aaaa");
        //    miniGameManager.GetComponent<MiniGameManager>().TouchGenerator();
        //}
    }


    public override void Check()
    {
        //base.Check();
        if (FlagManager.Instance.CheckGimmickFlag(needGimmickFlag))
        {
            MiniGameManager.Instance.TouchGenerator();
            Debug.Log(needGimmickFlag + " : TRUE");
            //gameObject.SetActive(false);
        }
        if (!FlagManager.Instance.CheckGimmickFlag(needGimmickFlag))
        {
            Debug.Log("False");
        }
        
    }
}

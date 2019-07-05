using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchGeneretor : GimmickEvent
{
    
    void Start()
    {
        CheckFlag();
    }

    
    void Update()
    {
        
    }

    public override void Check()
    {
        if (FlagManager.Instance.CheckGimmickFlag(needGimmickFlag))
        {
            Debug.Log(needGimmickFlag + " : TRUE");
            StartCoroutine(ChengeFran());
            gameObject.SetActive(false);
        }
    }

    public override bool CheckFlag()
    {
        //  黄色のもやを表示
        bool flag = FlagManager.Instance.CheckGimmickFlag(needGimmickFlag);
        if (flag)
        {
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            isDisplayBubble = flag;
        }
        DisplayBubble();
        return flag;
    }

    IEnumerator ChengeFran()
    {
        Fade.Instance.StartFadeInOut(1, Color.white);
        yield break;
    }

}

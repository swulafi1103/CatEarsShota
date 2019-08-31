using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoyaScript : MonoBehaviour
{
    [SerializeField]
    private GameObject[] moyaObjects;
    [SerializeField]
    private GameObject[] focusObjects;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void DisplayMoya()
    {
        for (int i = 0; i < moyaObjects.Length; i++)
        {
            if (FlagManager.Instance.CheckGimmickFlag(focusObjects[i].GetComponent<EventLoader>().NeedGimmickFlag) && FlagManager.Instance.CheckGimmickFlag(focusObjects[i].GetComponent<EventLoader>().NeedGimmickFlag))
                Debug.Log("aaa");
                    //if (focusObjects[i].GetComponent<EventLoader>().)
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCultureDevice : GimmickEvent
{
    void Start()
    {
        FlagManager.Instance.SetGimmickFlag(standgimmickFlag);
        //gameObject.transform.root.GetComponent<MapStatus>().MapObjectState[0] = true;
    }

    void Update()
    {
        
    }
}

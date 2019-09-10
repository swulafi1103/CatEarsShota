using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccesDoor : GimmickEvent, ICheckable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Check()
    {
        if (!FlagManager.Instance.CheckItemFlag(needItemFlag))
        {
            Debug.Log(needGimmickFlag + " : False");
            BubbleEvent.Instance.DisplayBubbles(BubbleEvent.BubbleType.Tana);
        }
        if (FlagManager.Instance.CheckItemFlag(needItemFlag))
        {
            Debug.Log(needGimmickFlag + " : TRUE");
            //gameObject.transform.root.GetComponent<MapStatus>().MapObjectState[1] = true;
            MainCamera.Instance.MovingMap(1);
            //  ペローがマップ2移動時にフランも一緒に移動
            MainCamera.Instance.MovingMapnotEventing(3);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.name == "Pero" && !FlagManager.Instance.IsEventing) {
            Check();
        }
    }
}

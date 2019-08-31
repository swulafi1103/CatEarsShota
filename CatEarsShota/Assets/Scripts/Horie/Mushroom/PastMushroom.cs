using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastMushroom : MonoBehaviour
{
    bool NotSet = true;

    [SerializeField,Range(0,1)]
    int MushNum = 0;

    [SerializeField]
    NowMushroom nowMushroom;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update() {
        //if(Input.GetKeyDown(KeyCode.U)) SetMushroom();  //debug
        if (!NotSet) return;
        if (MushNum == 0)
        {
            //if(FlagManager.Instance.CheckGimmickFlag(GimmickFlag))return;
            //いっこめ用のフラグが立ってたらreturn
        }
        else
        {
            //if(FlagManager.Instance.CheckGimmickFlag(GimmickFlag))return;
            //いっこめ用のフラグが立ってなかったらreturn
        }
        if (ItemManager.Instance.SelectedEventItem(ItemManager.ItemNum.Mushroom)) {
            OnMushroom();
        }
    }

    public void SetMushroom() {
        ItemManager.Instance.SetEventUI(ItemManager.ItemNum.Mushroom);
    }

    void OnMushroom() {
        Debug.Log("Mushroom Set");
        NotSet = false;
        nowMushroom.SetMush();
        if (MushNum == 0)
        {
            //flag変え
        }
    }

    public IEnumerable test()
    {

        yield break;
    }
}

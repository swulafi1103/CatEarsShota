using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastMushroom : MonoBehaviour
{
    

    [SerializeField]
    NowMushroom nowMushroom;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update() {
        //if(Input.GetKeyDown(KeyCode.U)) SetMushroom();  //debug
        //if (!NotSet) return;
        //if (MushNum == 0)
        //{
        //    //if(FlagManager.Instance.CheckGimmickFlag(GimmickFlag))return;
        //    //いっこめ用のフラグが立ってたらreturn
        //}
        //else
        //{
        //    //if(FlagManager.Instance.CheckGimmickFlag(GimmickFlag))return;
        //    //いっこめ用のフラグが立ってなかったらreturn
        //}
        //if (ItemManager.Instance.SelectedEventItem(ItemManager.ItemNum.Mushroom)) {
        //    OnMushroom();
        //}
    }

    public void SetMushroom() {
        //ItemManager.Instance.SetEventUI(ItemManager.ItemNum.Mushroom);
        ItemManager.Instance.SelectEvent(ItemManager.ItemNum.Mushroom, OnMushroom);
    }

    void OnMushroom() {
        Debug.Log("Mushroom Set");
        nowMushroom.SetMush();
        ItemManager.Instance.SetItemData(ItemManager.ItemNum.Mushroom);
    }
    
}

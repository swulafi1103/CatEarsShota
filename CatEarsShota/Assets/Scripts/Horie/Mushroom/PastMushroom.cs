using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastMushroom : MonoBehaviour
{
    bool NotSet = true;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update() {
        //if(Input.GetKeyDown(KeyCode.U)) SetMushroom();  //debug
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
    }
}

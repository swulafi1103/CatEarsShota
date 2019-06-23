using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetailPanel : MonoBehaviour
{

    Image detail;
    

    /// <summary>
    /// アイテム詳細表示
    /// </summary>
    /// <param name="item"></param>
    public void SetDetail(ItemData item) {

        if (detail == null) {
            detail = GetComponent<Image>();
        }

        detail.sprite = item.GetDetailText;
    }
    
}

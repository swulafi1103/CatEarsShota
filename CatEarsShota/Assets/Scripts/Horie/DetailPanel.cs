using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetailPanel : MonoBehaviour
{
    [SerializeField]
    private Image icon;
    [SerializeField]
    private Text itemName;
    [SerializeField]
    private  Text detailText;
    [SerializeField]
    private Image reportImage;

    /// <summary>
    /// アイテム詳細表示
    /// </summary>
    /// <param name="item"></param>
    public void SetItem(ItemData item) {

        SetImages(true);

        icon.sprite = item.GetItemSprite;
        itemName.text = item.GetItemName;
        detailText.text = item.GetDetailText;
    }

    /// <summary>
    /// レポート拡大表示
    /// </summary>
    /// <param name="item"></param>
    public void SetReport(ItemData item) {

        SetImages(false);

        reportImage.sprite = item.GetReportSprite;
    }

    private  void SetImages(bool act) {
        icon.gameObject.SetActive(act);
        itemName.gameObject.SetActive(act);
        detailText.transform.parent.gameObject.SetActive(act);
        detailText.gameObject.SetActive(act);

        reportImage.gameObject.SetActive(!act);
    }
}

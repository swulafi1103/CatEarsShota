using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUIPiece : MonoBehaviour
{
    [SerializeField]
    private Image ItemImage;
    [SerializeField]
    private Image ShadowImage;

    /// <summary>
    /// アイテムアイコン表示
    /// </summary>
    /// <param name="item"></param>
    public void SetImage(ItemData item) {
        SetShadow(false);

        ItemImage.sprite = item.GetItemSprite;
    }

    /// <summary>
    /// 影表示
    /// </summary>
    /// <param name="act"></param>
    private void SetShadow(bool act) {
        ShadowImage.gameObject.SetActive(act);
    }
}

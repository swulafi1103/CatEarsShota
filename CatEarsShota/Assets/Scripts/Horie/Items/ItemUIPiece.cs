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
    private ItemData nowItem;

    Vector2 defaltSize;

    Image thisImage;
    RectTransform trans;

    private void Start() {
        thisImage = GetComponent<Image>();
        trans = GetComponent<RectTransform>();
        defaltSize = ItemImage.rectTransform.sizeDelta;
    }

    /// <summary>
    /// アイテムアイコン表示
    /// </summary>
    /// <param name="item"></param>
    public void SetImage(ItemData item) {
        SetShadow(false);
        nowItem = item;
        ItemImage.sprite = nowItem.GetItemSprite;
        ThisSelected(false);
    }

    public void ThisSelected(bool select) {
        if (select) {
            thisImage.enabled = false;
            ItemImage.sprite = nowItem.GetSelecticon;
            ItemImage.SetNativeSize();
        }
        else
        {
            thisImage.enabled = true;
            ItemImage.sprite = nowItem.GetItemSprite;
            ItemImage.rectTransform.sizeDelta = defaltSize;
        }
    }

    /// <summary>
    /// 影表示
    /// </summary>
    /// <param name="act"></param>
    public void SetShadow(bool act) {
        ShadowImage.gameObject.SetActive(act);
    }
}

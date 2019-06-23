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

    [SerializeField]
    Sprite[] baseImage = new Sprite[2];

    Image thisImage;
    RectTransform trans;

    private void Start() {
        thisImage = GetComponent<Image>();
        trans = GetComponent<RectTransform>();
    }

    /// <summary>
    /// アイテムアイコン表示
    /// </summary>
    /// <param name="item"></param>
    public void SetImage(ItemData item) {
        SetShadow(false);
        ThisSelected(false);
        ItemImage.sprite = item.GetItemSprite;
    }

    public void ThisSelected(bool select) {
        if (select) {
            trans.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            thisImage.sprite = baseImage[0];
        }
        else {
            trans.localScale = Vector3.one;
            thisImage.sprite = baseImage[1];
        }
    }

    /// <summary>
    /// 影表示
    /// </summary>
    /// <param name="act"></param>
    private void SetShadow(bool act) {
        ShadowImage.gameObject.SetActive(act);
    }
}

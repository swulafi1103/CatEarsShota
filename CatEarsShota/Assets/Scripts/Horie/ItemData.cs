using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="New Item",menuName ="CreateItem")]
public class ItemData : ScriptableObject 
{
    public enum ItemType {
        Nomal=0,
        TextReport,
        SpriteReport,
        Pants
    };

    /// <summary>
    /// アイテムの種類
    /// </summary>
    [SerializeField]
    private ItemType type;

    public ItemType GetItemType {
        get { return type; }
    }

    /// <summary>
    /// ペローアイテム.elsxをもとに振っていく
    /// </summary>
    [SerializeField]
    private int itemNum;

    public int GetItemNum {
        get { return itemNum; }
    }

    /// <summary>
    /// アイテム欄に載るアイテム名(載せるのなら)
    /// </summary>
    [SerializeField]
    private string itemName;

    public string GetItemName {
        get { return itemName; }
    }

    /// <summary>
    /// アイテム欄に載る画像
    /// </summary>
    [SerializeField]
    private Sprite itemSprite;

    public Sprite GetItemSprite {
        get { return itemSprite; }
    }

    /// <summary>
    /// レポートのみ表示される文章を書く
    /// (その他はitemManagerの方で取得しないようにする)
    /// </summary>
    [SerializeField]
    [Multiline]
    private string reportText;

    public string GetReportText {
        get { return reportText; }
    }

    /// <summary>
    /// レポートのみ表示されるイラスト
    /// (その他はitemManagerの方で取得しないようにする)
    /// </summary>
    [SerializeField]
    private Sprite reportSprite;

    public Sprite GetReportSprite {
        get { return reportSprite; }
    }
}

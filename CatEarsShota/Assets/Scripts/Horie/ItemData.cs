using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="New Item",menuName ="CreateItem")]
public class ItemData : ScriptableObject 
{
    public enum ItemType {
        Nomal=0,
        Report,
        Pants
    };

    [SerializeField]
    private ItemType type;

    public ItemType GetItemType {
        get { return type; }
    }

    [SerializeField]
    private int itemNum;

    public int GetItemNum {
        get { return itemNum; }
    }

    [SerializeField]
    private string itemName;

    public string GetItemName {
        get { return itemName; }
    }

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

}

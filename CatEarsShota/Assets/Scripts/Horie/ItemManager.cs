using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    //[SerializeField]
    //private List<ItemData> itemList = new List<ItemData>();

    private static ItemManager instance;
    public static ItemManager Instance {
        get { return instance; }
    }

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// アイテム欄でAキーを押されたとき
    /// </summary>
    public void ItemClickd(ItemData item) {
        switch (item.GetItemType) {
            case ItemData.ItemType.Nomal:
                break;
            case ItemData.ItemType.SpriteReport:
                break;
            case ItemData.ItemType.Pants:
                ChangePants(item.GetItemNum);
                break;
        }
    }

    /// <summary>
    /// パンツ切り替え
    /// </summary>
    /// <param name="num"></param>
    private void ChangePants(int num) {
        //getpuntsnum(num-12);  //伊豆先輩の関数呼び出す
    }
}

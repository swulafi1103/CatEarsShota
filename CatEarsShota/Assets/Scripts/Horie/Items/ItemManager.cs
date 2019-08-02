using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    private List<ItemData> itemList = new List<ItemData>();

    private static ItemManager instance;
    public static ItemManager Instance {
        get { return instance; }
    }

    private bool isFran;


    /// <summary>
    /// playerがペローかフランか判定
    /// </summary>
    bool IsFran()
    {
        isFran = FlagManager.Instance.IsPast;
        return isFran;
    }

    PlayerItems Fran;
    PlayerItems Perrault;

    ItemUIController UIContriller;

    public enum ItemNum {
        Yerrow_Orb = 0,
        Blue_Orb,
        Green_Orb,
        Red_Orb,
        Report_88,
        Diary_Fran,
        Instructions,
        CardKey,
        Mushroom,
        Ilust_Piece,
        Picture_Book,
        BookMark_now,
        Pants_1,
        Pants_2,
        Pants_3,
        Pants_4,
        Pants_5,
        Pants_6,
        BookMark_Past
    }

    private void Awake() {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        SetData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void SetData() {
        UIContriller = GetComponent<ItemUIController>();
        Perrault = null;
        Fran = null;
        PlayerItems[] players = GetComponents<PlayerItems>();
        foreach (PlayerItems player in players) {
            if (player.GetTypes == PlayerItems.PlayerType.Perrault) {
                Perrault = player;
            }
            if (player.GetTypes == PlayerItems.PlayerType.Fran) {
                Fran = player;
            }

            if (Perrault != null && Fran != null) break;
        }
        
    }

    /// <summary>
    /// UIにそのタブで表示する今持ってるアイテムのみ渡す
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public List<ItemData> GetNowData(int t) {
        List<ItemData> nowList = new List<ItemData>();
        List<ItemData> allList = new List<ItemData>();
        List<bool> HaveData = new List<bool>();

        ItemData.ItemType types = (ItemData.ItemType)Enum.ToObject(typeof(ItemData.ItemType), t);

        if (IsFran()) {
            HaveData = Fran.GetTypeHaveData(types);
            allList = Fran.GetTypeImage(types);
        }
        else {
            HaveData = Perrault.GetTypeHaveData(types);
            allList = Perrault.GetTypeImage(types);
        }

        for (int i = 0; i < HaveData.Count; i++) {
            if (HaveData[i]) {
                nowList.Add(allList[i]);
            }
        }

        return nowList;
    }

    /// <summary>
    /// アイテム取得情報変更（ペローはfalse,フランはtrue）
    /// </summary>
    /// <param name="item">アイテムデータ</param>
    /// <param name="fran">ペローはfalse,フランはtrue</param>
    public void SetItemData(ItemNum itemNum) {

        int num = (int)itemNum;
        ItemData item = itemList[num];

        if (IsFran()) {
            Fran.SetItemGet(item);
        }
        else {
            Perrault.SetItemGet(item);
        }

        if (item.GetItemType == ItemData.ItemType.SpriteReport) {
            UIContriller.SetDetail(item);
        }
    }

    /// <summary>
    /// パンツ切り替え
    /// </summary>
    /// <param name="num"></param>
    public void ChangePants(int num) {
        PlayerManager.Instance.Pero.transform.GetChild(1).GetComponent<Animator>().SetInteger("pants_number", num - 12);
        Debug.Log(num - 12);
    }

    /// <summary>
    /// 特定のアイテムを持っているか
    /// </summary>
    /// <param name="num"></param>
    /// <param name="isPerrault"></param>
    /// <returns></returns>
    public bool IsGet(ItemNum itemNum) {
        int num = (int)itemNum;
        ItemData item = itemList[num];
        ItemData.ItemType types = item.GetItemType;
        List<ItemData> allList = new List<ItemData>();
        List<bool> HaveData = new List<bool>();

        if (IsFran())
        {
            allList = Fran.GetTypeImage(types);
            HaveData = Fran.GetTypeHaveData(types);
        }
        else
        {
            allList = Perrault.GetTypeImage(types);
            HaveData = Perrault.GetTypeHaveData(types);
        }
        bool have = false;
        for(int i = 0; i < allList.Count; i++) {
            if (item == allList[i]) {
                have = HaveData[i];
            }
        }
        return have;
    }

    /// <summary>
    /// 特定のアイテムしか選択できないアイテム欄出現
    /// </summary>
    /// <param name="item"></param>
    public void SetEventUI(ItemNum item) {
        int num = (int)item;
        ItemData data = itemList[num];
        UIContriller.SetEventUI(data);
    }

    public void SetEvents(ItemNum[] list)
    {
        ItemData[] datas = new ItemData[list.Length];
        for(int i = 0; i < list.Length; i++)
        {
            int num = (int)list[i];
            datas[i] = itemList[num];
        }
        UIContriller.SetEvents(datas);
    }

    /// <summary>
    /// 特定のアイテムしか選択できないアイテム欄出現後、アイテムを選択したか
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool SelectedEventItem(ItemNum item) {
        int num = (int)item;
        ItemData data = itemList[num];
        bool select = UIContriller.EndEventUI(data);
        return select;
    }
    
    public void SetItemUI() {
        UIContriller.StartItemUI();
    }
}

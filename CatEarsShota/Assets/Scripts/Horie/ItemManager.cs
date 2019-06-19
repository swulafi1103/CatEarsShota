using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemManager : MonoBehaviour
{
    //[SerializeField]
    //private List<ItemData> itemList = new List<ItemData>();

    private static ItemManager instance;
    public static ItemManager Instance {
        get { return instance; }
    }

    private bool isFran;
    public bool IsFran {
        get {
            GetPlayer();
            return isFran;
        }
    }

    PlayerItems Fran;
    PlayerItems Perrault;

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

        if (IsFran) {
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
    /// playerがペローかフランか判定
    /// </summary>
    void GetPlayer() {
        //仮
        isFran = false;
    }

    /// <summary>
    /// アイテム取得情報変更（ペローはfalse,フランはtrue）
    /// </summary>
    /// <param name="item">アイテムデータ</param>
    /// <param name="fran">ペローはfalse,フランはtrue</param>
    public void SetItemData(ItemData item,bool fran) {

        if (fran) {
            Fran.SetItemGet(item);
        }
        else {
            Perrault.SetItemGet(item);
        }
    }

    /// <summary>
    /// パンツ切り替え
    /// </summary>
    /// <param name="num"></param>
    public void ChangePants(int num) {
        //getpuntsnum(num-12);  //伊豆先輩の関数呼び出す
        Debug.Log(num - 12);
    }
}

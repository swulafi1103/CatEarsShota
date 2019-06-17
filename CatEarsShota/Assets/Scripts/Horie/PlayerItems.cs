using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour {

    public enum PlayerType {
        Perrault,
        Fran
    }

    [SerializeField]
    private PlayerType playerType;
    public PlayerType GetTypes {
        get { return playerType; }
    }

    [SerializeField]
    private List<ItemData> canGetItems = new List<ItemData>();
    private List<bool> itemIsGet = new List<bool>();

    [SerializeField]
    ItemData PerraultPants;

    // Start is called before the first frame update
    void Awake() {
        SetStartData();
        SetPerraultPants();
    }

    // Update is called once per frame
    void Update() {

    }

    /// <summary>
    /// 持ち物情報初期化
    /// </summary>
    void SetStartData() {
        for (int i = 0; i < canGetItems.Count; i++) {
            itemIsGet.Add(true);
        }
    }

    public void SetItemGet(ItemData item) {
        int num = canGetItems.Count;
        for(int i = 0; i < canGetItems.Count; i++) {
            if (canGetItems[i] == item) {
                num = i;
            }
        }

        if(num == canGetItems.Count) {
            Debug.Log("this item cannot have :" + item.GetItemName);
            return;
        }

        itemIsGet[num] = !itemIsGet[num];
    }

    public List<bool> GetTypeHaveData(ItemData.ItemType type) {
        List<bool> list = new List<bool>();

        for (int i = 0; i < canGetItems.Count; i++) {
            if (canGetItems[i].GetItemType == type) {
                list.Add(itemIsGet[i]);
            }
        }

        return list;
    }

    public List<ItemData> GetTypeImage(ItemData.ItemType type) {
        List<ItemData> list = new List<ItemData>();
        for (int i = 0; i < canGetItems.Count; i++) {
            if (canGetItems[i].GetItemType == type) {
                list.Add(canGetItems[i]);
            }
        }
        return list;

    }

    /// <summary>
    /// ペローのかぼちゃパンツのみ初期持ち
    /// </summary>
    private void SetPerraultPants() {
        if (playerType != PlayerType.Perrault) return;

        for(int i = 0; i < canGetItems.Count; i++) {
            if (canGetItems[i] == PerraultPants) {
                itemIsGet[i] = true;
                break;
            }
        }
    }
}
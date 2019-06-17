using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ItemUIController : MonoBehaviour
{
    [SerializeField]
    Image[] tags = new Image[3];
    int selectTab = 0;
    int[] selectMaxs = new int[2] { 2, 3 };

    [SerializeField]
    Color NonSelectColor;
    Color SelectedColor = Color.white;

    [SerializeField]
    GameObject[] items = new GameObject[6];

    List<bool> HaveData = new List<bool>();
    List<ItemData> ItemImage = new List<ItemData>();

    PlayerItems Perrault;
    PlayerItems Fran;

    bool IsFran = false;

    bool IsActUI = false;

    void Start() {
        Perrault = null;
        Fran = null;
        SetData();
        SetTabColor();
        SetIamges();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.D)) {

        }
        

        if (Input.GetKeyDown(KeyCode.F)) {
            selectTab++;

            if (IsFran) {
                if (selectTab == selectMaxs[0]) selectTab = 0;
            }
            else {
                if (selectTab == selectMaxs[1]) selectTab = 0;
            }
            SetTabColor();
            SetIamges();
        }
    }

    private void SetData() {
        PlayerItems[] players = GetComponents<PlayerItems>();
        foreach(PlayerItems player in players) {
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
    /// タブ切り替え
    /// </summary>
    private void SetTabColor() {
        for(int i = 0; i < tags.Length; i++) {
            if (i == selectTab) {
                tags[i].color = SelectedColor;
            }
            else {
                tags[i].color = NonSelectColor;
            }
        }
    }

    private void SetIamges() {
        //HaveData.Clear();
        //ItemImage.Clear();
        ItemData.ItemType types = (ItemData.ItemType)Enum.ToObject(typeof(ItemData.ItemType), selectTab);

        if (IsFran) {
            HaveData = Fran.GetTypeHaveData(types);
            ItemImage = Fran.GetTypeImage(types);
        }
        else {
            HaveData = Perrault.GetTypeHaveData(types);
            ItemImage = Perrault.GetTypeImage(types);
        }


        for(int i = 0; i < items.Length; i++) {
            bool act = i < HaveData.Count;
            items[i].SetActive(act);

            if (act) {
                items[i].GetComponent<ItemUIPiece>().ShadowImage.gameObject.SetActive(false);

                if (HaveData[i]) {
                    items[i].GetComponent<ItemUIPiece>().ItemImage.sprite = ItemImage[i].GetItemSprite;
                }
                else {
                    items[i].GetComponent<ItemUIPiece>().ItemImage.sprite = null;
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ItemUIController : MonoBehaviour
{
    [SerializeField]
    Sprite[] PanelSprites = new Sprite[3];
    [SerializeField]
    Image ImagePanel;

    int selectTab = 0;
    int[] selectMaxs = new int[2] { 2, 3 };

    int selectNum = 0;

    [SerializeField]
    GameObject[] items = new GameObject[6];

    [SerializeField]
    GameObject ItemPanel;

    [SerializeField]
    GameObject DetailPanel;

    [SerializeField]
    GameObject SelectImage;

    [SerializeField]
    Animator PlayerAnim;

    float AnimFrame = 8;

    List<ItemData> NowHave = new List<ItemData>();
    

    bool IsFran = false;

    bool IsActUI = false;

    bool IsActDetail = false;

    void Start() {
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.D)) {
            IsActUI = !IsActUI;
            IsActDetail = false;
            StartCoroutine(SetItemUI(IsActUI));
        }

        if (!IsActUI) return;
        PushCancel();
        
        PushGangeTab();
        
        PushShowDetail();

        PushMoveCursor();

    }

    /// <summary>
    /// タブ切り替え入力
    /// </summary>
    void PushGangeTab() {
        if (!Input.GetKeyDown(KeyCode.F)) return;

        if (IsActDetail) return;

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

    /// <summary>
    /// 詳細ウィンドウ切り替え
    /// </summary>
    void PushShowDetail() {
        if (!Input.GetKeyDown(KeyCode.A)) return;
        if (!SelectImage.gameObject.activeSelf) return;
        StartCoroutine(SetDetailUI(true));
    }

    /// <summary>
    /// キャンセルキー
    /// </summary>
    void PushCancel() {
        if (!Input.GetKeyDown(KeyCode.X)) return;

        if (IsActDetail) {
            StartCoroutine(SetDetailUI(false));
        }
        else {
            StartCoroutine(SetItemUI(false));
            
        }
    }

    /// <summary>
    /// カーソル移動入力
    /// </summary>
    void PushMoveCursor() {
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            MoveSelectIcon(true);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            MoveSelectIcon(false);
        }
    }

    /// <summary>
    /// 持っているアイテムのみカーソルが動く
    /// </summary>
    /// <param name="right"></param>
    void MoveSelectIcon(bool right) {

        if (!SelectImage.gameObject.activeSelf) return;

        int moves = right ? 1 : -1;
        
        selectNum += moves;
        if (selectNum >= NowHave.Count) selectNum = 0;
        if (selectNum >= items.Length) selectNum = 0;
        if (selectNum < 0 && items.Length > NowHave.Count) selectNum = NowHave.Count - 1;
        if (selectNum < 0 && items.Length <= NowHave.Count) selectNum = items.Length - 1;

        Vector3 pos = items[selectNum].GetComponent<RectTransform>().localPosition;
        SelectImage.GetComponent<RectTransform>().localPosition = pos;
    }

    /// <summary>
    /// カーソル初期設定
    /// </summary>
    void FirstSelectIcon() {
        if (NowHave.Count == 0) {
            SelectImage.gameObject.SetActive(false);
        }
        else {
            SelectImage.gameObject.SetActive(true);
            selectNum = 0;
            Vector3 pos = items[selectNum].GetComponent<RectTransform>().localPosition;
            SelectImage.GetComponent<RectTransform>().localPosition = pos;
        }
    }

    /// <summary>
    /// アイテム欄の表示、非表示
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    IEnumerator SetItemUI(bool act) {

        Vector3 startPos;
        Vector3 endPos;

        if (act) {
            IsFran = ItemManager.Instance.IsFran;

            SetAnim();
            selectTab = 0;
            SetTabColor();
            SetIamges();

            startPos = new Vector3(0, -1080, 0);
            endPos = Vector3.zero;
        }
        else {
            startPos = Vector3.zero;
            endPos = new Vector3(0, -1080, 0);
        }
        ItemPanel.GetComponent<RectTransform>().localPosition = startPos;

        for(int i = 0; i <= AnimFrame; i++) {
            float time = (float)i / AnimFrame;
            Vector3 pos = Vector3.Lerp(startPos, endPos, time);
            ItemPanel.GetComponent<RectTransform>().localPosition = pos;

            yield return null;
        }

    }

    /// <summary>
    /// 詳細パネルの表示、非表示
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    IEnumerator SetDetailUI(bool act) {
        //アニメーション用にコルーチン
        IsActDetail = act;
        if (act) {
            ItemData.ItemType types = (ItemData.ItemType)Enum.ToObject(typeof(ItemData.ItemType), selectTab);
            
            switch (types) {
                case ItemData.ItemType.Nomal:
                    DetailPanel.GetComponent<DetailPanel>().SetItem(NowHave[selectNum]);
                    break;
                case ItemData.ItemType.SpriteReport:
                    DetailPanel.GetComponent<DetailPanel>().SetReport(NowHave[selectNum]);
                    break;
                case ItemData.ItemType.Pants:
                    ItemManager.Instance.ChangePants(NowHave[selectNum].GetItemNum);
                    yield break;
            }
        }
        DetailPanel.SetActive(act);
        yield return null;
    }
    
    /// <summary>
    /// アニメーション切り替え
    /// </summary>
    void SetAnim() {
        //アニメーション切り替え
    }


    /// <summary>
    /// タブ切り替え
    /// </summary>
    private void SetTabColor() {
        ImagePanel.sprite = PanelSprites[selectTab];
    }

    /// <summary>
    /// アイテム画像貼り付け
    /// </summary>
    private void SetIamges() {
        NowHave.Clear();

        NowHave = ItemManager.Instance.GetNowData(selectTab);
        
        for (int i = 0; i < items.Length; i++) {
            bool act = i < NowHave.Count;
            items[i].SetActive(act);
            if (act) {
                items[i].GetComponent<ItemUIPiece>().SetImage(NowHave[i]);
            }
        }

        FirstSelectIcon();
    }
}
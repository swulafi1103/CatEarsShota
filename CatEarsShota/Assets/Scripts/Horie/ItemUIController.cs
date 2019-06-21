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
    ItemUIPiece[] items = new ItemUIPiece[6];

    [SerializeField]
    GameObject ItemPanel;

    [SerializeField]
    GameObject DetailPanel;

    [SerializeField]
    GameObject SelectImage;

    [SerializeField]
    Animator PlayerAnim;

    //[SerializeField]
    //TabBar tabBer;

    float AnimFrame = 8;

    List<ItemData> NowHave = new List<ItemData>();
    

    bool IsFran = false;

    bool IsActUI = false;

    bool IsActDetail = false;

    bool IsActTab = false;

    void Start() {
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.D)) {
            if (IsActUI) {
                PushGangeTab();
            }
            else {
                IsActDetail = false;
                StartCoroutine(SetItemUI(true));
            }
        }

        if (!IsActUI) return;
        PushCancel();

        //PushUpArrow();

        //PushGangeTab();

        PushShowDetail();

        PushMoveCursor();

    }

    /// <summary>
    /// タブ切り替え入力F
    /// </summary>
    void PushGangeTab() {
        //if (!Input.GetKeyDown(KeyCode.F)) return;

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
        FirstSelectIcon();
    }

    /// <summary>
    /// ↑キーでタブ移動へ
    /// </summary>
    void PushUpArrow() {
        if (!Input.GetKeyDown(KeyCode.UpArrow)) return;
        if (IsActDetail && IsActTab) return;

        IsActTab = true;

        items[selectNum].ThisSelected(false);
        //tabBer.ActAnim(true);
        //tabBer.MoveLine(selectTab);
    }

    /// <summary>
    /// 詳細ウィンドウ切り替え
    /// </summary>
    void PushShowDetail() {
        if (!Input.GetKeyDown(KeyCode.A)) return;


        if (NowHave.Count <= 0) return;
        StartCoroutine(SetDetailUI(true));

        //if (IsActTab) {
        //    IsActTab = false;
        //    //tabBer.ActAnim(false);

        //    SetTabColor();
        //    SetIamges();
        //    FirstSelectIcon();
        //}
        //else {
        //    if (NowHave.Count <= 0) return;
        //    StartCoroutine(SetDetailUI(true));
        //}
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
            //if (IsActTab) {
            //    MoveTabLine(true);
            //}
            //else {
            //    MoveSelectIcon(true);
            //}
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            MoveSelectIcon(false);
            //if (IsActTab) {
            //    MoveTabLine(false);
            //}
            //else {
            //    MoveSelectIcon(false);
            //}
        }
    }

    /// <summary>
    /// タブカーソル移動
    /// </summary>
    void MoveTabLine(bool right) {

        int moves = right ? 1 : -1;

        selectTab += moves;

        if (IsFran) {
            if (selectTab >= selectMaxs[0]) selectTab = 0;
        }
        else {
            if (selectTab >= selectMaxs[1]) selectTab = 0;
        }

        if (selectTab < 0) {
            selectTab = IsFran ? selectMaxs[0] - 1 : selectMaxs[1] - 1;
        }

        //tabBer.MoveLine(selectTab);
    }

    /// <summary>
    /// 持っているアイテムのみカーソルが動く
    /// </summary>
    /// <param name="right"></param>
    void MoveSelectIcon(bool right) {

        if (NowHave.Count <= 0) return;

        int moves = right ? 1 : -1;
        int beforenum = selectNum;
        selectNum += moves;
        if (selectNum >= NowHave.Count) selectNum = 0;
        if (selectNum >= items.Length) selectNum = 0;
        if (selectNum < 0 && items.Length > NowHave.Count) selectNum = NowHave.Count - 1;
        if (selectNum < 0 && items.Length <= NowHave.Count) selectNum = items.Length - 1;

        items[beforenum].ThisSelected(false);
        items[selectNum].ThisSelected(true);

        //Vector3 pos = items[selectNum].GetComponent<RectTransform>().localPosition;
        //SelectImage.GetComponent<RectTransform>().localPosition = pos;
    }

    /// <summary>
    /// カーソル初期設定
    /// </summary>
    void FirstSelectIcon() {
        //if (NowHave.Count == 0) {
        //    SelectImage.gameObject.SetActive(false);
        //}
        //else {
        //    SelectImage.gameObject.SetActive(true);
        //    selectNum = 0;


        //    //Vector3 pos = items[selectNum].GetComponent<RectTransform>().localPosition;
        //    //.GetComponent<RectTransform>().localPosition = pos;
        //}

        if (NowHave.Count <= 0) return;
        selectNum = 0;
        items[selectNum].ThisSelected(true);
    }

    /// <summary>
    /// アイテム欄の表示、非表示
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    IEnumerator SetItemUI(bool act) {

        IsActUI = act;
        Vector3 startPos;
        Vector3 endPos;

        if (act) {
            IsFran = ItemManager.Instance.IsFran;

            SetAnim();
            selectTab = 0;
            SetTabColor();
            //IsActTab = true;
            //tabBer.ActAnim(true);
            //tabBer.MoveLine(selectTab);
            SetIamges();
            FirstSelectIcon();

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
                    DetailPanel.GetComponent<DetailPanel>().SetDetail(NowHave[selectNum]);
                    break;
                case ItemData.ItemType.SpriteReport:
                    DetailPanel.GetComponent<DetailPanel>().SetDetail(NowHave[selectNum]);
                    break;
                case ItemData.ItemType.Pants:
                    IsActDetail = !act;
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
            items[i].gameObject.SetActive(act);
            if (act) {
                items[i].SetImage(NowHave[i]);
            }
        }

    }
}
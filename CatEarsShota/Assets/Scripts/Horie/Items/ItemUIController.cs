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
    Image PlayerPanel;
    [SerializeField]
    Sprite[] Playerimage = new Sprite[2];
    

    float AnimFrame = 8;

    List<ItemData> NowHave = new List<ItemData>();
    

    bool IsFran = false;

    bool IsActUI = false;

    bool IsActDetail = false;

    bool IsActTab = false;

    bool IsEvent = false;

    void Start() {
    }

    void Update() {
        if (FlagManager.Instance.IsEventing) return;

        if (IsEvent) {
            PushCancel();
            return;
        }
        

        if (!IsActUI) return;
        PushGangeTab();
        PushCancel();
        

        PushShowDetail();

        PushMoveCursor();

    }

    public void StartItemUI() {
        if (IsActUI) return;
        FlagManager.Instance.IsOpenUI = true;
        IsActDetail = false;
        StartCoroutine(SetItemUI(true));
    }

    /// <summary>
    /// タブ切り替え入力D
    /// </summary>
    void PushGangeTab() {
        if (!Input.GetKeyDown(KeyCode.D)) return;

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
        FirstSelectIcon(0);
    }
    

    /// <summary>
    /// 詳細ウィンドウ切り替え
    /// </summary>
    void PushShowDetail() {
        if (!Input.GetKeyDown(KeyCode.A)) return;


        if (NowHave.Count <= 0) return;
        StartCoroutine(SetDetailUI(true));
        
    }

    /// <summary>
    /// キャンセルキー
    /// </summary>
    void PushCancel() {
        if (!Input.GetKeyDown(KeyCode.X)) return;
        Debug.Log(IsEvent);

        if (IsActDetail) {
            StartCoroutine(SetDetailUI(false));
        }
        else {
            StartCoroutine(SetItemUI(false));
            if (IsEvent) IsEvent = false;
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
        
    }

    /// <summary>
    /// カーソル初期設定
    /// </summary>
    void FirstSelectIcon(int num) {

        if (NowHave.Count <= 0) return;
        selectNum = num;
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
            IsFran = FlagManager.Instance.IsPast;

            SetAnim();
            selectTab = 0;
            SetTabColor();
            SetIamges();
            FirstSelectIcon(0);

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

        if (IsFran) {
            PlayerPanel.sprite = Playerimage[1];
        }
        else {
            PlayerPanel.sprite = Playerimage[0];
        }

        PlayerPanel.SetNativeSize();
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

    /// <summary>
    /// イベント用アイテムUI_Start
    /// </summary>
    public void SetEventUI(ItemData item)
    {
        IsActUI = false;
        StartCoroutine(StartEventUI(item));
    }

    IEnumerator StartEventUI(ItemData item)
    {
        yield return new WaitForSeconds(0.1f);
        IsEvent = true;
        ItemPanel.GetComponent<RectTransform>().localPosition = Vector3.zero;
        selectTab = (int)item.GetItemType;
        SetTabColor();
        SetIamges();
        for (int i = 0; i < NowHave.Count; i++) {
            if (NowHave[i] == item) {
                items[i].SetShadow(false);
                FirstSelectIcon(i);
            }
            else {
                items[i].SetShadow(true);
            }
        }
    }
    
    /// <summary>
    /// イベント用アイテムUI_End
    /// </summary>
    public bool EndEventUI(ItemData item) {
        if (!IsEvent) return false;
        if (!Input.GetKeyDown(KeyCode.A)) return false;
        if (NowHave[selectNum] != item) return false;
        IsEvent = false;
        ItemPanel.GetComponent<RectTransform>().localPosition = new Vector3(0, -1080, 0);
        StopAllCoroutines();
        return true;
    }
}
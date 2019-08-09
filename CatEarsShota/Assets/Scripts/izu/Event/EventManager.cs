﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    #region Singleton
    private static EventManager instance;
    public static EventManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogWarning("EventManager is Null");
            }
            return instance;
        }
    }

    private bool CheckInstance()
    {
        if (instance == null)
        {
            instance = (EventManager)this;
            return true;
        }
        else if (Instance == this)
        {
            return true;
        }

        Destroy(this);
        return false;
    }
    #endregion

    private List<GameObject> gimmickList = new List<GameObject>();
    private List<GameObject> itemList = new List<GameObject>();

    void Awake()
    {
        CheckInstance();
        SetGimmickItemObject();
    }

    void Start()
    {
        
    }

    void Update()
    {
        //
        if (Input.GetKeyDown(KeyCode.F5))
        {
            UpdateEvent();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            PlayEvent(EventName.E01_Map1_to_map2);
        }        
    }

    /// <summary>フラグのチェックと更新</summary>
    public void UpdateEvent()
    {
        foreach(GameObject obj in gimmickList)
        {
            if (obj != null)
            {
                if (obj.GetComponent<EventBase>() != null)
                {
                    obj.GetComponent<EventBase>().CheckFlag();
                }
            }
        }
        foreach (GameObject obj in itemList)
        {
            if (obj != null)
            {
                if (obj.GetComponent<EventBase>() != null)
                {
                    obj.GetComponent<EventBase>().CheckFlag();
                }
            }
        }
    }

    void SetGimmickItemObject()
    {
        GameObject[] GimmickObjects = GameObject.FindGameObjectsWithTag("Gimmick");
        foreach (var obj in GimmickObjects)
        {
            if (!gimmickList.Contains(obj))
            {
                gimmickList.Add(obj);
            }
        }
        GameObject[] ItemObjects = GameObject.FindGameObjectsWithTag("Item");
        foreach (var obj in ItemObjects)
        {
            if (!itemList.Contains(obj))
            {
                itemList.Add(obj);
            }
        }
    }

    private IEnumerator CallCorutine(EventCategory eventCategory, int value, float delayTime, bool waitMovie, float volume, GameObject target = null)
    {
        switch (eventCategory)
        {
            case EventCategory.Movie:
                return PlayMovie(value, delayTime, waitMovie);
            case EventCategory.FadeIn:
                return FadeIn(value, delayTime, waitMovie);
            case EventCategory.FadeOut:
                return FadeOut(value, delayTime, waitMovie);
            case EventCategory.CameraZoom:
                return CameraZoom(value, delayTime, waitMovie);
            case EventCategory.Bubble:
                return Bubble(value, delayTime, waitMovie);
            case EventCategory.ChangeTime:
                return ChangeTime(value, delayTime, waitMovie);
            case EventCategory.Warp:
                return WarpPositionFran(value, delayTime, waitMovie);
            case EventCategory.DropItem:
                return DropItem(value, delayTime, waitMovie);
            case EventCategory.PickupItem:
                return PickupItem(value, delayTime, waitMovie, target);
            case EventCategory.Tutorial:
                return Tutorial(value, delayTime, waitMovie);
            case EventCategory.Minigame1:
                return Minigame1(value, delayTime, waitMovie);
            case EventCategory.Minigame2:
                return Minigame2(value, delayTime, waitMovie, target);
            case EventCategory.ChangeColor:
                return ChangeColor(value, delayTime, waitMovie);
            case EventCategory.TextWindow:
                return TextWindow(value, delayTime, waitMovie);
            case EventCategory.BGM:
                return PlayBGM(value, delayTime, waitMovie);
            case EventCategory.SE:
                return PlaySE(value, delayTime, waitMovie, volume);
            default:
                Debug.LogWarning("EventCategoryError");
                break;
        }
        Debug.LogWarning("EventCategoryError");
        return null;
    }


    public EventExcel eventExcel;
    public GameObject[] zoomObjects = new GameObject[3];

    public void PlayEvent(EventName eventName, GameObject target = null)
    {
        StartCoroutine(EventCorutine(eventName, target));
    }

    IEnumerator EventCorutine(EventName eventName ,GameObject target)
    {
        List<EventEntity> entity = null;
        int count = 0;

        switch (eventName)
        {
            case EventName.E01_Map1_to_map2:
                entity = eventExcel.E01_Map1_to_map2;
                count = entity.Count;
                break;
            case EventName.E02_Map2_to_map1:
                entity = eventExcel.E02_Map2_to_map1;
                count = entity.Count;
                break;
            case EventName.E03_Pant_pickup3:
                entity = eventExcel.E03_Pant_pickup3;
                count = entity.Count;
                break;
            default:
                Debug.Log("実装してない値");
                break;
        }
        Debug.Log("イベントのコマンドの数 : " + count);
        yield return null;
        for (int i = 0; i < count; i++)
        {
            FlagManager.Instance.IsEventing = true;
            yield return StartCoroutine(CallCorutine(entity[i].category, entity[i].value, entity[i].delayTime, entity[i].waitMovie, entity[i].volume, target));
        }
        FlagManager.Instance.IsEventing = false;
        Debug.Log("イベント終了");
        yield break;
    }


    #region イベントコルーチン

    IEnumerator PlayMovie(int value, float delayTime, bool waitMovie)
    {
        yield return new WaitForSeconds(delayTime);
        //  フェード中か
        yield return new WaitWhile(() => Fade.Instance.Fading == false);
        Debug.Log("Movie");
        MainCamera.Instance.TriggeredVideo((uint)value);
        yield break;
    }
    IEnumerator FadeIn(int value, float delayTime, bool waitMovie)
    {
        if (waitMovie)
        {
            yield return new WaitWhile(() => !FlagManager.Instance.IsMovie);
        }
        yield return new WaitForSeconds(delayTime);
        //  フェード中か
        yield return new WaitWhile(() => Fade.Instance.Fading == false);
        switch (value)
        {
            case 0:
                Fade.Instance.StartFade(0.5f, Color.black);
                break;
            case 1:
                Fade.Instance.StartFade(0.5f, Color.white);
                break;
            default:
                Debug.LogWarning("FadeInError, Value is Over");
                break;
        }        
        yield break;
    }
    IEnumerator FadeOut(int value, float delayTime, bool waitMovie)
    {
        if (waitMovie)
        {
            yield return new WaitWhile(() => !FlagManager.Instance.IsMovie);
        }
        yield return new WaitForSeconds(delayTime);
        Debug.Log("FadeOut");
        Fade.Instance.ClearFade(0.5f, Color.clear);
        yield break;
    }
    IEnumerator CameraZoom(int value, float delayTime, bool waitMovie)
    {
        if (waitMovie)
        {
            yield return new WaitWhile(() => !FlagManager.Instance.IsMovie);
        }
        yield return new WaitForSeconds(delayTime);
        Debug.Log("CameraZoom");
        switch (value)
        {
            case 0:
                Debug.Log("Map1の出入り口をズーム");
                MainCamera.Instance.T_ChangeFocus(zoomObjects[0]);
                break;
            case 1:
                Debug.Log("水槽をズーム(過去)");
                MainCamera.Instance.T_ChangeFocus(zoomObjects[1]);
                break;
            case 2:
                Debug.Log("研究所出口をズーム(過去)");
                MainCamera.Instance.T_ChangeFocus(zoomObjects[2]);
                break;
            default:
                Debug.Log("ズームの未実装のValue");
                break;
        }
        yield break;
    }
    IEnumerator Bubble(int value, float delayTime, bool waitMovie)
    {
        if (waitMovie)
        {
            yield return new WaitWhile(() => !FlagManager.Instance.IsMovie);
        }
        yield return new WaitForSeconds(delayTime);
        Debug.Log("BubbleValue:" + value);
        switch (value)
        {
            case 0:
                BubbleEvent.Instance.DisplayBubbles(BubbleEvent.BubbleType.Door);
                break;
            case 1:
                Debug.Log("BUbbleeeeeee");
                BubbleEvent.Instance.DisplayBubbles(BubbleEvent.BubbleType.Repair);
                break;
            case 2:
                BubbleEvent.Instance.DisplayBubbles(BubbleEvent.BubbleType.Tana);
                break;
            case 3:
                BubbleEvent.Instance.DisplayBubbles(BubbleEvent.BubbleType.Dark);
                break;
            case 4:
                BubbleEvent.Instance.DisplayBubbles(BubbleEvent.BubbleType.Escape);
                break;
            case 5:
                BubbleEvent.Instance.DisplayBubbles(BubbleEvent.BubbleType.Power);
                break;
            default:
                Debug.Log("この吹き出しは未実装");
                break;
        }
        yield break;
    }
    IEnumerator ChangeTime(int value, float delayTime, bool waitMovie)
    {        
        if (waitMovie)
        {
            yield return new WaitWhile(() => !FlagManager.Instance.IsMovie);
        }
        Debug.Log("ChangeTime");
        yield return new WaitForSeconds(delayTime);
        if (value == 0)
            FlagManager.Instance.ChegeFranPero(true);
        else
            FlagManager.Instance.ChegeFranPero(false);
        yield break;
    }
    IEnumerator WarpPositionPero(int value, float delayTime, bool waitMovie)    // セーブの処理にも座標情報があるのでそこを流用予定
    {
        if (waitMovie)
        {
            yield return new WaitWhile(() => !FlagManager.Instance.IsMovie);
        }
        yield return new WaitForSeconds(delayTime);
        Debug.Log("WarpPero");
        switch (value)
        {
            case 0:
                Debug.Log("0番目にワープ(セーブの座標統合予定)");
                break;
            case 1:
                Debug.Log("0番目にワープ(セーブの座標統合予定)");
                break;
            case 2:
                Debug.Log("0番目にワープ(セーブの座標統合予定)");
                break;
            case 3:
                Debug.Log("0番目にワープ(セーブの座標統合予定)");
                break;
            default:
                Debug.Log("まだこの座標は未実装");
                break;
        }
        yield break;
    }
    IEnumerator WarpPositionFran(int value, float delayTime, bool waitMovie)
    {
        if (waitMovie)
        {
            yield return new WaitWhile(() => !FlagManager.Instance.IsMovie);
        }
        yield return new WaitForSeconds(delayTime);
        Debug.Log("WarpFran");
        switch (value)
        {
            case 0:
                Debug.Log("0番目にワープ(セーブの座標統合予定)");
                break;
            case 1:
                Debug.Log("0番目にワープ(セーブの座標統合予定)");
                break;
            case 2:
                Debug.Log("0番目にワープ(セーブの座標統合予定)");
                break;
            case 3:
                Debug.Log("0番目にワープ(セーブの座標統合予定)");
                break;
            default:
                Debug.Log("まだこの座標は未実装");
                break;
        }
        yield break;
    }
    IEnumerator DropItem(int value, float delayTime, bool waitMovie)
    {
        if (waitMovie)
        {
            yield return new WaitWhile(() => !FlagManager.Instance.IsMovie);
        }
        yield return new WaitForSeconds(delayTime);
        Debug.Log("DropItem");
        yield break;
    }
    IEnumerator PickupItem(int value, float delayTime, bool waitMovie, GameObject target)
    {
        if (waitMovie)
        {
            yield return new WaitWhile(() => !FlagManager.Instance.IsMovie);
        }
        yield return new WaitForSeconds(delayTime);
        Debug.Log("PickUpItem");
        //  非表示
        target.SetActive(false);
        switch (value)
        {
            case 0:
                ItemManager.Instance.SetItemData(ItemManager.ItemNum.Yerrow_Orb);
                FlagManager.Instance.SetItemFlag(ItemFlag.I_01_YellowOrb);
                break;
            case 1:
                ItemManager.Instance.SetItemData(ItemManager.ItemNum.Blue_Orb);
                FlagManager.Instance.SetItemFlag(ItemFlag.I_02_BlueOrb);
                break;
            case 2:
                ItemManager.Instance.SetItemData(ItemManager.ItemNum.Green_Orb);
                FlagManager.Instance.SetItemFlag(ItemFlag.I_03_GreenOrb);
                break;
            case 3:
                ItemManager.Instance.SetItemData(ItemManager.ItemNum.Red_Orb);
                FlagManager.Instance.SetItemFlag(ItemFlag.I_04_RedOrb);
                break;
            case 4:
                ItemManager.Instance.SetItemData(ItemManager.ItemNum.CardKey);
                FlagManager.Instance.SetItemFlag(ItemFlag.I_05_CardKey);
                break;
            case 5:
                ItemManager.Instance.SetItemData(ItemManager.ItemNum.Ilust_Piece);
                FlagManager.Instance.SetItemFlag(ItemFlag.I_06_WallPaintingPiece);
                break;
            case 6:
                ItemManager.Instance.SetItemData(ItemManager.ItemNum.BookMark_Past);
                FlagManager.Instance.SetItemFlag(ItemFlag.I_07_BookStop);
                break;
            //----------------------------------------------------------------
            case 7:
                ItemManager.Instance.SetItemData(ItemManager.ItemNum.Yerrow_Orb);
                FlagManager.Instance.SetItemFlag(ItemFlag.I_01_YellowOrb);
                break;
            case 8:
                ItemManager.Instance.SetItemData(ItemManager.ItemNum.Yerrow_Orb);
                FlagManager.Instance.SetItemFlag(ItemFlag.I_01_YellowOrb);
                break;
            case 9:
                ItemManager.Instance.SetItemData(ItemManager.ItemNum.Yerrow_Orb);
                FlagManager.Instance.SetItemFlag(ItemFlag.I_01_YellowOrb);
                break;
            case 10:
                ItemManager.Instance.SetItemData(ItemManager.ItemNum.Yerrow_Orb);
                FlagManager.Instance.SetItemFlag(ItemFlag.I_01_YellowOrb);
                break;
            case 11:
                ItemManager.Instance.SetItemData(ItemManager.ItemNum.Yerrow_Orb);
                FlagManager.Instance.SetItemFlag(ItemFlag.I_01_YellowOrb);
                break;
            case 12:
                ItemManager.Instance.SetItemData(ItemManager.ItemNum.Yerrow_Orb);
                FlagManager.Instance.SetItemFlag(ItemFlag.I_01_YellowOrb);
                break;
            case 13:
                ItemManager.Instance.SetItemData(ItemManager.ItemNum.Yerrow_Orb);
                FlagManager.Instance.SetItemFlag(ItemFlag.I_01_YellowOrb);
                break;
            case 14:
                ItemManager.Instance.SetItemData(ItemManager.ItemNum.Yerrow_Orb);
                FlagManager.Instance.SetItemFlag(ItemFlag.I_01_YellowOrb);
                break;
            case 15:
                ItemManager.Instance.SetItemData(ItemManager.ItemNum.Yerrow_Orb);
                FlagManager.Instance.SetItemFlag(ItemFlag.I_01_YellowOrb);
                break;
            case 16:
                ItemManager.Instance.SetItemData(ItemManager.ItemNum.Yerrow_Orb);
                FlagManager.Instance.SetItemFlag(ItemFlag.I_01_YellowOrb);
                break;
            case 17:
                ItemManager.Instance.SetItemData(ItemManager.ItemNum.Yerrow_Orb);
                FlagManager.Instance.SetItemFlag(ItemFlag.I_01_YellowOrb);
                break;
            default:
                Debug.LogWarning("アイテム取得で未実装の番号が選択されました。番号：" + value);
                break;
        }
        yield break;
    }
    IEnumerator Tutorial(int value, float delayTime, bool waitMovie)
    {
        if (waitMovie)
        {
            yield return new WaitWhile(() => !FlagManager.Instance.IsMovie);
        }
        yield return new WaitForSeconds(delayTime);
        Debug.Log("Tutorial");
        switch (value)
        {
            case 0:
                //  過去・現在切り替えのチュートリアル
                TutorialContriller.Instance.ChangeModeTuto();
                yield return new WaitWhile(() => FlagManager.Instance.CheckGimmickFlag(GimmickFlag.G_05_Tuto_TimeChenge) == true);
                break;
            case 1:
                Debug.Log("未実装：ミニゲーム１の２番");
                break;
            default:
                Debug.Log("未実装の番号です。番号：" + value);
                break;
        }
        yield break;
    }
    IEnumerator Minigame1(int value, float delayTime, bool waitMovie)
    {
        if (waitMovie)
        {
            yield return new WaitWhile(() => !FlagManager.Instance.IsMovie);
        }
        yield return new WaitForSeconds(delayTime);
        Debug.Log("Minigame1");
        //  ミニゲーム１表示
        MiniGameManager.Instance.TouchGenerator(value);
        yield break;
    }
    IEnumerator Minigame2(int value, float delayTime, bool waitMovie, GameObject target)
    {        
        if (waitMovie)
        {
            yield return new WaitWhile(() => !FlagManager.Instance.IsMovie);
        }
        yield return new WaitForSeconds(delayTime);
        Debug.Log("Minigame2");
        //target.GetComponent<PanelPuzzleControl>().s 
        //  ミニゲーム２表示
        
        yield break;
    }
    IEnumerator ChangeColor(int value, float delayTime, bool waitMovie)
    {
        if (waitMovie)
        {
            yield return new WaitWhile(() => !FlagManager.Instance.IsMovie);
        }
        yield return new WaitForSeconds(delayTime);
        Debug.Log("ChngeColor");
        //  mapの色変え
        switch (value)
        {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
            default:
                Debug.Log("マップの色替え未完成");
                break;
        }
        yield break;
    }
    IEnumerator TextWindow(int value, float delayTime, bool waitMovie)
    {
        if (waitMovie)
        {
            yield return new WaitWhile(() => !FlagManager.Instance.IsMovie);
        }
        yield return new WaitForSeconds(delayTime);
        Debug.Log("TextWindow");
        switch (value)
        {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
            default:
                Debug.Log("テキストウィンドウの表示未実装");
                break;
        }
        yield break;
    }
    IEnumerator PlayBGM(int value, float delayTime, bool waitMovie)
    {
        if (waitMovie)
        {
            yield return new WaitWhile(() => !FlagManager.Instance.IsMovie);
        }
        yield return new WaitForSeconds(delayTime);
        Debug.Log("BGM");
        switch (value)
        {
            case 0:
                SoundManager.Instance.PlayBGM(SoundManager.BGM_Name.BGM_00_Opening);
                break;
            case 1:
                SoundManager.Instance.PlayBGM(SoundManager.BGM_Name.BGM_01_Gray);
                break;
            case 2:
                SoundManager.Instance.PlayBGM(SoundManager.BGM_Name.BGM_02_Yellow);
                break;
            case 3:
                SoundManager.Instance.PlayBGM(SoundManager.BGM_Name.BGM_03_Red);
                break;
            case 4:
                SoundManager.Instance.PlayBGM(SoundManager.BGM_Name.BGM_04_Blue);
                break;
            case 5:
                SoundManager.Instance.PlayBGM(SoundManager.BGM_Name.BGM_05_Green);
                break;
            case 6:
                SoundManager.Instance.PlayBGM(SoundManager.BGM_Name.BGM_06_Past1);
                break;
            case 7:
                SoundManager.Instance.PlayBGM(SoundManager.BGM_Name.BGM_07_Past2);
                break;
            default:
                Debug.LogWarning(value +"番のBGMは未実装");
                break;
        }
        yield break;
    }
    IEnumerator PlaySE(int value, float delayTime, bool waitMovie, float vol)
    {
        if (waitMovie)
        {
            yield return new WaitWhile(() => !FlagManager.Instance.IsMovie);
        }
        yield return new WaitForSeconds(delayTime);
        Debug.Log("SE");
        switch (value)
        {
            case 0:
                SoundManager.Instance.PlaySE(SoundManager.SE_Name.SE_00_Alerm);
                break;
            case 1:
                SoundManager.Instance.PlaySE(SoundManager.SE_Name.SE_01_BreakWin);
                break;
            case 2:
                SoundManager.Instance.PlaySE(SoundManager.SE_Name.SE_02_Wator);
                break;
            default:
                Debug.LogWarning(value + "番のSEは未実装");
                break;
        }
        yield break;
    }
    #endregion
}

public enum EventName
{
    E01_Map1_to_map2,                   //（マップ1出口接触）フェードイン→プレイヤー座標変更→フェードアウト
    E02_Map2_to_map1,                   //（マップ2入口接触）フェードイン→プレイヤー座標変更→フェードアウト
    E03_Pant_pickup3,                   //（パンツ3に触れる）パンツ3拾う
    E04_MiniGame2_clear,                //（壁画Aボタン）ミニゲーム2入る
    E05_Map1_to_map2_past,              //（マップ1出口接触）フェードイン→プレイヤー座標変更→フェードアウト
    E06_Map2_to_map1_past,              //（マップ2入口接触）フェードイン→プレイヤー座標変更→フェードアウト
    E07_Report2_pickup_past,            //フランの研究日誌をAで拾う→フェードイン→アイテムの表示
    E08_Piece_pickup_past,              //壁画のピースを拾う→アイテム欄に入る
    E09_Gate1geme_past,                 //調べると（A）フェードイン→ミニゲーム１の出現
    E10_Gate1geme_clear_past,           //ゲームクリアフェードアウト→扉が開く
    E11_Gate1geme_error_past,           //ゲーム失敗フェードアウト
    E12_Stairs1_up_past,                //階段を調べる→フェードイン→フランの座標位置変更→フェードアウト
    E13_Stairs1_down_past,              //階段を調べる→フェードイン→フランの座標位置変更→フェードアウト
    E14_Enemy_exit_past,                //出口にＡボタン→フェードイン→ムービー開始→フランの位置変更→フェードアウト
    E15_Timecapsule_piece_buried_past,  //埋め場にＡボタン→チュートリアル表示→壁画のピースを使用→ SE→埋め場画像差し替え→UI「キノコのもとを手に入れました」を表示→右側の腐木にビックリ吹き出し表示
    E16_Mushroom_plant1_past,           //チュートリアルの表示→ビックリマークアウト→インベントリの表示→キノコのもとを使用→UI「腐木にキノコのもとを設置」
    E17_Mushroom_noplant_past,          //（キノコを設置せずにはしご降りようとする場合）はしごにＡボタン→キノコのもとの吹き出し
    E18_Cardkey_needed,                 //（カードキーなしで出口にAボタン）吹き出し
    E19_Pant_pickup4,                   //（パンツ4に触れる）パンツ4拾う
    E20_Timecapsule_piece,              //（埋め場Aボタン）フェードイン→SE→画像差し替え→フェードアウト→ピース入手→吹き出しウィンドウ
    E21_Pant_pickup5,                   //（パンツ5に触れる）パンツ5拾う
    E22_Picture_book_piece_pickup,      //（植物図鑑のページにAボタン）レポート拾う→フェードイン→レポート表示
    E23_MiniGame2_clear,                //（ミニゲーム2クリア）フェードイン→ムービー→BGM→変色ムービー→赤オーブドロップ→ビックリマーク吹き出し
    E24_Alone_start_past,               //階段を調べる→フェードイン→ムービー開始→→フランの位置変更→フランの立ち絵（ペローなし）差し替え→アイテム「しおり」入手→UI「しおりの入手した」→マップ1と2の行き来を止める（プログラマー？）→フェードアウト→UI「しおりを手に入れました」を表示
    E25_Gate2game_past,                 //調べると（A）フェードイン→ミニゲーム１の出現
    E26_Gate2geme_clear_past,           //ゲームクリアフェードアウト→扉が開く
    E27_Gate2geme_error_past,           //ゲーム失敗フェードアウト
    E28_Stairs2_up_past,                //階段を調べる→フェードイン→フランの座標位置変更→フェードアウト
    E29_Stairs2_down_past,              //階段を調べる→フェードイン→フランの座標位置変更→フェードアウト
    E30_Stairs3_up_past,                //階段を調べる→フェードイン→フランの座標位置変更→フェードアウト
    E31_Stairs3_down_past,              //階段を調べる→フェードイン→フランの座標位置変更→フェードアウト
    E32_Apparatus_on_past,              //調べるとヒントの吹き出しの表示→装置起動→埋め場にビックリマークの吹き出しの表示
    E33_report3_pickup,                 //エネルギー循環装置の説明書を拾う→フェードイン→アイテム表示
    E34_Timecapsule_bookmark_buried_past,   //調べるとイベントリの表示→しおり使用→ SE→埋め場画像差し替え→UI「キノコのもとを手に入れました」を表示→左側の腐木の上にビックリマークの吹き出しの表示
    E35_Mushroom_plant2_past,           //左側の腐木を調べてインベントリの表示→キノコのもとを使用→UI「キノコのもとを設置した」→フェードイン→ムービー→背景（壊れた階段ver）差し替え→敵出現→装置にビックリマークの吹き出しの表示→フェードアウト
    E36_End_past,                       //調べるとフェードイン→ムービー→過去モード終了
    E37_Pant_pickup,                    //（パンツ6に触れる）パンツ6拾う
    E38_No_answer,                      //（過去現在切り替えが止められている場合）Fボタン→吹き出しウィンドウ
    E39_Blue_event,                     //（水槽にAボタン）フェードイン→回想ムービー→BGM→黒化ムービー→（キャラアニメーション差し替え）→変色ムービー→青オーブドロップ→ビックリマーク吹き出し→キーカード入手→カードキー入手吹き出しウィンドウ
    E40_Orb_fillin,                     //（オーブパネルにAボタン）オーブパネル中央表示
    E41_Bad_end,                        //（カードキー所持で出口Aボタン）インベントリ画面出現→カードキー使う→ビックリ吹き出し消える→フェードイン→ムービー
    E42_Time_capsule_bookmark,          //（埋め場Aボタン）フェードイン→SE→画像差し替え→フェイドアウト→しおり入手→しおり入手吹き出しウィンドウ
    E43_Green_event,                    //（しおり入手）フェードイン→BGM→ムービー→変色ムービー→緑オーブドロップ→ビックリマーク吹き出し
    E44_Orb_fillin_clear,               //（四つのオーブを嵌めた）暗転→ムービー
}

/// <summary>
/// キャラの調べる反応させる関数
/// </summary>
public interface ICheckable
{
    void Check();
}

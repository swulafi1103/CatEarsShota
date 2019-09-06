using System;
using System.Collections;
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
    private GameObject map2;
    private GameObject map1;

    public System.Action TypeinGameMap1ClearedFunc;
    public System.Action TypeinGameMap2FirstClearedFunc;
    public System.Action TypeinGameMap2LetterClearedFunc;
    public System.Action PieceGameClearedFunc;

    void Awake()
    {
        CheckInstance();
        SetGimmickItemObject();
    }

    void Start()
    {
        map2 = GameObject.FindGameObjectWithTag("Map2");
        map1 = GameObject.FindGameObjectWithTag("Map1");
    }

    void Update()
    {
        //
        if (Input.GetKeyDown(KeyCode.F5))
        {
            //  Debug用
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
        foreach (GameObject obj in gimmickList)
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
            case EventCategory.ChangeSprite:
                return ChangeSprite(value, delayTime, waitMovie);
            case EventCategory.StandFlag:
                return StandFlag(delayTime, waitMovie, target);
            case EventCategory.SetPiece:
                return SetPiece(value, delayTime, waitMovie);
            case EventCategory.SetMushRoomNoMoto:
                return SetMushRoomNoMoto(value, delayTime, waitMovie);
            case EventCategory.SetOrb:
                return SetOrb(value, delayTime, waitMovie);
            case EventCategory.GlowMushroom:
                return GlowMushroom(value, delayTime, waitMovie, target);
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

    List<EventEntity> GetEventEntity(EventName eventName)
    {
        List<EventEntity> entity = null;
        switch (eventName)
        {
            case EventName.E01_Map1_to_map2:
                entity = eventExcel.E01_Map1_to_map2;
                break;
            case EventName.E02_Map2_to_map1:
                entity = eventExcel.E02_Map2_to_map1;
                break;
            case EventName.E03_Pant_pickup3:
                entity = eventExcel.E03_Pant_pickup3;
                break;
            case EventName.E04_MiniGame2_clear:
                entity = eventExcel.E04_MiniGame2_clear;
                break;
            case EventName.E05_Map1_to_map2_past:
                entity = eventExcel.E05_Map1_to_map2_past;
                break;
            case EventName.E06_Map2_to_map1_past:
                entity = eventExcel.E06_Map2_to_map1_past;
                break;
            case EventName.E07_Report2_pickup_past:
                entity = eventExcel.E07_Report2_pickup_past;
                break;
            case EventName.E08_Piece_pickup_past:
                entity = eventExcel.E08_Piece_pickup_past;
                break;
            case EventName.E09_Gate1geme_past:
                entity = eventExcel.E09_Gate1geme_past;
                break;
            case EventName.E10_Gate1geme_clear_past:
                entity = eventExcel.E10_Gate1geme_clear_past;
                break;
            case EventName.E11_Gate1geme_error_past:
                entity = eventExcel.E11_Gate1geme_error_past;
                break;
            case EventName.E12_Stairs1_up_past:
                entity = eventExcel.E12_Stairs1_up_past;
                break;
            case EventName.E13_Stairs1_down_past:
                entity = eventExcel.E13_Stairs1_down_past;
                break;
            case EventName.E14_Enemy_exit_past:
                entity = eventExcel.E14_Enemy_exit_past;
                break;
            case EventName.E15_Timecapsule_piece_buried_past:
                entity = eventExcel.E15_Timecapsule_piece_buried_pa;
                break;
            case EventName.E16_Mushroom_plant1_past:
                entity = eventExcel.E16_Mushroom_plant1_past;
                break;
            case EventName.E17_Mushroom_noplant_past:
                entity = eventExcel.E17_Mushroom_noplant_past;
                break;
            case EventName.E18_Cardkey_needed:
                entity = eventExcel.E18_Cardkey_needed;
                break;
            case EventName.E19_Pant_pickup4:
                entity = eventExcel.E19_Pant_pickup4;
                break;
            case EventName.E20_Timecapsule_piece:
                entity = eventExcel.E20_Timecapsule_piece;
                break;
            case EventName.E21_Pant_pickup5:
                entity = eventExcel.E21_Pant_pickup5;
                break;
            case EventName.E22_Picture_book_piece_pickup:
                entity = eventExcel.E22_Picture_book_piece_pickup;
                break;
            case EventName.E23_MiniGame2_clear:
                entity = eventExcel.E23_MiniGame2_clear;
                break;
            case EventName.E24_Alone_start_past:
                entity = eventExcel.E24_Alone_start_past;
                break;
            case EventName.E25_Gate2game_past:
                entity = eventExcel.E25_Gate2game_past;
                break;
            case EventName.E26_Gate2geme_clear_past:
                entity = eventExcel.E26_Gate2geme_clear_past;
                break;
            case EventName.E27_Gate2geme_error_past:
                entity = eventExcel.E27_Gate2geme_error_past;
                break;
            case EventName.E28_Stairs2_up_past:
                entity = eventExcel.E28_Stairs2_up_past;
                break;
            case EventName.E29_Stairs2_down_past:
                entity = eventExcel.E29_Stairs2_down_past;
                break;
            case EventName.E30_Stairs3_up_past:
                entity = eventExcel.E30_Stairs3_up_past;
                break;
            case EventName.E31_Stairs3_down_past:
                entity = eventExcel.E31_Stairs3_down_past;
                break;
            case EventName.E32_Apparatus_on_past:
                entity = eventExcel.E32_Apparatus_on_past;
                break;
            case EventName.E33_report3_pickup:
                entity = eventExcel.E33_report3_pickup;
                break;
            case EventName.E34_Timecapsule_bookmark_buried_past:
                entity = eventExcel.E34_Timecapsule_bookmark_buried;
                break;
            case EventName.E35_Mushroom_plant2_past:
                entity = eventExcel.E35_Mushroom_plant2_past;
                break;
            case EventName.E36_End_past:
                entity = eventExcel.E36_End_past;
                break;
            case EventName.E37_Pant_pickup:
                entity = eventExcel.E37_Pant_pickup;
                break;
            case EventName.E38_No_answer:
                entity = eventExcel.E38_No_answer;
                break;
            case EventName.E39_Blue_event:
                entity = eventExcel.E39_Blue_event;
                break;
            case EventName.E40_Orb_fillin:
                entity = eventExcel.E40_Orb_fillin;
                break;
            case EventName.E41_Bad_end:
                entity = eventExcel.E41_Bad_end;
                break;
            case EventName.E42_Time_capsule_bookmark:
                entity = eventExcel.E42_Time_capsule_bookmark;
                break;
            case EventName.E43_Green_event:
                entity = eventExcel.E43_Green_event;
                break;
            case EventName.E44_Orb_fillin_clear:
                entity = eventExcel.E44_Orb_fillin_clear;
                break;
            default:
                Debug.Log("実装してない値");
                break;
        }

        return entity;
    }

    IEnumerator EventCorutine(EventName eventName, GameObject target)
    {
        List<EventEntity> entity = null;
        entity = GetEventEntity(eventName);
        int count = entity.Count;
        Debug.Log("イベント名 ： " + eventName + "イベントのコマンドの数 : " + count);
        yield return null;
        //  イベントの開始
        for (int i = 0; i < count; i++)
        {
            Debug.Log("イベント" + i + "番");
            FlagManager.Instance.IsEventing = true;
            yield return StartCoroutine(CallCorutine(entity[i].category, entity[i].value, entity[i].delayTime, entity[i].waitMovie, entity[i].volume, target));
        }
        //FlagManager.Instance.IsEventing = false;
        Debug.Log("イベント終了");
        yield break;
    }


    #region イベントコルーチン

    IEnumerator PlayMovie(int value, float delayTime, bool waitMovie)
    {
        yield return new WaitForSeconds(delayTime);
        //  フェード中か
        while (!Fade.Instance.Fading == false)
            yield return null;
        Debug.Log("Movie");
        MainCamera.Instance.TriggeredVideo((uint)value);
        FlagManager.Instance.IsEventing = false;
        yield break;
    }
    IEnumerator FadeIn(int value, float delayTime, bool waitMovie)
    {
        if (waitMovie)
        {
            while (FlagManager.Instance.IsMovie == true)
                yield return null;
        }
        yield return new WaitForSeconds(delayTime);
        //  フェード中か
        while (!Fade.Instance.Fading == false)
            yield return null;
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
        FlagManager.Instance.IsEventing = false;
        yield break;
    }
    IEnumerator FadeOut(int value, float delayTime, bool waitMovie)
    {
        if (waitMovie)
        {
            while (FlagManager.Instance.IsMovie == true)
                yield return null;
        }
        yield return new WaitForSeconds(delayTime);
        while (!Fade.Instance.Fading == false)
            yield return null;
        Debug.Log("FadeOut");
        Fade.Instance.ClearFade(0.5f, Color.clear);
        FlagManager.Instance.IsEventing = false;
        yield break;
    }
    IEnumerator CameraZoom(int value, float delayTime, bool waitMovie)
    {
        if (waitMovie)
        {
            while (FlagManager.Instance.IsMovie == true)
                yield return null;
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
        FlagManager.Instance.IsEventing = false;
        yield break;
    }
    IEnumerator Bubble(int value, float delayTime, bool waitMovie)
    {
        if (waitMovie)
        {
            while (FlagManager.Instance.IsMovie == true)
                yield return null;
        }
        yield return new WaitForSeconds(delayTime);
        Debug.Log("BubbleValue:" + value);
        switch (value)
        {
            case 0:
                BubbleEvent.Instance.DisplayBubbles(BubbleEvent.BubbleType.Door);
                break;
            case 1:
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
            case 6:
                BubbleEvent.Instance.DisplayBubbles(BubbleEvent.BubbleType.Skull);
                break;
            case 7:
                BubbleEvent.Instance.DisplayBubbles(BubbleEvent.BubbleType.CardKey);
                break;
            case 8:
                BubbleEvent.Instance.DisplayBubbles(BubbleEvent.BubbleType.BookMark);
                break;
            case 9:
                BubbleEvent.Instance.DisplayBubbles(BubbleEvent.BubbleType.Pero);
                break;
            case 10:
                BubbleEvent.Instance.DisplayBubbles(BubbleEvent.BubbleType.Mashroom);
                break;
            case 11:
                BubbleEvent.Instance.DisplayBubbles(BubbleEvent.BubbleType.Orb);
                break;
            default:
                Debug.Log("この吹き出しは未実装");
                break;
        }
        FlagManager.Instance.IsEventing = false;
        yield break;
    }
    IEnumerator ChangeTime(int value, float delayTime, bool waitMovie)
    {
        if (waitMovie)
        {
            while (FlagManager.Instance.IsMovie == true)
                yield return null;
        }
        Debug.Log("ChangeTime");
        yield return new WaitForSeconds(delayTime);
        if (value == 0)
            FlagManager.Instance.ChegeFranPero(true);
        else
            FlagManager.Instance.ChegeFranPero(false);
        FlagManager.Instance.IsEventing = false;
        yield break;
    }
    //IEnumerator WarpPositionPero(int value, float delayTime, bool waitMovie)    // セーブの処理にも座標情報があるのでそこを流用予定
    //{
    //    if (waitMovie)
    //    {
    //        yield return new WaitWhile(() => !FlagManager.Instance.IsMovie);
    //    }
    //    yield return new WaitForSeconds(delayTime);
    //    Debug.Log("WarpPero");
    //    switch (value)
    //    {
    //        case 0:
    //            Debug.Log("0番目にワープ(セーブの座標統合予定)");
    //            break;
    //        case 1:
    //            Debug.Log("0番目にワープ(セーブの座標統合予定)");
    //            break;
    //        case 2:
    //            Debug.Log("0番目にワープ(セーブの座標統合予定)");
    //            break;
    //        default:
    //            Debug.Log("まだこの座標は未実装");
    //            break;
    //    }
    //    yield break;
    //}
    IEnumerator WarpPositionFran(int value, float delayTime, bool waitMovie)
    {
        if (waitMovie)
        {
            while (FlagManager.Instance.IsMovie == true)
                yield return null;
        }
        yield return new WaitForSeconds(delayTime);
        Debug.Log("WarpFran");
        GetComponent<WarpManager>().WarpFran(value);
        yield break;
    }
    IEnumerator DropItem(int value, float delayTime, bool waitMovie)
    {
        if (waitMovie)
        {
            while (FlagManager.Instance.IsMovie == true)
                yield return null;
        }
        yield return new WaitForSeconds(delayTime);
        Debug.Log("DropItem");
        //  アイテムの表示
        UpdateEvent();
        yield break;
    }
    IEnumerator PickupItem(int value, float delayTime, bool waitMovie, GameObject target)
    {
        if (waitMovie)
        {
            while (FlagManager.Instance.IsMovie == true)
                yield return null;
        }
        yield return new WaitForSeconds(delayTime);
        Debug.Log("PickUpItem");
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
                ItemManager.Instance.SetItemData(ItemManager.ItemNum.Report_88);
                FlagManager.Instance.SetItemFlag(ItemFlag.I_08_ReportNo88);
                break;
            case 5:
                ItemManager.Instance.SetItemData(ItemManager.ItemNum.Diary_Fran);
                FlagManager.Instance.SetItemFlag(ItemFlag.I_09_DiaryFran);
                break;
            case 6:
                ItemManager.Instance.SetItemData(ItemManager.ItemNum.Instructions);
                FlagManager.Instance.SetItemFlag(ItemFlag.I_10_Instructions);
                break;
            case 7:
                ItemManager.Instance.SetItemData(ItemManager.ItemNum.CardKey);
                FlagManager.Instance.SetItemFlag(ItemFlag.I_05_CardKey);
                break;
            case 8:
                ItemManager.Instance.SetItemData(ItemManager.ItemNum.Mushroom);
                FlagManager.Instance.SetItemFlag(ItemFlag.I_19_MushRoom);
                break;
            case 9:
                ItemManager.Instance.SetItemData(ItemManager.ItemNum.Ilust_Piece);
                FlagManager.Instance.SetItemFlag(ItemFlag.I_06_WallPaintingPiece);
                break;
            case 10:
                ItemManager.Instance.SetItemData(ItemManager.ItemNum.Picture_Book);
                FlagManager.Instance.SetItemFlag(ItemFlag.I_11_PlantBookPiece);
                break;
            case 11:
                ItemManager.Instance.SetItemData(ItemManager.ItemNum.BookMark_now);
                FlagManager.Instance.SetItemFlag(ItemFlag.I_07_BookStopNow);
                break;
            case 12:
                ItemManager.Instance.SetItemData(ItemManager.ItemNum.Pants_1);
                FlagManager.Instance.SetItemFlag(ItemFlag.I_12_Pants_A);
                break;
            case 13:
                ItemManager.Instance.SetItemData(ItemManager.ItemNum.Pants_2);
                FlagManager.Instance.SetItemFlag(ItemFlag.I_13_Pants_B);
                break;
            case 14:
                ItemManager.Instance.SetItemData(ItemManager.ItemNum.Pants_3);
                FlagManager.Instance.SetItemFlag(ItemFlag.I_14_Pants_C);
                break;
            case 15:
                ItemManager.Instance.SetItemData(ItemManager.ItemNum.Pants_4);
                FlagManager.Instance.SetItemFlag(ItemFlag.I_15_Pants_D);
                break;
            case 16:
                ItemManager.Instance.SetItemData(ItemManager.ItemNum.Pants_5);
                FlagManager.Instance.SetItemFlag(ItemFlag.I_16_Pants_E);
                break;
            case 17:
                ItemManager.Instance.SetItemData(ItemManager.ItemNum.Pants_6);
                FlagManager.Instance.SetItemFlag(ItemFlag.I_17_Pants_F);
                break;
            case 18:
                ItemManager.Instance.SetItemData(ItemManager.ItemNum.BookMark_Past);
                FlagManager.Instance.SetItemFlag(ItemFlag.I_18_BookStopPast);
                break;
            default:
                Debug.LogWarning("アイテム取得で未実装の番号が選択されました。番号：" + value);
                yield break;
        }
        SoundManager.Instance.PlaySE(SoundManager.SE_Name.SE_06_ItemPickUp);
        //  非表示
        target.SetActive(false);
        FlagManager.Instance.IsEventing = false;
        yield break;
    }
    IEnumerator Tutorial(int value, float delayTime, bool waitMovie)
    {
        if (waitMovie)
        {
            while (FlagManager.Instance.IsMovie == true)
                yield return null;
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
        FlagManager.Instance.IsEventing = false;
        yield break;
    }
    IEnumerator Minigame1(int value, float delayTime, bool waitMovie)
    {
        if (waitMovie)
        {
            while (FlagManager.Instance.IsMovie == true)
                yield return null;
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
            while (FlagManager.Instance.IsMovie == true)
                yield return null;
        }
        yield return new WaitForSeconds(delayTime);
        Debug.Log("Minigame2");
        //  ミニゲーム２表示
        PanelPuzzleControl.Instance.StartPanelPuzzle();
        yield break;
    }
    IEnumerator ChangeColor(int value, float delayTime, bool waitMovie)
    {
        if (waitMovie)
        {
            while (FlagManager.Instance.IsMovie == true)
                yield return null;
        }
        yield return new WaitForSeconds(delayTime);
        Debug.Log("ChngeColor");
        //  mapの色変え
        switch (value)
        {
            case 0:
            case 1://pero black command
            case 2:
            case 3:
                map2.GetComponent<MapStatus>().ChangeColorObj(value);
                break;
            default:
                Debug.Log("マップの色替え未完成");
                break;
        }
        FlagManager.Instance.IsEventing = false;
        yield break;
    }
    IEnumerator TextWindow(int value, float delayTime, bool waitMovie)
    {
        if (waitMovie)
        {
            while (FlagManager.Instance.IsMovie == true)
                yield return null;
        }
        yield return new WaitForSeconds(delayTime);
        Debug.Log("TextWindow");
        TutorialContriller.Instance.SetTextWindow(value);
        yield break;
    }
    IEnumerator PlayBGM(int value, float delayTime, bool waitMovie)
    {
        if (waitMovie)
        {
            while (FlagManager.Instance.IsMovie == true)
                yield return null;
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
                Debug.LogWarning(value + "番のBGMは未実装");
                break;
        }
        yield break;
    }
    IEnumerator PlaySE(int value, float delayTime, bool waitMovie, float vol)
    {
        if (waitMovie)
        {
            while (FlagManager.Instance.IsMovie == true)
                yield return null;
        }
        yield return new WaitForSeconds(delayTime);
        Debug.Log("SE");
        SoundManager.Instance.PlaySE(value, vol);
        yield break;
    }
    IEnumerator ChangeSprite(int value, float delayTime, bool waitMovie)
    {
        if (waitMovie)
        {
            while (FlagManager.Instance.IsMovie == true)
                yield return null;
        }
        yield return new WaitForSeconds(delayTime);
        Debug.Log("ChangeSprite");
        switch (value)
        {
            case 0:
                map1.GetComponent<MapStatus>().UpdateGimmick(0, false);
                break;
            case 1:
                map1.GetComponent<MapStatus>().UpdateGimmick(0, true);
                break;
            case 2:
                map1.GetComponent<MapStatus>().UpdateGimmick(1, false);
                break;
            case 3:
                map1.GetComponent<MapStatus>().UpdateGimmick(1, true);
                break;
            case 4:
                map2.GetComponent<MapStatus>().UpdateGimmick(0, false);
                break;
            case 5:
                map2.GetComponent<MapStatus>().UpdateGimmick(0, true);
                break;
            case 6:
                map2.GetComponent<MapStatus>().UpdateGimmick(1, false);
                break;
            case 7:
                map2.GetComponent<MapStatus>().UpdateGimmick(1, true);
                break;

        }
        yield break;
    }
    IEnumerator StandFlag(float delayTime, bool waitMovie, GameObject target)
    {
        if (waitMovie)
        {
            while (FlagManager.Instance.IsMovie == true)
                yield return null;
        }
        yield return new WaitForSeconds(delayTime);
        Debug.Log("StandFlag");
        FlagManager.Instance.SetGimmickFlag(target.GetComponent<EventLoader>().StandgimmickFlag, target.GetComponent<EventLoader>().StandgimmickFlag_Map2);
        FlagManager.Instance.IsEventing = false;
        yield break;
    }
    IEnumerator SetMushRoomNoMoto(int value, float delayTime, bool waitMovie)
    {
        if (waitMovie)
        {
            while (FlagManager.Instance.IsMovie == true)
                yield return null;
        }
        yield return new WaitForSeconds(delayTime);
        Debug.Log("SetMushRoomNoMoto");
        //  ここに関数追加予定
        MushroomControll.Instance.SetPastMush(value);
        yield break;
    }


    IEnumerator SetPiece(int value, float delayTime, bool waitMovie)
    {
        if (waitMovie)
        {
            while (FlagManager.Instance.IsMovie == true)
                yield return null;
        }
        yield return new WaitForSeconds(delayTime);
        Debug.Log("SetPiece");
        //  ここに関数追加予定
        PanelPuzzleControl.Instance.SetLastPiece();
        yield break;
    }
    IEnumerator SetOrb(int value, float delayTime, bool waitMovie)
    {
        if (waitMovie)
        {
            while (FlagManager.Instance.IsMovie == true)
                yield return null;
        }
        yield return new WaitForSeconds(delayTime);
        Debug.Log("SetOrb");
        //  ここに関数追加予定
        OrbSetter.Instance.OpenItemUI();
        yield break;
    }
    IEnumerator GlowMushroom(int value, float delayTime, bool waitMovie, GameObject target)
    {
        if (waitMovie)
        {
            while (FlagManager.Instance.IsMovie == true)
                yield return null;
        }
        yield return new WaitForSeconds(delayTime);
        Debug.Log("GlowMushroom");
        target.GetComponent<NowMushroom>().SetMush();
        yield break;
    }

    #endregion
}

public enum EventName
{
    //  流れ1
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
    //  流れ2
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
    //  流れ3
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

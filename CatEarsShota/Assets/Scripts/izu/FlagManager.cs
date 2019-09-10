using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DefaultExecutionOrder(-10)]
public class FlagManager : MonoBehaviour
{
    #region Singleton
    private static FlagManager instance;
    public static FlagManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogWarning("FlagManager is Null");
            }
            return instance;
        }
    }

    private bool CheckInstance()
    {
        if (instance == null)
        {
            instance = (FlagManager)this;
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

    //  過去モードか
    [SerializeField]
    private bool isPast = false;
    public bool IsPast
    {
        get { return isPast; }
        set
        {
            isPast = value;
            PlayerManager.Instance.SwitchPlayerMode(IsPast);
        }
    }
    //  動画再生中か
    [SerializeField]
    private bool isMovie = false;
    public bool IsMovie
    {
        get { return isMovie; }
        set { isMovie = value; }
    }
    //  イベント中か(演出中など)
    [SerializeField]
    private bool isEventing = false;
    public bool IsEventing
    {
        get { return isEventing; }
        set
        {
            isEventing = value;
            //Debug.Log("IsEventing : " + isEventing);
        }
    }
    //  UI開いているか
    [SerializeField]
    private bool isOpenUI = false;
    public bool IsOpenUI
    {
        get { return isOpenUI; }
        set { isOpenUI = value; }
    }

    [SerializeField]
    private bool isYellow = false;
    public bool IsYellow
    {
        get { return isYellow; }
        set { isYellow = value; }
    }
    [SerializeField]
    private bool isLockPast = false;
    public bool IsLockPast
    {
        get { return isLockPast; }
        set { isLockPast = value; }
    }


    [SerializeField, EnumFlags]
    private ItemFlag itemFlag;
    [SerializeField, EnumFlags]
    private GimmickFlag gimmickFlag;
    [SerializeField, EnumFlags]
    private GimmickFlag_Map2 gimmickFlag_Map2;


    void Awake()
    {
        CheckInstance();
        //  初回の動画再生
        MainCamera.Instance.CheckInstance();
        MainCamera.Instance.TriggeredVideo(0);
        InitFlag();
    }

    void Start()
    {
        
    }

    void Update()
    {
        DebugFlag();
    }


    /// <summary>
    /// デバッグ用
    /// </summary>
    private void DebugFlag()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ChegeFranPero();
        }
        if (Input.GetKeyDown(KeyCode.F10))
        {
            Debug.Log("更新");
            PlayerManager.Instance.SwitchPlayerMode(IsPast);
        }
    }

    /// <summary>
    /// フラグの初期化
    /// </summary>
    private void InitFlag()
    {
        itemFlag = 0;
        gimmickFlag = 0;
        gimmickFlag_Map2 = 0;
    }

    /// <summary>アイテムフラグの追加</summary>
    /// <param name="item">追加するアイテムフラグ</param>
    public void SetItemFlag(ItemFlag item)
    {
        itemFlag = itemFlag | item;
        EventManager.Instance.UpdateEvent();
    }
    /// <summary>ギミックフラグの追加</summary>
    /// <param name="gimmick">追加するギミックフラグ</param>
    public void SetGimmickFlag(GimmickFlag gimmick, GimmickFlag_Map2 gimmick2)
    {
        gimmickFlag = gimmickFlag | gimmick;
        gimmickFlag_Map2 = gimmickFlag_Map2 | gimmick2;
        EventManager.Instance.UpdateEvent();
    }
    public void SetGimmickFlag(GimmickFlag gimmick)
    {
        gimmickFlag = gimmickFlag | gimmick;
        EventManager.Instance.UpdateEvent();
    }
    public void SetGimmickFlag(GimmickFlag_Map2 gimmick2)
    {
        gimmickFlag_Map2 = gimmickFlag_Map2 | gimmick2;
        EventManager.Instance.UpdateEvent();
    }
    /// <summary>アイテムフラグの削除</summary>
    /// <param name="item">削除する対象</param>    
    public void FoldItemFlag(ItemFlag item)
    {
        itemFlag = itemFlag & ~item;
    }
    /// <summary>ギミックフラグの削除</summary>
    /// <param name="gimmick">削除する対象</param>
    public void FoldGimmickFlag(GimmickFlag gimmick)
    {
        gimmickFlag = gimmickFlag & ~gimmick;
    }
    public void FoldGimmickFlag(GimmickFlag_Map2 gimmick)
    {
        gimmickFlag_Map2 = gimmickFlag_Map2 & ~gimmick;
    }

    /// <summary>アイテムフラグのチェック</summary>
    /// <param name="item">チェックの対象</param>
    /// <returns></returns>
    public bool CheckItemFlag(ItemFlag item)
    {
        if (itemFlag.HasFlag(item))
        {
            return true;
        }
        return false;
    }
    /// <summary>ギミックフラグのチェック</summary>
    /// <param name="gimmick">チェックの対象</param>
    /// <returns></returns>
    public bool CheckGimmickFlag(GimmickFlag gimmick, GimmickFlag_Map2 gimmick2)
    {
        if (gimmickFlag.HasFlag(gimmick) && gimmickFlag_Map2.HasFlag(gimmick2))
        {
            return true;
        }
        return false;
    }
    public bool CheckGimmickFlag(GimmickFlag gimmick)
    {
        if (gimmickFlag.HasFlag(gimmick))
        {
            return true;
        }
        return false;
    }
    public bool CheckGimmickFlag(GimmickFlag_Map2 gimmick2)
    {
        if (gimmickFlag_Map2.HasFlag(gimmick2))
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// モードの切り替え
    /// </summary>
    public void ChegeFranPero()
    {
        IsPast = !IsPast;
    }
    public void ChageFranPero(bool isPero)
    {
        if (isPero)
            IsPast = false;
        else
            IsPast = true;
    }

    public void ChangeTimeFade()
    {
        StartCoroutine(ChangeTimeCorutine());
    }

    IEnumerator ChangeTimeCorutine()
    {
        FlagManager.Instance.IsEventing = true;
        SoundManager.Instance.StopBGM();
        Fade.Instance.StartFade(1.0f, Color.black);
        yield return new WaitForSeconds(1.0f);
        if (FlagManager.Instance.IsPast == false)
        {
            FlagManager.Instance.IsPast = true;
        }
        else if (FlagManager.Instance.IsPast == true)
        {
            FlagManager.Instance.IsPast = false;
        }
        yield return new WaitForSeconds(1f);
        Fade.Instance.StartFade(1.0f, Color.clear);
        SoundManager.Instance.TimeChangeStartBGM();
        yield return new WaitForSeconds(1.0f);
        FlagManager.Instance.IsEventing = false;
    }

}

[Flags]
public enum ItemFlag
{
    I_01_YellowOrb           = 1 << 0,
    I_02_BlueOrb             = 1 << 1,
    I_03_GreenOrb            = 1 << 2,
    I_04_RedOrb              = 1 << 3,
    I_05_CardKey             = 1 << 4,
    I_06_WallPaintingPiece   = 1 << 5,
    I_07_BookStopNow         = 1 << 6,
    I_08_ReportNo88          = 1 << 7,
    I_09_DiaryFran           = 1 << 8,
    I_10_Instructions        = 1 << 9,
    I_11_PlantBookPiece      = 1 << 10,
    I_12_Pants_A             = 1 << 11,
    I_13_Pants_B             = 1 << 12,
    I_14_Pants_C             = 1 << 13,
    I_15_Pants_D             = 1 << 14,
    I_16_Pants_E             = 1 << 15,
    I_17_Pants_F             = 1 << 16,
    I_18_BookStopPast        = 1 << 17,
    I_19_MushRoom            = 1 << 18,
}

[Flags]
public enum GimmickFlag
{
    G_01_StandUp                    = 1 << 0,
    G_02_Tuto_MoveAndJump           = 1 << 1,
    G_03_EquipBand                  = 1 << 2,
    G_04_TouchYellowGenerator       = 1 << 3,
    G_05_Tuto_TimeChenge            = 1 << 4,
    G_06_EscapeAlarm                = 1 << 5,
    G_07_CheckShelf                 = 1 << 6,
    G_08_PowerShortageDoor          = 1 << 7,
    G_09_Minigame1_0                = 1 << 8,
    G_10_OpenDoor                   = 1 << 9,
    G_11_EnterDoor                  = 1 << 10,
    G_12_PerraultTouchGenerator     = 1 << 11,
    G_13_Tuto_PickUp                = 1 << 12,
    G_14_Tuto_ItemInventory         = 1 << 13,
    //  Map1まで
    
}

public enum GimmickFlag_Map2
{
    G_15_PickUpPiece                = 1 << 0,
    G_16_PickUpDiary                = 1 << 1,
    G_17_Minigame1_Clear_Map2up     = 1 << 2,
    G_18_FranCheckExitDoor          = 1 << 3,
    G_19_EnterEnemy                 = 1 << 4,
    G_20_FillPiece_Past             = 1 << 5,
    G_21_SetMushroomNoMoto_1        = 1 << 6,
    G_22_DigPiece                   = 1 << 7,
    G_23_SetPiece                   = 1 << 8,
    G_24_Minigame2_Clear            = 1 << 9,
    G_25_RedOrbAnimation            = 1 << 10,
    //  赤オーブまで↑
    G_26_LevingMovie                = 1 << 11,
    G_27_Minigame1_Clear_Map2down   = 1 << 12,
    G_28_ChackAquarium              = 1 << 13,
    G_29_GetMushRoomNoMoto          = 1 << 14,
    G_30_BlueAnimation              = 1 << 15,
    G_31_EndPastMode                = 1 << 16,
    //  青オーブまで↑
    G_32_DidUp0_BookMark            = 1 << 17,
    G_33_GetCardKey                 = 1 << 18,
    G_34_GreenAnimation             = 1 << 19,
    G_35_SetOrb                     = 1 << 20,
    G_36_TrueAnimation              = 1 << 21,
    //  トゥルーエンドまで↑
    G_36_ChackAquarium_past         = 1 << 22,
    G_37_FillinBookMark             = 1 << 23,
    G_38_SetMoshroomNoMoto_2        = 1 << 24,
}

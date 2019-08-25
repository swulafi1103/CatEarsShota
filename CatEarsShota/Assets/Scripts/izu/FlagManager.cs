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
        set { isEventing = value; }
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


    [SerializeField, EnumFlags]
    private ItemFlag itemFlag;
    [SerializeField, EnumFlags]
    private GimmickFlag gimmickFlag;


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
    public void SetGimmickFlag(GimmickFlag gimmick)
    {
        gimmickFlag = gimmickFlag | gimmick;
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


    /// <summary>アイテムフラグのチェック</summary>
    /// <param name="item">チェックの対象</param>
    /// <returns></returns>
    public bool CheckItemFlag(ItemFlag item)
    {
        if (itemFlag.HasFlag(item))
        {
            //Debug.Log("True");
            return true;
        }
        //Debug.Log("False");
        return false;
    }
    /// <summary>ギミックフラグのチェック</summary>
    /// <param name="gimmick">チェックの対象</param>
    /// <returns></returns>
    public bool CheckGimmickFlag(GimmickFlag gimmick)
    {
        if (gimmickFlag.HasFlag(gimmick))
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
    public void ChegeFranPero(bool isPero)
    {
        if (isPero)
            IsPast = false;
        else
            IsPast = true;
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
    I_07_BookStop            = 1 << 6,
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
    G_15_PickUpPiece                = 1 << 14,
    G_16_PickUpDiary                = 1 << 15,
    G_17_Minigame1_Clear_Map2up     = 1 << 16,
    G_18_FranCheckExitDoor          = 1 << 17,
    G_19_EnterEnemy                 = 1 << 18,
    G_20_FillPiece_Past             = 1 << 19,
    G_21_SetMushroomNoMoto_Past     = 1 << 20,
    G_22_DigPiece                   = 1 << 21,
    G_23_SetPiece                   = 1 << 22,
    G_24_Minigame2_Clear            = 1 << 23,
    G_25_RedOrbAnimation            = 1 << 24,
    //  赤オーブまで↑
    G_26_LevingMovie                = 1 << 25,
    G_27_Minigame1_Clear_Map2down   = 1 << 26,
    G_28_ChackAquarium              = 1 << 27,
    G_29_GetMushRoomNoMoto          = 1 << 28,
    G_30_BlueAnimation              = 1 << 29,
    G_31_EndPastMode                = 1 << 30,
    //  青オーブまで↑
    G_32_DidUp0_BookMark            = 1 << 31,
    G_33_GetCardKey                 = 1 << 32,
    G_34_GreenAnimation             = 1 << 33,
    G_35_SetOrb                     = 1 << 34,
    G_36_TrueAnimation              = 1 << 35,
    //  トゥルーエンドまで↑
}

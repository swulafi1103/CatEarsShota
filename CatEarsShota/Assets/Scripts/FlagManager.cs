using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class FlagManager : MonoBehaviour
{
    //  過去モードか
    private bool isPast = false;
    public bool IsPast
    {
        get { return isPast; }
        set { isPast = value; }
    }
    //  イベント中か(動画や演出中など)
    private bool isEventing = false;
    public bool IsEventing
    {
        get { return IsEventing; }
        set { isEventing = value; }
    }
    //  UI開いているか
    private bool isOpenUI = false;
    public bool IsOpenUI
    {
        get { return IsOpenUI; }
        set { IsOpenUI = value; }
    }
    


    [SerializeField, EnumFlags]
    private ItemFlag itemFlag;
    [SerializeField, EnumFlags]
    private GimmickFlag gimmickFlag;


    void Awake()
    {
        InitFlag();
    }

    void Start()
    {
        
    }

    void Update()
    {
        test();
    }

    private void test()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Debug.Log("ItemFlag:" + itemFlag + " (2進数変換: " + Convert.ToString((int)itemFlag, 2));
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            itemFlag = (ItemFlag)(-1);
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            SetItemFlag(ItemFlag.RedOrb);
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            Debug.Log("FlagCheck : " + CheckItemFlag(ItemFlag.RedOrb));
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FoldItemFlag(ItemFlag.RedOrb);
        }
    }


    private void InitFlag()
    {
        itemFlag = 0;
        gimmickFlag = 0;
    }


    //  フラグの追加
    public void SetItemFlag(ItemFlag item)
    {
        itemFlag = itemFlag | item;
    }
    public void SetGimmickFlag(GimmickFlag gimmick)
    {
        gimmickFlag = gimmickFlag | gimmick;
    }

    //  フラグの削除
    public void FoldItemFlag(ItemFlag item)
    {
        itemFlag = itemFlag & ~item;
    }
    public void FoldGimmickFlag(GimmickFlag gimmick)
    {
        gimmickFlag = gimmickFlag & ~gimmick;
    }


    //  フラグのチェック
    public bool CheckItemFlag(ItemFlag item)
    {
        if ((itemFlag.HasFlag(item)))
        {
            return true;
        }
        return false;
    }
    public bool CheckGimmickFlag(GimmickFlag gimmick)
    {
        if ((gimmickFlag.HasFlag(gimmick)))
        {
            return true;
        }
        return false;
    }

}

[Flags]
public enum ItemFlag
{
    YellowOrb           = 1 << 0,
    BlueOrb             = 1 << 1,
    GreenOrb            = 1 << 2,
    RedOrb              = 1 << 3,
    CardKey             = 1 << 4,
    WallPaintingPiece   = 1 << 5,
    BookStop            = 1 << 6,
    ReportNo88          = 1 << 7,
    DiaryFran           = 1 << 8,
    Instructions        = 1 << 9,
    PlantBookPiece      = 1 << 10,
    Pants_A             = 1 << 11,
    Pants_B             = 1 << 12,
    Pants_C             = 1 << 13,
    Pants_D             = 1 << 14,
    Pants_E             = 1 << 15,
    Pants_F             = 1 << 16,
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
    G_15_WallPaintPiece             = 1 << 14,
    G_16_StudyDiary                 = 1 << 15,
    G_17_Minigame1_1                = 1 << 16,
    G_18_FranCheckExitDoor          = 1 << 17,
    G_19_PerraultCheckStorage       = 1 << 18,
}

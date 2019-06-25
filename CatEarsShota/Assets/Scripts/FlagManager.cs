using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FlagManager : MonoBehaviour
{
    [Flags]
    private enum ItemTypes
    {
        YellowOrb           = 1 << 0,
        BlueOrb             = 1 << 1,
        GreenOrb            = 1 << 2,
        RedOrb              = 1 << 3,
        CardKey             = 1 << 4,
        WallPaintingPiece   = 1 << 5,
        BookStop            = 1 << 6,
    }

    [Flags]
    private enum ReportTypes
    {
        ReportNo88      = 1 << 0,
        DiaryFran       = 1 << 1,
        Instructions    = 1 << 2,
        PlantBookPiece  = 1 << 3,
    }

    [Flags]
    private enum PantsTypes
    {
        Pants_A = 1 << 0,
        Pants_B = 1 << 1,
        Pants_C = 1 << 2,
        Pants_D = 1 << 3,
        Pants_E = 1 << 4,
        Pants_F = 1 << 5,
    }

    private ItemTypes ItemFlag;
    private ReportTypes ReportFlag;
    private PantsTypes PantsFlag;


    void Awake()
    {
        InitFlag();
    }

    void Start()
    {
        
    }

    void Update()
    {

    }

    private void InitFlag()
    {
        ItemFlag = 0;
        ReportFlag = 0;
        PantsFlag = 0;
    }

    private void SetItemFlag(ItemTypes item)
    {
        ItemFlag = ItemFlag | item;
    }

    private bool CheckItemFlag(ItemTypes item)
    {
        if ((ItemFlag & item) != 0)
        {
            return true;
        }
        return false;
    }

    private void SetReportFlag(ReportTypes report)
    {
        ReportFlag = ReportFlag | report;
    }

    private bool CheckReportFlag(ReportTypes report)
    {
        if ((ReportFlag & report) != 0)
        {
            return true;
        }
        return false;
    }

    private void SetPantsFlag(PantsTypes pants)
    {
        PantsFlag = PantsFlag | pants;
    }

    private bool CheckPantsFlag(PantsTypes pants)
    {
        if ((PantsFlag & pants) != 0)
        {
            return true;
        }
        return false;
    }

}

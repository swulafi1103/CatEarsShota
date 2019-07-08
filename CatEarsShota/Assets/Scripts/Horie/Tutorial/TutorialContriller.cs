using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialContriller : MonoBehaviour
{
    GameObject PerraultObj;
    GameObject FranObj;
    IconTutorial iconTutorial;
    PanelTutorial panelTutorial;

    static TutorialContriller instance = null;

    bool[] tutoFlag = new bool[10];

    public static TutorialContriller Instance {
        get { return instance; }
    }

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetData();
    }

    // Update is called once per frame
    void Update()
    {
        if (FlagManager.Instance.IsEventing) return;
        if (ActAnim()) return;
        MoveTutorial();
        CheckingTuto();
        ItemTuto();
        ChangeModeTuto();
    }

    public bool ActAnim()
    {
        if (FlagManager.Instance.IsEventing) return false;
        bool icon = iconTutorial.ActAnim;
        bool panel = panelTutorial.ActAnim;
        if (!icon && !panel) return false;
        else return true;
    }

    void SetData() {
        PerraultObj = FindObjectOfType<PerraultMove>().gameObject;
        //FranObj = FindObjectOfType<FranMove>().gameObject;

        iconTutorial = GetComponent<IconTutorial>();
        panelTutorial = GetComponent<PanelTutorial>();

        for(int i = 0; i < tutoFlag.Length; i++)
        {
            tutoFlag[i] = false;
        }
    }

    /// <summary>
    /// 移動、ジャンプ、二段ジャンプ
    /// 0
    /// </summary>
    void MoveTutorial() {
        if (tutoFlag[0]) return;
        if (!FlagManager.Instance.CheckGimmickFlag(GimmickFlag.G_01_StandUp)) return;
        Rigidbody2D p = PerraultObj.GetComponent<Rigidbody2D>();
        iconTutorial.MoveTuto(p);
        tutoFlag[0] = true;
    }

    /// <summary>
    /// 「調べる」チュートリアル
    /// 1 , 2
    /// </summary>
    void CheckingTuto()
    {
        if (FlagManager.Instance.CheckGimmickFlag(GimmickFlag.G_02_Tuto_MoveAndJump) && !tutoFlag[1])
        {
            iconTutorial.IconTuto(IconTutorial.IconNum.Checking);
            tutoFlag[1] = true;
        }
        if (FlagManager.Instance.CheckGimmickFlag(GimmickFlag.G_03_EquipBand) && !tutoFlag[2])
        {
            iconTutorial.IconTuto(IconTutorial.IconNum.Checking2);
            tutoFlag[2] = true;
        }
    }

    /// <summary>
    /// 「アイテム拾う」チュートリアル
    /// 3
    /// </summary>
    void ItemTuto()
    {
        if (tutoFlag[3]) return;
        if (!FlagManager.Instance.CheckGimmickFlag(GimmickFlag.G_08_PowerShortageDoor)) return;
        iconTutorial.IconTuto(IconTutorial.IconNum.Item);
        tutoFlag[3] = true;
    }

    /// <summary>
    /// 「アイテム欄を開く」チュートリアル
    /// 4
    /// </summary>
    void ItemUITuto()
    {
        iconTutorial.IconTuto(IconTutorial.IconNum.ItemUI);
    }

    /// <summary>
    /// 「時間切り替え」チュートリアル
    /// 5
    /// </summary>
    void ChangeModeTuto() {
        if (tutoFlag[5]) return;
        if (!FlagManager.Instance.CheckGimmickFlag(GimmickFlag.G_04_TouchYellowGenerator)) return;
        panelTutorial.PanelTuto(PanelTutorial.PanelNum.ChangeMode);
        tutoFlag[5] = true;
    }

    /// <summary>
    /// パンツ説明
    /// 6
    /// </summary>
    void PantsTuto() {
        panelTutorial.PanelTuto(PanelTutorial.PanelNum.Pants);
    }

    /// <summary>
    /// パンツ切り替え説明
    /// 7
    /// </summary>
    void ChangePantsTuto() {
        panelTutorial.PanelTuto(PanelTutorial.PanelNum.ChangePants);
    }

    /// <summary>
    /// タイムカプセル説明
    /// 8
    /// </summary>
    void TimeCapsuleTuto() {
        panelTutorial.PanelTuto(PanelTutorial.PanelNum.TimeCapsule);
    }

    /// <summary>
    /// キノコ説明
    /// 9
    /// </summary>
    void MushroomTuto() {
        panelTutorial.PanelTuto(PanelTutorial.PanelNum.Mushroom);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialContriller : MonoBehaviour
{
    GameObject PerraultObj;
    GameObject FranObj;
    IconTutorial iconTutorial;
    PanelTutorial panelTutorial;
    TextWindow textWindow;

    static TutorialContriller instance = null;

    bool[] tutoFlag = new bool[8];

    GameObject TutoColliderObj;

    int NowColliderNum = 0;


    [SerializeField]
    Camera PerraultCamera;
    [SerializeField]
    Camera FranCamera;

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
        PantsTuto();
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
        textWindow = GetComponentInChildren<TextWindow>();

        TutoColliderObj = FindObjectOfType<TutoCollider>().gameObject;
        TutoColliderObj.SetActive(false);

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
        SetCollider(0);
    }

    /// <summary>
    /// 「調べる」チュートリアル
    /// 1 , 2
    /// </summary>
    void CheckingTuto()
    {
        if (FlagManager.Instance.CheckGimmickFlag(GimmickFlag.G_02_Tuto_MoveAndJump) && !tutoFlag[1])
        {
            CheckCamera();
            iconTutorial.IconTuto(IconTutorial.IconNum.Checking);
            tutoFlag[1] = true;
            SetCollider(1);
        }
        if (FlagManager.Instance.CheckGimmickFlag(GimmickFlag.G_03_EquipBand) && !tutoFlag[2])
        {
            CheckCamera();
            iconTutorial.IconTuto(IconTutorial.IconNum.Checking2);
            tutoFlag[2] = true;
            SetCollider(2);
        }
    }

    /// <summary>
    /// 「アイテム拾う」チュートリアル
    /// 3
    /// </summary>
    void ItemTuto()
    {
        if (tutoFlag[3]) return;
        if (!FlagManager.Instance.CheckGimmickFlag(GimmickFlag.G_09_Minigame1_0)) return;
        CheckCamera();
        iconTutorial.IconTuto(IconTutorial.IconNum.Item);
        tutoFlag[3] = true;
        TutoColliderObj.SetActive(false);
    }
    

    /// <summary>
    /// 「時間切り替え」チュートリアル
    /// 4
    /// </summary>
    public void ChangeModeTuto() {
        if (tutoFlag[4]) return;
        CheckCamera();
        panelTutorial.PanelTuto(PanelTutorial.PanelNum.ChangeMode);
        tutoFlag[4] = true;
    }

    /// <summary>
    /// パンツ説明
    /// 5
    /// </summary>
    void PantsTuto()
    {
        if (tutoFlag[5]) return;
        if (!FlagManager.Instance.CheckItemFlag(ItemFlag.I_12_Pants_A)) return;
        CheckCamera();
        panelTutorial.PanelTuto(PanelTutorial.PanelNum.Pants);
        tutoFlag[5] = true;
    }
    

    /// <summary>
    /// タイムカプセル説明
    /// 6
    /// </summary>
    public void TimeCapsuleTuto()
    {
        CheckCamera();
        panelTutorial.PanelTuto(PanelTutorial.PanelNum.TimeCapsule);
    }

    /// <summary>
    /// キノコ説明
    /// 7
    /// </summary>
    public void MushroomTuto()
    {
        CheckCamera();
        panelTutorial.PanelTuto(PanelTutorial.PanelNum.Mushroom);
    }

    /// <summary>
    /// objctに近づいたら発生するチュートリアル用のコライダー生成
    /// 0:check1,1:check2,2:item
    /// </summary>
    /// <param name="num">0:check1,1:check2,2:item</param>
    void SetCollider(int num) {
        TutoColliderObj.SetActive(true);
        TutoColliderObj.GetComponent<TutoCollider>().SetPosition(num);
        NowColliderNum = num;
    }

    /// <summary>
    /// objctに近づいたら発生するチュートリアル
    /// 0:check1,1:check2,2:item
    /// </summary>
    public void OnTutoCollider()
    {
        switch (NowColliderNum)
        {
            case 0:
                CheckingTuto();
                break;
            case 1:
                CheckingTuto();
                break;
            case 2:
                ItemTuto();
                break;
            case 3:
                break;
        }
    }

    public void SetTextWindow(int num) {
        CheckCamera();
        textWindow.SetWindow(num);
    }

    void CheckCamera()
    {
        //transform.GetComponent<Canvas>().worldCamera = Camera.current;
        if (FlagManager.Instance.IsPast)
        {
            transform.GetComponent<Canvas>().worldCamera = FranCamera;
        }
        else
        {
            transform.GetComponent<Canvas>().worldCamera = PerraultCamera;
        }
    }
}

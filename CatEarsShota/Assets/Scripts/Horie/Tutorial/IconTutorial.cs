using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconTutorial : MonoBehaviour
{
    [SerializeField]
    List<Sprite> Icons = new List<Sprite>();
    [SerializeField]
    Image IconImage;

    int AnimFrame = 30;

    Rigidbody2D PlayerRid;

    bool actAnim = false;
    public bool ActAnim
    {
        get { return actAnim; }
    }

    float moveVec = 0;
    [SerializeField]
    float moveRange = 1000;

    bool FirstJump = false;

    [SerializeField]
    Sprite[] hasigoTutoImage = new Sprite[2];

    public enum IconNum {
        Move = 0,
        Jump,
        HiJump,
        Checking,
        Checking2,
        Item,
        ItemUI,
        None
    }

    private IconNum NowNum;

    // Start is called before the first frame update
    void Start()
    {
        NowNum = IconNum.None;
        actAnim = false;
        FirstJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (TutorialContriller.Instance.ActAnim()) return;
        switch (NowNum) {
            case IconNum.Move:
                CheckMove();
                break;
            case IconNum.Jump:
                CheckJump();
                break;
            case IconNum.HiJump:
                CheckHiJump();
                break;
            case IconNum.Checking:
                Checking();
                break;
            case IconNum.Checking2:
                Checking2();
                break;
            case IconNum.Item:
                ItemTuto();
                break;
            case IconNum.ItemUI:
                ItemUITuto();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 判定にRigidBody必須なチュートリアル表示
    /// 移動
    /// </summary>
    public void MoveTuto(Rigidbody2D rid) {
        PlayerRid = rid;
        NowNum = IconNum.Move;
        StartCoroutine(StartAnim());
    }

    /// <summary>
    /// RigidBody不必要なチュートリアル表示
    /// </summary>
    public void IconTuto(IconNum num) {
        NowNum = num;
        StartCoroutine(StartAnim());
    }

    /// <summary>
    /// 移動判定
    /// </summary>
    void CheckMove() {
        moveVec += Mathf.Abs(PlayerRid.velocity.x);
        if (moveVec >= moveRange) {
            StartCoroutine(AnimSet(IconNum.Jump));
        }
    }

    /// <summary>
    /// ジャンプ判定
    /// </summary>
    void CheckJump() {
        if (!Input.GetKeyDown(KeyCode.Space)) return;
        StartCoroutine(AnimSet(IconNum.HiJump));
    }

    /// <summary>
    /// 二段ジャンプ判定
    /// </summary>
    void CheckHiJump() {

        if (FirstJump) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                StartCoroutine(EndAnim());
                FlagManager.Instance.SetGimmickFlag(GimmickFlag.G_02_Tuto_MoveAndJump);
                FirstJump = false;
            }else if (Mathf.Abs(PlayerRid.velocity.y) == 0) {
                FirstJump = false;
                return;
            }
        }
        else {
            if (Input.GetKeyDown(KeyCode.Space)) {
                FirstJump = true;
            }
        }
    }

    /// <summary>
    /// 「調べる」判定
    /// </summary>
    void Checking() {
        //if (!Input.GetKeyDown(KeyCode.A)) return;
        bool check1 = FlagManager.Instance.CheckGimmickFlag(GimmickFlag.G_03_EquipBand);
        if (!check1) return;
        StartCoroutine(EndAnim());
    }

    void Checking2()
    {
        //if (!Input.GetKeyDown(KeyCode.A)) return;
        bool check2 = FlagManager.Instance.CheckGimmickFlag(GimmickFlag.G_04_TouchYellowGenerator);
        if (!check2) return;
        StartCoroutine(EndAnim());
    }

    /// <summary>
    /// 「アイテム拾う」判定
    /// </summary>
    void ItemTuto() {
        bool item = ItemManager.Instance.IsGet(ItemManager.ItemNum.Yerrow_Orb);
        if (!item) return;
        StartCoroutine(AnimSet(IconNum.ItemUI));
    }

    /// <summary>
    /// 「アイテム欄」判定
    /// </summary>
    void ItemUITuto() {
        if (!Input.GetKeyDown(KeyCode.D)) return;
        StartCoroutine(EndAnim());
    }

    /// <summary>
    /// 非表示→次表示一連操作
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    IEnumerator AnimSet(IconNum num) {
        var col = StartCoroutine(EndAnim());
        yield return col;
        IconTuto(num);
    }

    /// <summary>
    /// 表示アニメーション開始
    /// </summary>
    /// <returns></returns>
    IEnumerator StartAnim() {
        if (actAnim) {
            yield break;
        }
        actAnim = true;

        int SPNum = (int)NowNum;
        if (SPNum >= Icons.Count) {
            Debug.Log("NULL NUM:" + SPNum);
            yield break;
        }
        IconImage.enabled = false;

        Color color = Color.white;
        color.a = 0;
        IconImage.color = color;
        IconImage.sprite = Icons[SPNum];
        IconImage.SetNativeSize();
        IconImage.enabled = true;
        for (int i = 0; i <= AnimFrame; i++) {
            float a = (float)i / AnimFrame;
            color.a = a;
            IconImage.color = color;
            yield return null;
        }
        actAnim = false;
    }

    /// <summary>
    /// 終了アニメーション開始
    /// </summary>
    /// <returns></returns>
    IEnumerator EndAnim() {
        if (actAnim) {
            yield break;
        }
        actAnim = true;

        Color color = Color.white;
        color.a = 1;
        IconImage.color = color;

        for(int i = AnimFrame; i >= 0; i--) {
            float a = (float)i / AnimFrame;
            color.a = a;
            IconImage.color = color;
            yield return null;
        }
        NowNum = IconNum.None;
        actAnim = false;
        yield break;
    }

    public void StartHasigoTuto(int num) {
        StartCoroutine(HasigoTuto(num));
    }

    IEnumerator HasigoTuto(int num) {
        Color color = Color.white;
        color.a = 0;
        IconImage.color = color;
        IconImage.sprite = hasigoTutoImage[num];
        IconImage.SetNativeSize();
        IconImage.enabled = true;
        for (int i = 0; i <= AnimFrame; i++) {
            float a = (float)i / AnimFrame;
            color.a = a;
            IconImage.color = color;
            yield return null;
        }
        yield break;
    }

    public void EndHasigoTuto() {
        StartCoroutine(EndHasigo());
    }

    IEnumerator EndHasigo() {
        Color color = Color.white;
        color.a = 1;
        IconImage.color = color;

        for (int i = AnimFrame; i >= 0; i--) {
            float a = (float)i / AnimFrame;
            color.a = a;
            IconImage.color = color;
            yield return null;
        }

        yield break;
    }
}
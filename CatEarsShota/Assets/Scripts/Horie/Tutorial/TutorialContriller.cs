using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialContriller : MonoBehaviour
{
    GameObject PerraultObj;
    GameObject FranObj;
    IconTutorial iconTutorial;

    static TutorialContriller instance = null;

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
        MoveTutorial();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetData() {
        PlayerMoves[] objs = FindObjectsOfType<PlayerMoves>();
        foreach(PlayerMoves player in objs) {
            if (player.gameObject.name == "Perrault") {
                PerraultObj = player.gameObject;
            }
            if (player.gameObject.name == "Fran") {
                FranObj = player.gameObject;
            }
        }

        iconTutorial = GetComponent<IconTutorial>();
    }

    /// <summary>
    /// 移動、ジャンプ、二段ジャンプ
    /// </summary>
    public void MoveTutorial() {
        Rigidbody2D p = PerraultObj.GetComponent<Rigidbody2D>();
        iconTutorial.MoveTuto(p);
    }

    /// <summary>
    /// 「調べる」チュートリアル
    /// </summary>
    public void CheckingTuto() {
        iconTutorial.IconTuto(IconTutorial.IconNum.Checking);
    }

    /// <summary>
    /// 「アイテム拾う」チュートリアル
    /// </summary>
    public void ItemTuto() {
        iconTutorial.IconTuto(IconTutorial.IconNum.Item);
    }

    /// <summary>
    /// 「アイテム欄を開く」チュートリアル
    /// </summary>
    public void ItemUITuto() {
        iconTutorial.IconTuto(IconTutorial.IconNum.ItemUI);
    }
}

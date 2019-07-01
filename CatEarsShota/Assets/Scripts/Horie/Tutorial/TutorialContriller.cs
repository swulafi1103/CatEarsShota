﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialContriller : MonoBehaviour
{
    GameObject PerraultObj;
    GameObject FranObj;
    IconTutorial iconTutorial;
    PanelTutorial panelTutorial;

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
        //MoveTutorial();   //debug
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetData() {
        PerraultObj = FindObjectOfType<PerraultMove>().gameObject;
        //FranObj = FindObjectOfType<FranMove>().gameObject;

        iconTutorial = GetComponent<IconTutorial>();
        panelTutorial = GetComponent<PanelTutorial>();
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

    /// <summary>
    /// 「時間切り替え」チュートリアル
    /// </summary>
    public void ChangeModeTuto() {
        panelTutorial.PanelTuto(PanelTutorial.PanelNum.ChangeMode);
    }

    /// <summary>
    /// パンツ説明
    /// </summary>
    public void PantsTuto() {
        panelTutorial.PanelTuto(PanelTutorial.PanelNum.Pants);
    }

    /// <summary>
    /// パンツ切り替え説明
    /// </summary>
    public void ChangePantsTuto() {
        panelTutorial.PanelTuto(PanelTutorial.PanelNum.ChangePants);
    }

    /// <summary>
    /// タイムカプセル説明
    /// </summary>
    public void TimeCapsuleTuto() {
        panelTutorial.PanelTuto(PanelTutorial.PanelNum.TimeCapsule);
    }

    /// <summary>
    /// キノコ説明
    /// </summary>
    public void MushroomTuto() {
        panelTutorial.PanelTuto(PanelTutorial.PanelNum.Mushroom);
    }
}

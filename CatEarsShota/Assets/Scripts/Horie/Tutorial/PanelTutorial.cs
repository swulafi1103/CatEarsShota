using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelTutorial : MonoBehaviour
{
    [SerializeField]
    List<Sprite> panels = new List<Sprite>();
    [SerializeField]
    GameObject Panel;
    RectTransform PanelTrans;
    Image PanelImage;

    float AnimFrame = 10;
    bool actAnim = false;
    public bool ActAnim
    {
        get { return actAnim; }
    }
    

    public enum PanelNum {
        ChangeMode = 0,
        Pants,
        ChangePants,
        TimeCapsule,
        Mushroom,
        None
    }

    PanelNum nowNum = PanelNum.None;

    // Start is called before the first frame update
    void Start()
    {
        nowNum = PanelNum.None;
        PanelTrans = Panel.GetComponent<RectTransform>();
        PanelImage = Panel.GetComponent<Image>();
        actAnim = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (TutorialContriller.Instance.ActAnim()) return;
        CheckBackKey();
        CheckAKey();
    }

    void CheckBackKey() {
        if (nowNum == PanelNum.None) return;
        if (nowNum == PanelNum.Pants) return;
        if (!Input.GetKeyDown(KeyCode.X)) return;
        switch (nowNum) {
            case PanelNum.ChangeMode:
                FlagManager.Instance.SetGimmickFlag(GimmickFlag.G_05_Tuto_TimeChenge);
                StartCoroutine(EndAnim());
                break;
            //case PanelNum.Pants:
            //    StartCoroutine(AnimSet(PanelNum.ChangePants));
            //    break;
            default:
                StartCoroutine(EndAnim());
                break;
        }
    }

    void CheckAKey()
    {
        if (nowNum != PanelNum.Pants) return;
        if (!Input.GetKeyDown(KeyCode.A)) return;
        StartCoroutine(EndAnim());
    }

    public void PanelTuto(PanelNum num) {
        nowNum = num;
        StartCoroutine(StartAnim());

    }

    /// <summary>
    /// 開始アニメーション
    /// </summary>
    /// <returns></returns>
    IEnumerator StartAnim() {
        if (actAnim) yield break;

        actAnim = true;

        PanelTrans.localScale = new Vector2(1, 0);
        int num = (int)nowNum;
        if (num >= panels.Count) {
            Debug.Log("NULL NUM:" + num);
            yield break;
        }
        PanelImage.sprite = panels[num];
        PanelImage.SetNativeSize();

        for(float f = 0; f <= AnimFrame; f++) {
            float sizeY = f / AnimFrame;
            PanelTrans.localScale = new Vector2(1, sizeY);
            yield return null;
        }
        //PanelImage.SetNativeSize();
        actAnim = false;
        yield break;
    }

    IEnumerator EndAnim() {
        if (actAnim) yield break;

        actAnim = true;

        PanelTrans.localScale = Vector2.one;
        for (float f = AnimFrame; f >= 0; f--) {
            float sizeY = f / AnimFrame;
            PanelTrans.localScale = new Vector2(1, sizeY);
            yield return null;
        }
        nowNum = PanelNum.None;
        actAnim = false;

        yield break;
    }

    IEnumerator AnimSet(PanelNum num)
    {
        Coroutine col = StartCoroutine(EndAnim());
        yield return col;

        nowNum = num;

        col = StartCoroutine(StartAnim());
        yield return col;

        yield break;
    }
}

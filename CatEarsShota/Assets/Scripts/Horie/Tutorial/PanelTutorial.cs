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
    bool ActAnim = false;

    [SerializeField]
    Vector2 firstSize = new Vector2(800, 600);

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
        ActAnim = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) {
            PanelTuto(PanelNum.ChangeMode);
        }
        CheckBackKey();
    }

    void CheckBackKey() {
        if (nowNum == PanelNum.None) return;
        if (!Input.GetKeyDown(KeyCode.X)) return;
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
        if (ActAnim) yield break;

        ActAnim = true;

        PanelTrans.sizeDelta = new Vector2(firstSize.x, 0);
        int num = (int)nowNum;
        if (num >= panels.Count) {
            Debug.Log("NULL NUM:" + num);
            yield break;
        }
        PanelImage.sprite = panels[num];
        PanelImage.SetNativeSize();

        for(float f = 0; f <= AnimFrame; f++) {
            float sizeY = firstSize.y * (f / AnimFrame);
            PanelTrans.sizeDelta = new Vector2(firstSize.x, sizeY);
            yield return null;
        }
        ActAnim = false;
    }

    IEnumerator EndAnim() {
        if (ActAnim) yield break;

        ActAnim = true;

        PanelTrans.sizeDelta = firstSize;
        for (float f = AnimFrame; f >= 0; f--) {
            float sizeY = firstSize.y * (f / AnimFrame);
            PanelTrans.sizeDelta = new Vector2(firstSize.x, sizeY);
            yield return null;
        }
        nowNum = PanelNum.None;
        ActAnim = false;
    }
}

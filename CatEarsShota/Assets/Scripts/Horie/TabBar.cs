using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabBar : MonoBehaviour
{
    [SerializeField]
    Vector3[] movedata = new Vector3[3];

    RectTransform Rect;

    // Start is called before the first frame update
    void Start()
    {
        Rect = this.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /// <summary>
    /// 表示
    /// </summary>
    /// <param name="act"></param>
    public void ActAnim(bool act) {
        this.gameObject.SetActive(act);
    }

    /// <summary>
    /// 線移動
    /// </summary>
    /// <param name="num"></param>
    public void MoveLine(int num) {
        Rect.localPosition = movedata[num];
    }
}

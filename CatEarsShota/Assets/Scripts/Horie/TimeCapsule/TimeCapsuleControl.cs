using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCapsuleControl : MonoBehaviour
{
    private static TimeCapsuleControl instance;
    public static TimeCapsuleControl Instance {
        get { return instance; }
    }

    private void Awake() {
        instance = this;
    }

    ItemManager.ItemNum[] list = new ItemManager.ItemNum[2] {
        ItemManager.ItemNum.Ilust_Piece,
        ItemManager.ItemNum.BookMark_Past
    };

    //[SerializeField]
    //SpriteRenderer pastTimeCapsule;

    //[SerializeField]
    //SpriteRenderer nowTimeCapsule;

    //[SerializeField]
    //Sprite nowOnItemSprite;

    //[SerializeField]
    //Sprite pastOnItemSprite;


    GameObject map2;

    // Start is called before the first frame update
    void Start()
    {
        map2 = GameObject.FindGameObjectWithTag("Map2");
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Y))
        //    SetUI(1);
    }

    /// <summary>
    /// 過去で埋めるためにアイテム欄表示する
    /// </summary>
    /// <param name="num"></param>
    public void SetUI(int num) {
        if (num == 0) {
            ItemManager.Instance.SelectEvent(list[num], SetKey);
        }
        else {
            ItemManager.Instance.SelectEvent(list[num], SetBookmark);
        }
    }
    
    void SetKey() {
        setItem(0);
    }

    void SetBookmark() {
        setItem(1);
    }

    /// <summary>
    /// アイテム選択された
    /// </summary>
    /// <param name="num"></param>
    void setItem(int num) {
        //画像変更処理
        map2.GetComponent<MapStatus>().UpdateGimmick(1, true);
        map2.GetComponent<MapStatus>().UpdateGimmick(0, true);

        //フラグ処理
        ItemManager.Instance.SetItemData(list[num]);
    }
}

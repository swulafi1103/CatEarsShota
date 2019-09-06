using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbSetter : MonoBehaviour
{
    [SerializeField]
    Image[] OrbImages = new Image[4];
    [SerializeField]
    SpriteRenderer[] MapOrbImages = new SpriteRenderer[4];

    [SerializeField]
    GameObject OrbSetterImage;

    ItemManager.ItemNum[] orblist =
    {
        ItemManager.ItemNum.Blue_Orb,
        ItemManager.ItemNum.Green_Orb,
        ItemManager.ItemNum.Red_Orb,
        ItemManager.ItemNum.Yerrow_Orb
    };

    

    private static OrbSetter instance;
    public static OrbSetter Instance {
        get { return instance; }
    }

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Y))
        //    SetDetailOrb();

        //OrbCheck();
        CheckXKey();
        SetitemUI();
    }

    public void SetDetailOrb()
    {
        OrbSetterImage.SetActive(true);
        FlagManager.Instance.IsEventing = true;
    }

    void CheckXKey()
    {
        if (!OrbSetterImage.activeSelf) return;
        if (!Input.GetKeyDown(KeyCode.X)) return;
        FlagManager.Instance.IsEventing = false;
        OrbSetterImage.SetActive(false);
    }

    void SetitemUI()
    {
        if (!OrbSetterImage.activeSelf) return;
        if (!Input.GetKeyDown(KeyCode.D)) return;
        OpenItemUI();
    }

    public void OpenItemUI() {
        OrbSetterImage.SetActive(false);
        FlagManager.Instance.IsEventing = false;

        System.Action[] eventList = new System.Action[4] {
        OrbSet0,
        OrbSet1,
        OrbSet2,
        OrbSet3,
        };

        ItemManager.Instance.SetOrbUI(orblist, eventList);
    }

    void OrbCheck()
    {
        for(int i = 0; i < OrbImages.Length; i++)
        {
            if (OrbImages[i].enabled == true) continue;
            if (!ItemManager.Instance.SelectedEventItem(orblist[i])) continue;
            OrbSetterImage.SetActive(true);
            OrbImages[i].enabled = true;
            MapOrbImages[i].enabled = true;
            CheckFlag();
        }
    }
    

    private void OrbSet0() {
        OrbSet(0);
    }

    void OrbSet1() {
        OrbSet(1);
    }

    void OrbSet2() {
        OrbSet(2);
    }

    void OrbSet3() {
        OrbSet(3);
    }

    void OrbSet(int num) {
        SetDetailOrb();
        OrbImages[num].enabled = true;
        MapOrbImages[num].enabled = true;
        ItemManager.Instance.SetItemData(orblist[num]);
        CheckFlag();
    }

    /// <summary>
    /// ここで何がはめられているかでflagmanager変更
    /// </summary>
    void CheckFlag()
    {
        foreach(SpriteRenderer orb in MapOrbImages)
        {
            if (orb.enabled == false)
            {
                return;
            }
        }

        //トゥルーエンド呼び出し
        Debug.Log("ALL ORB SETED");
    }
}

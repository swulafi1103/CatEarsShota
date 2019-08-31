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
        //ItemManager.Instance.SetEvents(orblist);
        for(int i = 0; i < orblist.Length; i++) {
            switch (i) {
                case 0:
                    ItemManager.Instance.SelectEvent(orblist[i], OrbSet0);
                    break;
                case 1:
                    ItemManager.Instance.SelectEvent(orblist[i], OrbSet1);
                    break;
                case 2:
                    ItemManager.Instance.SelectEvent(orblist[i], OrbSet2);
                    break;
                case 3:
                    ItemManager.Instance.SelectEvent(orblist[i], OrbSet3);
                    break;
            }
        }
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
    

    void OrbSet0() {
        OrbSetterImage.SetActive(true);
        OrbImages[0].enabled = true;
        MapOrbImages[0].enabled = true;
        CheckFlag();
    }

    void OrbSet1() {
        OrbSetterImage.SetActive(true);
        OrbImages[1].enabled = true;
        MapOrbImages[1].enabled = true;
        CheckFlag();
    }

    void OrbSet2() {
        OrbSetterImage.SetActive(true);
        OrbImages[2].enabled = true;
        MapOrbImages[2].enabled = true;
        CheckFlag();
    }

    void OrbSet3() {
        OrbSetterImage.SetActive(true);
        OrbImages[3].enabled = true;
        MapOrbImages[3].enabled = true;
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
    }
}

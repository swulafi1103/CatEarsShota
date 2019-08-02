using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbSetter : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer[] OrbImages = new SpriteRenderer[4];

    ItemManager.ItemNum[] orblist =
    {
        ItemManager.ItemNum.Blue_Orb,
        ItemManager.ItemNum.Green_Orb,
        ItemManager.ItemNum.Red_Orb,
        ItemManager.ItemNum.Yerrow_Orb
    };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OrbCheck();
    }

    public void SetitemUI()
    {
        ItemManager.Instance.SetEvents(orblist);
    }

    void OrbCheck()
    {
        for(int i = 0; i < OrbImages.Length; i++)
        {
            if (OrbImages[i].enabled == true) return;
            if (!ItemManager.Instance.SelectedEventItem(orblist[i])) return;
            OrbImages[i].enabled = true;
        }
    }
}

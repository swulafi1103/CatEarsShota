using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapStatus : MonoBehaviour
{
    public YelloObj[] yelloObjs = new YelloObj[10];
    public ChangableObj[] gimmickObjs = new ChangableObj[5];
    public ChangableObj[] Map2Objs = new ChangableObj[2];
    private int currentcolor =0;
    /*  public enum mapColor
      {
          yellow,
          red,
          blue,
          green
      }*/
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            ChangeColorObj(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeColorObj(0);
        }else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeColorObj(1);
        }else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeColorObj(2);
        }else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeColorObj(3);
        }

    }

    public void ChangeColorObj(int newColor)
    {
        currentcolor = newColor;
        Debug.Log(gameObject.name);
        for (int i = 0; i < yelloObjs.Length; i++)
        {
            if (yelloObjs[i].ChangeObj != null)
            {
                yelloObjs[i].ChangeObj.GetComponent<SpriteRenderer>().sprite = yelloObjs[i].colorSprite[newColor];
            }
            if (yelloObjs[i].ChangeObj == null)
            {
                Debug.Log("マップ色エラー");
            }
        }
        for (int i = 0; i < gimmickObjs.Length; i++)
        {
            if (gimmickObjs[i].ChangeObj != null)
                gimmickObjs[i].ChangeObj.GetComponent<SpriteRenderer>().sprite = gimmickObjs[i].colorSprite[newColor * 2 + (gimmickObjs[i].OnStatus ? 1 : 0)];
            else
                Debug.Log("gimmickObjs error"+" "+gameObject.name );
        }
    }

    public void UpdateGimmick(int index,bool newstatus)
    {
        gimmickObjs[index].OnStatus = newstatus;
        if (gimmickObjs[index].ChangeObj != null)
            gimmickObjs[index].ChangeObj.GetComponent<SpriteRenderer>().sprite = gimmickObjs[index].colorSprite[currentcolor * 2 + (gimmickObjs[index].OnStatus ? 1 : 0)];
        else
            Debug.Log("Update gimmickObjs error");
    }
    public void UpdateMapPast(int index, bool newstatus)
    {
        Map2Objs[index].OnStatus = newstatus;
        if (Map2Objs[index].ChangeObj != null)
            Map2Objs[index].ChangeObj.GetComponent<SpriteRenderer>().sprite = Map2Objs[index].colorSprite[Map2Objs[index].OnStatus ? 1:0];
        else
            Debug.Log("Update Map2Objs error");
    }
}

[System.Serializable]
public struct YelloObj
{
    public GameObject ChangeObj;
    public Sprite[] colorSprite;
}
[System.Serializable]
public struct ChangableObj
{
    public bool OnStatus;
    public GameObject ChangeObj;
    public Sprite[] colorSprite;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapStatus : MonoBehaviour
{
    public bool[] MapObjectState = new bool[3];
    public GameObject[] MapObject = new GameObject[3];
    public YelloObj[] yelloObjs = new YelloObj[10];

    private bool turnyellow = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            ChangeColorObj();
        }
    }
    void FixedUpdate()
    {
        if (!turnyellow)
        {
            for (int i = 0; i < 3; i++)
            {
                if (MapObjectState[i] == true)
                    MapObject[i].GetComponent<ObjectStatus>().index = 1;
                else
                    MapObject[i].GetComponent<ObjectStatus>().index = 0;
            }
        }
    }

    public void ChangeColorObj()
    {
        turnyellow = true;
        for (int i = 0; i < 3; i++)
        {
            MapObject[i].GetComponent<ObjectStatus>().index = 3;
        }
        for (int i = 0; i < yelloObjs.Length; i++)
        {
            if (yelloObjs[i].ChangeObj != null)
            {
                yelloObjs[i].ChangeObj.GetComponent<SpriteRenderer>().sprite = yelloObjs[i].yellowSprite;
            }
            if (yelloObjs[i].ChangeObj == null)
            {
                Debug.Log("黄色でエラー");
            }
        }
    }
}

[System.Serializable]
public struct YelloObj
{
    public GameObject ChangeObj;
    public Sprite yellowSprite;    
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapStatus : MonoBehaviour
{
    public bool[] MapObjectState = new bool[3];
    public GameObject[] MapObject = new GameObject[3];
    public YelloObj[] yelloObjs = new YelloObj[10];
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        for(int i=0; i < 3; i++)
        {
            if (MapObjectState[i] == true)
                MapObject[i].GetComponent<ObjectStatus>().index = 1;
            else
                MapObject[i].GetComponent<ObjectStatus>().index = 0;
        }
    }

    public void ChangeColorObj()
    {
        for (int i = 0; i < yelloObjs.Length; i++)
        {
            yelloObjs[i].ChangeObj.GetComponent<SpriteRenderer>().sprite = yelloObjs[i].yellowSprite;
        }
    }
}

[System.Serializable]
public struct YelloObj
{
    public GameObject ChangeObj;
    public Sprite yellowSprite;    
}
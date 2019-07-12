using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    #region Singleton
    private static EventManager instance;
    public static EventManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogWarning("EventManager is Null");
            }
            return instance;
        }
    }

    private bool CheckInstance()
    {
        if (instance == null)
        {
            instance = (EventManager)this;
            return true;
        }
        else if (Instance == this)
        {
            return true;
        }

        Destroy(this);
        return false;
    }
    #endregion

    private List<GameObject> gimmickList = new List<GameObject>();
    private List<GameObject> itemList = new List<GameObject>();


    void Awake()
    {
        CheckInstance();
        SetGimmickItemObject();
    }

    void Start()
    {
        
    }

    void Update()
    {
        //
        if (Input.GetKeyDown(KeyCode.F5))
        {
            UpdateEvent();
        }
    }

    /// <summary>
    /// フラグのチェックと更新
    /// </summary>
    public void UpdateEvent()
    {
        foreach(GameObject obj in gimmickList)
        {
            if (obj != null)
            {
                if (obj.GetComponent<EventBase>() != null)
                {
                    obj.GetComponent<EventBase>().CheckFlag();
                }
            }
        }
        foreach (GameObject obj in itemList)
        {
            if (obj != null)
            {
                if (obj.GetComponent<EventBase>() != null)
                {
                    obj.GetComponent<EventBase>().CheckFlag();
                }
            }            
        }
    }

    void SetGimmickItemObject()
    {
        GameObject[] GimmickObjects = GameObject.FindGameObjectsWithTag("Gimmick");
        foreach (var obj in GimmickObjects)
        {
            if (!gimmickList.Contains(obj))
            {
                gimmickList.Add(obj);
            }
        }
        GameObject[] ItemObjects = GameObject.FindGameObjectsWithTag("Item");
        foreach (var obj in ItemObjects)
        {
            if (!itemList.Contains(obj))
            {
                itemList.Add(obj);
            }
        }
    }
}

/// <summary>
/// キャラの調べる反応させる関数
/// </summary>
public interface ICheckable
{
    void Check();
}

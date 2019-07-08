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

    [SerializeField]
    private GameObject[] gimmickObjects;
    [SerializeField]
    private GameObject[] itemObjects;


    void Awake()
    {
        CheckInstance();
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
        foreach(GameObject obj in gimmickObjects)
        {
            if (obj != null)
            {
                if (obj.GetComponent<EventBase>() != null)
                {
                    obj.GetComponent<EventBase>().CheckFlag();
                }
            }
        }
        foreach (GameObject obj in itemObjects)
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
}

/// <summary>
/// キャラの調べる反応させる関数
/// </summary>
public interface ICheckable
{
    void Check();
}

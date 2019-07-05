using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerManager : MonoBehaviour
{
    #region Singleton
    private static PlayerManager instance;
    public static PlayerManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogWarning("PlayerManager is Null");
            }
            return instance;
        }
    }

    private bool CheckInstance()
    {
        if (instance == null)
        {
            instance = (PlayerManager)this;
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

    private GameObject perrault = null;
    public GameObject Pero
    {
        get { return perrault; }
    }

    private GameObject fran = null;
    public GameObject Fran
    {
        get { return fran; }
    }

    void Awake()
    {
        CheckInstance();
        FindPerraultFran();
        SwitchPlayerMode(FlagManager.Instance.IsPast);
    }

    // ペローやフランの検索
    void FindPerraultFran()
    {
        if (GameObject.Find("Perrault"))
            perrault = GameObject.Find("Perrault");
        if (GameObject.Find("Fran"))
            fran = GameObject.Find("Fran");
        if (perrault != null && fran != null)
            Debug.Log("FindSucces");
        else
            Debug.LogWarning("FindFaild");
    }

    public void SwitchPlayerMode(bool past)
    {
        if (past)
        {
            perrault.GetComponent<PerraultMove>().enabled = false;
            fran.GetComponent<FranMove>().enabled = true;
        }
        else
        {
            perrault.GetComponent<PerraultMove>().enabled = true;
            fran.GetComponent<FranMove>().enabled = false;
        }
    }

    //  プレイヤーの座標取得
    public Vector2 GetPlayerPos()
    {
        Vector2 vec2;
        if (!FlagManager.Instance.IsPast)
        {
            vec2 = perrault.transform.position;
        }
        else
        {
            vec2 = fran.transform.position;
        }
        return vec2;
    }
    

}
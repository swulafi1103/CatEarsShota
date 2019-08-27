using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelPuzzleControl : MonoBehaviour
{
    #region Singleton
    private static PanelPuzzleControl instance;
    public static PanelPuzzleControl Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogWarning("PanelPuzzleControl is Null");
            }
            return instance;
        }
    }

    private bool CheckInstance()
    {
        if (instance == null)
        {
            instance = (PanelPuzzleControl)this;
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


    PanelPuzzle panelPuzzle;
    GameObject tutoPanel;

    bool isOnce = true;

    // Start is called before the first frame update
    void Start()
    {
        panelPuzzle = GetComponentInChildren<PanelPuzzle>();
        panelPuzzle.gameObject.SetActive(false);
        tutoPanel = transform.GetChild(1).gameObject;
        tutoPanel.SetActive(false);
        isOnce = true;
    }

    private void Awake() {
        CheckInstance();
    }

    // Update is called once per frame
    void Update()
    {
        TutoEnd();
    }
    
    /// <summary>
    /// パネルの起動判定
    /// </summary>
    public void StartPanelPuzzle()
    {
        if (isOnce)
        {
            tutoPanel.SetActive(true);
        }
        else
        {
            panelPuzzle.gameObject.SetActive(true);
            panelPuzzle.PushStart();
        }
        FlagManager.Instance.IsEventing = true;
    }

    void TutoEnd()
    {
        if (!tutoPanel.activeSelf) return;
        if (!Input.GetKeyDown(KeyCode.Return)) return;
        tutoPanel.SetActive(false);
        isOnce = false;

        panelPuzzle.gameObject.SetActive(true);
        panelPuzzle.PushStart();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelPuzzleControl : MonoBehaviour
{
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

    static PanelPuzzleControl instance;
    public static PanelPuzzleControl Instance {
        get { return instance; }
    }
    private void Awake() {
        if (instance == null) instance = this;

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

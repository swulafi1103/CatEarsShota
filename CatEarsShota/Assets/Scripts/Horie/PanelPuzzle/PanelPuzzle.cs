using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelPuzzle : MonoBehaviour
{
    [SerializeField]
    Sprite[] PanelImage = new Sprite[6];

    int[] startPos = new int[6] { 2, 0, 5, 1, 4, 3 };
    int[] panelNum = new int[6] { 2, 0, 5, 1, 4, 3 };
    int[] endPos = new int[6] { 1, 2, 3, 0, 5, 4 };

    int carsorSelectNum = 0;

    Image[] panels = new Image[6];
    Vector3[] movePos = new Vector3[6];
    RectTransform selectCarsor;
    

    enum GameState
    {
        wait,
        Start,
        PanelSeted,
        Conprete
    }

    GameState _gameState;

    // Start is called before the first frame update
    void Start()
    {
        SetData();
    }

    // Update is called once per frame
    void Update()
    {
        switch (_gameState) {
            case GameState.wait:
                CheckStart();
                break;
            case GameState.Start:
                MoveCarsor();
                PushAKey();
                break;
            case GameState.PanelSeted:
                break;
            case GameState.Conprete:
                break;
        }

    }

    void SetData()
    {
        for(int i = 0; i < 6; i++)
        {
            panels[i] = transform.GetChild(i + 1).GetComponent<Image>();
            movePos[i] = panels[i].transform.localPosition;
            
        }

        _gameState = GameState.wait;
        carsorSelectNum = 0;
        selectCarsor = transform.GetChild(7).GetComponent<RectTransform>();
        selectCarsor.localPosition = movePos[0];

        SetStartPanel();
    }

    /// <summary>
    /// パズル初期化
    /// </summary>
    void SetStartPanel()
    {
        panelNum = startPos;
        for(int i = 0; i < 6; i++)
        {
            if (panelNum[i] == 0)
            {
                panels[i].enabled = false;
            }
            else
            {
                panels[i].sprite = PanelImage[panelNum[i] - 1];
            }
        }
    }

    /// <summary>
    /// 表示、非表示
    /// </summary>
    void CheckStart() {
        //flag取得処理

        _gameState = GameState.Start;
    }

    /// <summary>
    /// 矢印入力
    /// </summary>
    void MoveCarsor() {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            SelectMove(-3);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            SelectMove(3);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            SelectMove(-1);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            SelectMove(1);
        }
    }

    /// <summary>
    /// セレクトの箱移動
    /// </summary>
    /// <param name="moves"></param>
    void SelectMove(int moves) {
        int next = carsorSelectNum + moves;
        if (next < 0 || 6 <= next) return;
        if (panelNum[next] == 0) {
            next += moves;
            if (next < 0 || 6 <= next) return;
        }
        carsorSelectNum = next;
        selectCarsor.localPosition = movePos[carsorSelectNum];
    }

    /// <summary>
    /// 5揃いクリア判定
    /// </summary>
    void CheckClear() {
        for(int i = 0; i < panelNum.Length; i++) {
            if (panelNum[i] != endPos[i]) return;
        }

        _gameState = GameState.PanelSeted;
        selectCarsor.gameObject.SetActive(false);
        Debug.Log("5 Seted");
    }

    /// <summary>
    /// AKey
    /// </summary>
    void PushAKey() {
        if (!Input.GetKeyDown(KeyCode.A)) return;
        SwitchingPanel();
    }

    /// <summary>
    /// パネル移動
    /// </summary>
    void SwitchingPanel() {
        int[] movevec = new int[4] { -3, -1, 1, 3 };

        for(int i = 0; i < movevec.Length; i++) {

            int next = carsorSelectNum + movevec[i];
            if (movevec[i] == 1 && next == 3) continue;
            if (movevec[i] == -1 && next == 2) continue;
            if (next < 0 || 6 <= next) continue;
            if (panelNum[next] != 0) continue;

            panels[next].sprite = panels[carsorSelectNum].sprite;
            panels[next].enabled = true;
            panels[carsorSelectNum].enabled = false;

            panelNum[next] = panelNum[carsorSelectNum];
            panelNum[carsorSelectNum] = 0;

            carsorSelectNum = next;
            selectCarsor.localPosition = movePos[carsorSelectNum];
            CheckClear();
            return;
        }
    }
}

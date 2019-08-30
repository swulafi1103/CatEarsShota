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
    
    bool panelAct = false;
    bool ItemUIAct = false;
    
    Image DKeyImage;

    enum GameState
    {
        wait,
        Start,
        PanelSeted,
        Conprete
    }

    GameState _gameState = GameState.wait;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (_gameState) {
            case GameState.wait:
                break;
            case GameState.Start:
                MoveCarsor();
                PushAKey();
                PushSKey();
                PushXkey();
                break;
            case GameState.PanelSeted:
                PushXkey();
                PushDkey();
                SelectPiece();
                break;
            case GameState.Conprete:
                PushXkey();
                break;
        }

    }

    /// <summary>
    /// データ初期化
    /// </summary>
    void SetData()
    {
        panelAct = false;
        ItemUIAct = false;
        for (int i = 0; i < 6; i++)
        {
            panels[i] = transform.GetChild(i + 1).GetComponent<Image>();
            movePos[i] = panels[i].transform.localPosition;
            
        }

        _gameState = GameState.Start;
        selectCarsor = transform.GetChild(7).GetComponent<RectTransform>();

        DKeyImage = transform.GetChild(10).GetComponent<Image>();

        SetStartPanel();
    }

    /// <summary>
    /// パズル初期化
    /// </summary>
    void SetStartPanel()
    {
        for(int i = 0; i < 6; i++)
        {
            panelNum[i] = startPos[i];
            if (panelNum[i] == 0)
            {
                panels[i].enabled = false;
            }
            else
            {
                panels[i].enabled = true;
                panels[i].sprite = PanelImage[panelNum[i] - 1];
            }
        }
        carsorSelectNum = 0;
        selectCarsor.localPosition = movePos[carsorSelectNum];
        DKeyImage.enabled = false;
    }
    
    /// <summary>
    /// 呼び出し
    /// </summary>
    public void PushStart()
    {
        if (panelAct) return;

        switch (_gameState)
        {
            case GameState.wait:
                SetData();
                break;
            case GameState.PanelSeted:
                SetDIcon();
                break;
        }
        panelAct = true;
    }

    void SetDIcon()
    {
        bool haves = ItemManager.Instance.IsGet(ItemManager.ItemNum.Ilust_Piece);
        if (!haves) return;
        DKeyImage.enabled = true;
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
        SetDIcon();
    }

    /// <summary>
    /// AKey(In Game)
    /// </summary>
    void PushAKey() {
        if (!panelAct) return;
        if (!Input.GetKeyDown(KeyCode.A)) return;
        SwitchingPanel();
    }


    /// <summary>
    /// SKey
    /// </summary>
    void PushSKey()
    {
        if (!panelAct) return;
        if (!Input.GetKeyDown(KeyCode.S)) return;
        SetStartPanel();
    }

    /// <summary>
    /// XKey
    /// </summary>
    void PushXkey()
    {
        if (!panelAct) return;
        if (!Input.GetKeyDown(KeyCode.X)) return;
        panelAct = false;
        this.gameObject.SetActive(false);
        FlagManager.Instance.IsEventing = false;
    }

    /// <summary>
    /// DKey
    /// </summary>
    void PushDkey()
    {
        if (!panelAct) return;
        if (!DKeyImage.enabled) return;
        if (!Input.GetKeyDown(KeyCode.D)) return;
        ItemManager.Instance.SetEventUI(ItemManager.ItemNum.Ilust_Piece);
        ItemUIAct = true;
    }

    void SelectPiece()
    {
        if (!ItemUIAct) return;
        bool select = ItemManager.Instance.SelectedEventItem(ItemManager.ItemNum.Ilust_Piece);
        if (!select) return;
        ItemUIAct = false;
        for(int i = 0; i < panelNum.Length; i++)
        {
            if (panelNum[i] != 0) continue;
            panels[i].sprite = PanelImage[5];
            panels[i].enabled = true;
            DKeyImage.enabled = false;
            _gameState = GameState.Conprete;
            //フラグ書き換え
            if (EventManager.Instance.PieceGameClearedFunc != null)
                EventManager.Instance.PieceGameClearedFunc.Invoke();
            return;
        }
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

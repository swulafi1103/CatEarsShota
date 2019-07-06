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

    Image[] panels = new Image[6];
    Vector3[] movePos = new Vector3[6];

    enum GameState
    {
        Start,
        Selected,
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
        
    }

    void SetData()
    {
        for(int i = 0; i < 6; i++)
        {
            panels[i] = transform.GetChild(i + 1).GetComponent<Image>();
            movePos[i] = panels[i].transform.localPosition;
            Debug.Log(panels[i].gameObject.name);
            
        }
        _gameState = GameState.Start;
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
}

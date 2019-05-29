using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviour
{
    public GameObject CameraMain;
    public GameObject Door;

    private static readonly KeyCode[] USEKEYS = { KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.LeftArrow, KeyCode.RightArrow };    //  ミニゲームに使うキー配列
    private KeyCode[]   questionCommand;        //  問題のキー配列
    private const int   MISTAKELIMIT = 4;       //  ミス上限値
    private int         mistakeCount = 0;       //  ミスのカウント
    private int         numOrder;               //  解答中のコマンドの並び
    private bool        isMinigame = false;     //  ミニゲーム中か
    private bool        isCountdownEnd = false; //  ミニゲームのカウントダウンは終わったか
    [SerializeField, Range(0.1f, 15f)]
    private float       defaultTimeLimit = 10f; //  ミニゲームの制限時間
    private float       timeLimit;

    [SerializeField]
    private GameObject  discriptionObj;         //  説明用のUI
    [SerializeField]
    private GameObject  miniGameViewObj;        //  ミニゲームのコマンドなどが表示されるUI
    [SerializeField]
    private GameObject  timerObj;               //  タイマーを表示する用
    [SerializeField]
    private Text        countdownObj;           //  カウントダウン用
    [SerializeField]
    private GameObject  mistakeCountObj;        //  失敗回数を表示する用  
    [SerializeField]
    private GameObject  commandParentObj;       //  コマンドの表示用
    [SerializeField]
    private GameObject PlayerMovePerrault;
    [SerializeField]
    private GameObject PlayerMoveFran;             //playerの移動制限解除用
    [SerializeField]
    private GameObject map;                     //mapオブジェクト変更
    [SerializeField]
    private GameObject mainGameMgr;

    [SerializeField]
    private GameObject  textPrefab;             //  コマンド用のPrefab
    [SerializeField]
    private Color       beforeColor;
    [SerializeField]
    private Color       afterColor;
    private Text[]      commandTexts;
    

    //[SerializeField]
    //private Sprite[]    keySprites;           //  問題に並べる画像
    //[SerializeField]
    //private Image[] countDownImage;


    /*
     * プレイヤーが発電機に触れることイベント開始
     * ミニゲームの説明画面とStartボタンが表示される
     * スタートボタンを押された後、カウントダウン開始
     * 問題の表示と制限時間の表示
     * キー入力のチェック
     * 間違えるとコマンドもリセット
     * 一定数のミスで再スタート
     * すべて時間内に打ち終わると成功
     */

    void Start()
    {

    }


    void Update()
    {
        CheckTypingKey();
        //  テスト用

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
            //TouchGenerator();
        //}
    }

    /// <summary>
    /// 発電機を触れたら
    /// </summary>
    public void TouchGenerator()
    {
        discriptionObj.SetActive(true);
        isMinigame = true;
        PlayerMovePerrault.GetComponent<PlayerMoves>().Notmoves = true;
        PlayerMoveFran.GetComponent<PlayerMoves>().Notmoves = true;
    }

    /// <summary>
    /// ミニゲームの開始
    /// </summary>
    public void StartMiniGame()
    {
        mistakeCount = 0;
        countdownObj.color = Color.white;
        discriptionObj.SetActive(false);
        miniGameViewObj.SetActive(true);
        timeLimit = defaultTimeLimit;
        timerObj.GetComponent<Text>().text = timeLimit.ToString("0");
        StartCoroutine(CountDown());
    }


    /// <summary>
    /// 同じ数字が続かないランダムな配列の生成
    /// </summary>
    /// <param name="size"></param>
    void GenerateRandomNums(int size)
    {
        int[] randomNums = new int[size];
        int tmp = -1;
        for (int i = 0; i < size; i++)
        {
            int randNum = UnityEngine.Random.Range(0, USEKEYS.Length);
            if (tmp != randNum)
            {
                tmp = randNum;
                randomNums[i] = randNum;
            }
            else
            {
                while (tmp == randNum)
                {
                    randNum = UnityEngine.Random.Range(0, USEKEYS.Length);
                }
                tmp = randNum;
                randomNums[i] = randNum;
            }
        }
        CreateQuestionCommand(randomNums);
    }

    /// <summary>
    /// 問題となるのコマンドを生成
    /// </summary>
    /// <param name="randomNums"></param>
    void CreateQuestionCommand(int[] randomNums)
    {
        questionCommand = new KeyCode[randomNums.Length];
        if (commandTexts != null)
        {
            foreach (var obj in commandTexts)
            {
                Destroy(obj.gameObject);
            }
        }
        commandTexts = new Text[randomNums.Length];
        for (int i = 0; i < randomNums.Length; i++)
        {
            questionCommand[i] = USEKEYS[randomNums[i]];
            GameObject obj = Instantiate(textPrefab, commandParentObj.transform);
            obj.GetComponent<Text>().color = beforeColor;
            commandTexts[i] = obj.GetComponent<Text>();
            commandTexts[i].text = ChangeKeyCodeString(questionCommand[i]);
        }
    }

    /// <summary>
    /// タイピングしたキーの判定
    /// </summary>
    void CheckTypingKey()
    {
        if (isCountdownEnd && isMinigame)
        {
            timeLimit -= Time.deltaTime;
            if (timeLimit <= 0)
            {
                MinigameFaild();
                timeLimit = Math.Max(0, timeLimit);
            }
            timerObj.GetComponent<Text>().text = timeLimit.ToString("0");
        }

        if (Input.anyKeyDown　&& isCountdownEnd)
        {
            foreach (KeyCode code in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(code))
                {
                    if (0 <= Array.IndexOf(USEKEYS, code))
                    {
                        //  正解の処理
                        if (Input.GetKeyDown(questionCommand[numOrder]))
                        {
                            CorrectAnswer();
                        }
                        //  不正解の処理
                        else if (!Input.GetKeyDown(questionCommand[numOrder]))
                        {
                            IncorrectAnswer();
                        }
                        //  文字の色変え
                        for (int i = 0; i < numOrder; i++)
                        {
                            commandTexts[i].GetComponent<Text>().color = afterColor;
                        }
                    }
                }
            }
        }
    }


    /// <summary>
    /// 正解時の処理
    /// </summary>
    void CorrectAnswer()
    {
        numOrder++;
        if (numOrder >= questionCommand.Length)
        {
            MinigameClear();
        }
    }

    /// <summary>
    /// 不正解の時の処理
    /// </summary>
    void IncorrectAnswer()
    {
        mistakeCount++;
        mistakeCountObj.GetComponent<Text>().text = mistakeCount.ToString();
        StartCoroutine(ShakeObject(0.2f, miniGameViewObj));
        if (mistakeCount >= MISTAKELIMIT)
        {
            MinigameFaild();
            return;
        }
        timeLimit -= 1;
        numOrder = 0;
        GenerateRandomNums(5);
    }

    /// <summary>
    /// ハッキングに成功
    /// </summary>
    void MinigameClear()
    {
        StartCoroutine(DisplayClearText());
        map.GetComponent<MapStatus>().MapObjectState[2] = true;
        map.GetComponent<MapStatus>().MapObjectState[1] = true;
        PlayerMoveFran.GetComponent<PlayerMoves>().Notmoves = false;
        mainGameMgr.GetComponent<PlayerPositionSync>().PastMode = false;
        PlayerMovePerrault.GetComponent<PlayerMoves>().Notmoves = false;


    }

    /// <summary>
    /// ハッキングに失敗
    /// </summary>
    void MinigameFaild()
    {

        PlayerMoveFran.GetComponent<PlayerMoves>().Notmoves = false;
        mainGameMgr.GetComponent<PlayerPositionSync>().PastMode = false;
        PlayerMovePerrault.GetComponent<PlayerMoves>().Notmoves = false;
        StartCoroutine(DisplayFaildText());
    }

    /// <summary>
    /// KeyCodeを文字列に変換する
    /// </summary>
    /// <param name="keyCode"></param>
    /// <returns></returns>
    private string ChangeKeyCodeString(KeyCode keyCode)
    {
        switch (keyCode)
        {
            case KeyCode.A:
                return "Ａ";
            case KeyCode.S:
                return "Ｓ";
            case KeyCode.D:
                return "Ｄ";
            case KeyCode.RightArrow:
                return "→";
            case KeyCode.LeftArrow:
                return "←";
            default:
                //  でてたらヤバイ
                Debug.Log("?の文字:" + keyCode);
                return "?";
        }
    }

    /// <summary>
    /// ミニゲームの開始時に表示
    /// </summary>
    IEnumerator CountDown()
    {
        Text txtObj = countdownObj;
        yield return new WaitForSeconds(1f);
        txtObj.enabled = true;
        txtObj.text = "3";
        yield return new WaitForSeconds(0.5f);
        txtObj.text = "2";
        yield return new WaitForSeconds(0.5f);
        txtObj.text = "1";
        yield return new WaitForSeconds(0.5f);
        txtObj.text = "START";
        isCountdownEnd = true;
        yield return new WaitForSeconds(0.5f);
        txtObj.enabled = false;
        txtObj.text = "";
        GenerateRandomNums(5);
        yield break;
    }

    /// <summary>
    /// クリア時の表示
    /// </summary>
    /// <returns>The clear text.</returns>
    IEnumerator DisplayClearText()
    {
        Text txtObj = countdownObj;
        txtObj.enabled = true;
        if (commandTexts != null)
        {
            foreach (var obj in commandTexts)
            {
                obj.text = "";
                yield return null;
            }
        }
        countdownObj.text = "CLEAR!!";
        isMinigame = false;
        isCountdownEnd = false;
        yield return new WaitForSeconds(2);
        countdownObj.text = "";
        miniGameViewObj.SetActive(false);
        numOrder = 0;
        mistakeCount = 0;
        mistakeCountObj.GetComponent<Text>().text = mistakeCount.ToString();
        CameraMain.GetComponent<Camera>().PastMode = false;
        Door.GetComponent<MapStatus>().MapObjectState[2] = true;
        yield break;
    }

    IEnumerator DisplayFaildText()
    {
        Text txtObj = countdownObj;
        txtObj.enabled = true;
        if (commandTexts != null)
        {
            foreach (var obj in commandTexts)
            {
                obj.text = "";
                yield return null;
            }
        }
        countdownObj.text = "ERROR";
        countdownObj.color = Color.red;
        isMinigame = false;
        isCountdownEnd = false;
        yield return new WaitForSeconds(2);
        countdownObj.text = "";
        miniGameViewObj.SetActive(false);
        mistakeCount = 0;
        mistakeCountObj.GetComponent<Text>().text = mistakeCount.ToString();
        yield break;
    }

    IEnumerator ShakeObject(float time, GameObject obj)
    {
        const float shakePower = 0.2f;
        Vector3 pos = obj.transform.position;
        while (time >= 0)
        {
            Vector3 randVec3 = UnityEngine.Random.insideUnitSphere * shakePower;
            randVec3.z = 0;
            obj.transform.position += pos + randVec3;
            time -= Time.deltaTime;
            yield return null;
            obj.transform.position = pos;
        }
        obj.transform.position = pos;
        yield break;
    }

}

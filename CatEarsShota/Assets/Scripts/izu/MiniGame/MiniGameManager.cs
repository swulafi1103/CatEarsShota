using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviour
{
    #region Singleton
    private static MiniGameManager instance;
    public static MiniGameManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogWarning("MiniGameManager is Null");
            }
            return instance;
        }
    }

    private bool CheckInstance()
    {
        if (instance == null)
        {
            instance = (MiniGameManager)this;
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
    private GameObject map;                    //mapオブジェクト変更

    private static readonly KeyCode[] USEKEYS = { KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.LeftArrow, KeyCode.RightArrow };    //  ミニゲームに使うキー配列
    private KeyCode[]   questionCommand;        //  問題のキー配列
    //private const int   MISTAKELIMIT = 4;     //  ミス上限値
    //private int         mistakeCount = 0;     //  ミスのカウント
    private int         numOrder;               //  解答中のコマンドの並び
    private bool        isMinigame = false;     //  ミニゲーム中か
    private bool        isCountdownEnd = false; //  ミニゲームのカウントダウンは終わったか
    private bool        isFadeing = false;      //  フェード中か
    private float       defaultTimeLimit = 20f; //  ミニゲームの制限時間
    private float       timeLimit;
    private float       fadeAlpha = 0;

    private GameObject  discriptionObj;         //  説明用のUI
    private GameObject  miniGameViewObj;        //  ミニゲームのコマンドなどが表示されるUI

    private GameObject  timerObj;               //  タイマーを表示する用
    private Text        countdownObj;           //  カウントダウン用

    //[SerializeField]
    //private GameObject  mistakeCountObj;      //  失敗回数を表示する用
    private GameObject  commandParentObj;       //  コマンドの表示用

    [SerializeField]
    private GameObject[] keyObjects = new GameObject[5];    //  コマンドの画像のPrefab
    private Color       fadeColor = Color.red;
    private GameObject[] commandTexts;


    [SerializeField]
    private GameObject pastDoor;

    void Awake()
    {
        CheckInstance();
        //  必要なObejctの検索、見当たらない場合はLogを出す
        FindNeedObject();
    }

    void Update()
    {
        CheckTypingKey();
        if (isMinigame)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                StartMiniGame();
            }
        }
    }

    void OnGUI()
    {
        if (isFadeing)
        {
            //色と透明度を更新して白テクスチャを描画 .
            fadeColor.a = fadeAlpha;
            GUI.color = fadeColor;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
        }
    }

    void FindNeedObject()
    {
        discriptionObj = transform.GetChild(0).gameObject;
        miniGameViewObj = transform.GetChild(1).gameObject;
        timerObj = transform.Find("MinigameBackGround/TimerBackGround/SecondText").gameObject;
        countdownObj = transform.Find("MinigameBackGround/CountDown").GetComponent<Text>();
        commandParentObj = transform.Find("MinigameBackGround/CommandParent").gameObject;
        string log = "MiniGame Find Succes";
        if (discriptionObj == null)
        {
            log = "MiniGame Find Faild" + ": MissingObj : " + discriptionObj.name;
        }
        if (miniGameViewObj == null)
        {
            log = "MiniGame Find Faild" + ": MissingObj : " + miniGameViewObj.name;
        }
        if (timerObj == null)
        {
            log = "MiniGame Find Faild" + ": MissingObj : " + timerObj.name;
        }
        if (countdownObj == null)
        {
            log = "MiniGame Find Faild" + ": MissingObj : " + countdownObj.name;
        }
        if (commandParentObj == null)
        {
            log = "MiniGame Find Faild" + ": MissingObj : " + commandParentObj.name;
        }
        Debug.Log(log);
    }

    /// <summary>
    /// 発電機を触れたら
    /// </summary>
    public void TouchGenerator()
    {
        discriptionObj.SetActive(true);
        isMinigame = true;
        FlagManager.Instance.IsEventing = true;
    }

    /// <summary>
    /// ミニゲームの開始
    /// </summary>
    public void StartMiniGame()
    {
        //  失敗数でゲームオーバーになるように(処理を変えたためコメントアウト)
        //mistakeCount = 0;
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
        commandTexts = new GameObject[randomNums.Length];
        for (int i = 0; i < randomNums.Length; i++)
        {
            questionCommand[i] = USEKEYS[randomNums[i]];
            switch (questionCommand[i])
            {
                case KeyCode.A:
                    commandTexts[i] = Instantiate(keyObjects[0], commandParentObj.transform);
                    break;
                case KeyCode.S:
                    commandTexts[i] = Instantiate(keyObjects[1], commandParentObj.transform);
                    break;
                case KeyCode.D:
                    commandTexts[i] = Instantiate(keyObjects[2], commandParentObj.transform);
                    break;
                case KeyCode.LeftArrow:
                    commandTexts[i] = Instantiate(keyObjects[3], commandParentObj.transform);
                    break;
                case KeyCode.RightArrow:
                    commandTexts[i] = Instantiate(keyObjects[4], commandParentObj.transform);
                    break;
            }
            commandTexts[i].GetComponent<Image>().SetNativeSize();
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
                            commandTexts[i].GetComponent<ChangePressKey>().ChangeAfterSprite();
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
        //mistakeCount++;
        //mistakeCountObj.GetComponent<Text>().text = mistakeCount.ToString();
        StartCoroutine(ShakeObject(0.2f, miniGameViewObj));
        //if (mistakeCount >= MISTAKELIMIT)
        //{
        //    MinigameFaild();
        //    return;
        //}
        StartCoroutine(DamegeEffect(0.25f));
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
        Fade.Instance.StartFade(0.5f, Color.black, () => MainCamera.Instance.TriggeredVideo(2));
        map.GetComponent<MapStatus>().ChangeColorObj();
        StartCoroutine(ChengeFran());
        //FlagManager.Instance.IsPast = false;
    }

    /// <summary>
    /// ハッキングに失敗
    /// </summary>
    void MinigameFaild()
    {
        FlagManager.Instance.IsPast = true;
        StartCoroutine(DisplayFaildText());
        pastDoor.GetComponent<PlayMinigame>().isOpenMinigame = false;
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

    IEnumerator ChengeFran()
    {
        Fade.Instance.StartFadeInOut(1, Color.white);
        yield break;
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
                Destroy(obj);
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
        //mistakeCount = 0;
        //mistakeCountObj.GetComponent<Text>().text = mistakeCount.ToString();
        map.GetComponent<MapStatus>().MapObjectState[2] = true;
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
                Destroy(obj);
                yield return null;
            }
        }
        countdownObj.text = "ERROR";
        countdownObj.color = Color.red;
        isMinigame = false;
        isCountdownEnd = false;
        isFadeing = true;
        float time = 0;
        while (time <= 0.5f)
        {
            fadeAlpha = Mathf.Lerp(0, 1f, time / 0.5f);
            time += Time.unscaledDeltaTime;
            yield return 0;
        }
        time = 0;
        while (time <= 2)
        {
            fadeAlpha = Mathf.Lerp(1f, 0f, time / 2);
            time += Time.unscaledDeltaTime;
            yield return 0;
        }
        isFadeing = false;
        countdownObj.text = "";
        miniGameViewObj.SetActive(false);
        //mistakeCount = 0;
        //mistakeCountObj.GetComponent<Text>().text = mistakeCount.ToString();
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

    IEnumerator DamegeEffect(float interval)
    {
        isFadeing = true;
        //だんだん明るく
        float time = 0;
        while (time <= interval)
        {
            fadeAlpha = Mathf.Lerp(1f, 0f, time / interval);
            time += Time.unscaledDeltaTime;
            yield return 0;
        }
        isFadeing = false;

        yield break;
    }

}

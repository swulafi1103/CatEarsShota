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
    private GameObject map;     //mapオブジェクト変更
    [SerializeField]
    private Sprite[] countdownImage = new Sprite[4];
    [SerializeField]
    private GameObject[] keyObjects = new GameObject[5];    //  コマンドの画像のPrefab

    private static readonly KeyCode[] USEKEYS = { KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.LeftArrow, KeyCode.RightArrow };    //  ミニゲームに使うキー配列
    private KeyCode[]   questionCommand;        //  問題のキー配列
    private int         questionValue = -1;     //  問題の数
    private int         numOrder;               //  解答中のコマンドの場所
    private bool        isMinigame = false;     //  ミニゲーム中か
    private bool        isCountdownEnd = false; //  ミニゲームのカウントダウンは終わったか
    private bool        isFadeing = false;      //  フェード中か
    private const float defaultTimeLimit = 20f; //  ミニゲームの制限時間
    private float       timeLimit;
    private float       fadeAlpha = 0;
    private GameObject  discriptionObj;         //  説明用のUI
    private GameObject  miniGameViewObj;        //  ミニゲームのコマンドなどが表示されるUI
    private Text        clearText;
    private GameObject  timerObj;               //  タイマーを表示する用
    private Image       countdownObj;           //  カウントダウン用
    private GameObject  commandParentObj;       //  コマンドの表示用
    private GameObject[] commandTexts;
    private Color       fadeColor = Color.red;

    [HideInInspector]
    public GameObject generetor;

    void Awake()
    {
        CheckInstance();
        FindNeedObject();
    }

    void Update()
    {
        CheckTypingKey();
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
        countdownObj = transform.Find("MinigameBackGround/CountDown").GetComponent<Image>();
        clearText= transform.Find("MinigameBackGround/ClearText").GetComponent<Text>();
        commandParentObj = transform.Find("MinigameBackGround/CommandParent").gameObject;
    }

    /// <summary>発電機を触れたら</summary>
    public void TouchGenerator(int value)
    {
        SetQustionValue(value);
        StartCoroutine(SetTuto());
    }

    private void SetQustionValue(int value)
    {
        switch (value)
        {
            case 0:
                questionValue = 5;
                break;
            case 1:
                questionValue = 7;
                break;
            case 2:
                questionValue = 9;
                break;
        }
    }


    //  タイピングゲームの説明とUIのアニメーション
    IEnumerator SetTuto() {
        isMinigame = true;
        FlagManager.Instance.IsEventing = true;
        discriptionObj.SetActive(true);
        discriptionObj.transform.localScale = new Vector3(1, 0, 1);

        float animTime = 0.2f;
        float time = 0;

        while (time <= animTime) {
            discriptionObj.transform.localScale = new Vector3(1, time / animTime, 1);
            time += Time.unscaledDeltaTime;
            yield return null;
        }
        discriptionObj.transform.localScale = Vector3.one;

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.A));

        time = 0;

        while (time <= animTime) {
            discriptionObj.transform.localScale = new Vector3(1, 1 - (time / animTime), 1);
            time += Time.unscaledDeltaTime;
            yield return null;
        }
        discriptionObj.transform.localScale = new Vector3(1, 0, 1);
        StartMiniGame();

        yield break;
    }

    /// <summary>ミニゲームの開始</summary>
    public void StartMiniGame()
    {
        discriptionObj.SetActive(false);
        miniGameViewObj.SetActive(true);
        timeLimit = defaultTimeLimit;
        timerObj.GetComponent<Text>().text = timeLimit.ToString();
        StartCoroutine(CountDown());
    }

    /// <summary>同じ数字が続かないランダムな配列の生成</summary>
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

    /// <summary>問題となるのコマンドを生成</summary>
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

    /// <summary>タイピングしたキーの判定</summary>
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

    /// <summary>正解時の処理</summary>
    void CorrectAnswer()
    {
        numOrder++;
        if (numOrder >= questionCommand.Length)
        {
            MinigameClear();
        }
    }

    bool isDameged = false;

    /// <summary>不正解の時の処理</summary>
    void IncorrectAnswer()
    {
        if (isDameged)
        {
            return;
        }
        StartCoroutine(ShakeObject(0.2f, miniGameViewObj));
        StartCoroutine(DamegeEffect(0.25f));
        timeLimit -= 1;
        numOrder = 0;
        GenerateRandomNums(questionValue);
    }

    /// <summary>成功時</summary>
    void MinigameClear()
    {
        StartCoroutine(DisplayClearText());
        //  マップ１のミニゲーム１のクリアフラグSet
        switch (questionValue)
        {
            case 5:     //  Map1のときの処理
                generetor.GetComponent<PlayMinigame>().CompleteGimmick();
                FlagManager.Instance.SetGimmickFlag(GimmickFlag.G_09_Minigame1_0);
                generetor.GetComponent<PlayMinigame>().MiniGameClear();
                break;
            case 7:     //  Map2の扉１の処理
                Debug.Log("タイピングゲーム2個目、フラグのセットが未完");
                break;
            case 9:     //  Map1の扉２の処理
                Debug.Log("タイピングゲーム3個目、フラグのセットが未完");
                break;
        }
    }

    /// <summary>ハッキングに失敗時</summary>
    void MinigameFaild()
    {
        FlagManager.Instance.IsEventing = false;
        StartCoroutine(DisplayFaildText());
        generetor.GetComponent<PlayMinigame>().isOpenMinigame = false;
    }

    IEnumerator ChangeFran()
    {
        Fade.Instance.StartFadeInOut(1, Color.white);
        yield break;
    }

    /// <summary>ミニゲームの開始時に表示</summary>
    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(1f);
        countdownObj.enabled = true;
        countdownObj.sprite = countdownImage[0];
        countdownObj.SetNativeSize();
        yield return new WaitForSeconds(0.5f);
        countdownObj.sprite = countdownImage[1];
        yield return new WaitForSeconds(0.5f);
        countdownObj.sprite = countdownImage[2];
        yield return new WaitForSeconds(0.5f);
        countdownObj.sprite = countdownImage[3];
        countdownObj.SetNativeSize();
        isCountdownEnd = true;
        yield return new WaitForSeconds(0.5f);
        countdownObj.enabled = false;
        GenerateRandomNums(questionValue);
        yield break;
    }

    /// <summary>クリア時の表示</summary>
    /// <returns>The clear text</returns>
    IEnumerator DisplayClearText()
    {
        clearText.enabled = true;
        if (commandTexts != null)
        {
            foreach (var obj in commandTexts)
            {
                Destroy(obj);
                yield return null;
            }
        }
        clearText.text = "CLEAR!!";
        isMinigame = false;
        isCountdownEnd = false;
        yield return new WaitForSeconds(2);
        clearText.text = "";
        clearText.enabled = false;
        miniGameViewObj.SetActive(false);
        numOrder = 0;
        FlagManager.Instance.IsEventing = false;
        yield break;
    }

    IEnumerator DisplayFaildText()
    {
        clearText.enabled = true;
        if (commandTexts != null)
        {
            foreach (var obj in commandTexts)
            {
                Destroy(obj);
                yield return null;
            }
        }
        clearText.text = "ERROR";
        clearText.color = Color.red;
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
        clearText.text = "";
        clearText.enabled = false;
        miniGameViewObj.SetActive(false);
        clearText.color = Color.white;
        yield break;
    }

    IEnumerator ShakeObject(float time, GameObject obj)
    {
        isDameged = true;
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
        isDameged = false;
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

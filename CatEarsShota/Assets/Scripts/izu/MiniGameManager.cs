using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviour
{
    private static readonly KeyCode[] USEKEYS = { KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.LeftArrow, KeyCode.RightArrow };    //  ミニゲームに使うキー配列
    private KeyCode[]   questionCommand;         //  問題のキー配列
    private const int   MISTAKELIMIT = 4;       //  ミス上限値
    private int         mistakeCount = 0;       //  ミスのカウント
    private int         numOrder;               //  解答中のコマンドの並び
    [SerializeField, Range(0.1f, 15f)]
    private float       timeLimit = 10f;        //  ミニゲームの制限時間

    [SerializeField]
    private Canvas      miniGameCanvas;
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
     * すべて時間内に打ち終わると
     */
    
    void Start()
    {
        StartCoroutine(CountDown());
    }


    void Update()
    {
        CheckTypingKey();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateRandomNums(3);
        }
    }

    /// <summary>
    /// ミニゲームの開始時に表示
    /// </summary>
    IEnumerator CountDown()
    {
        Text txtObj = miniGameCanvas.transform.GetChild(0).gameObject.GetComponent<Text>();
        yield return new WaitForSeconds(1f);
        txtObj.enabled = true;
        txtObj.text = "3";
        yield return new WaitForSeconds(0.5f);
        txtObj.text = "2";
        yield return new WaitForSeconds(0.5f);
        txtObj.text = "1";
        yield return new WaitForSeconds(0.5f);
        txtObj.text = "START";
        yield return new WaitForSeconds(0.5f);
        txtObj.enabled = false;
        GenerateRandomNums(5);
        yield break;
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
        //DrawUI(randomNums);
    }

    /// <summary>
    /// 問題となるのコマンドを生成
    /// </summary>
    /// <param name="randomNums"></param>
    private void CreateQuestionCommand(int[] randomNums)
    {
        questionCommand = new KeyCode[randomNums.Length];
        string commandText = "";
        for (int i = 0; i < randomNums.Length; i++)
        {
            questionCommand[i] = USEKEYS[randomNums[i]];
            commandText += ChangeKeyCodeString(questionCommand[i]);
            //  最後の文字ではなかったら
            if (i < randomNums.Length - 1)
            {
                //  間をあけるために空白も追加する
                commandText += " ";
            }
        }
        miniGameCanvas.transform.GetChild(1).GetComponent<Text>().text = commandText;
    }

    /// <summary>
    /// UIの描画
    /// </summary>
    /// <param name="randomNums"></param>
    private void DrawUI(int[] randomNums)
    {
        for (int i = 0; i < randomNums.Length ; i++)
        {            
            Debug.Log("Num = " + randomNums[i]);
        }
    }

    /// <summary>
    /// タイピングしたキーの判定
    /// </summary>
    void CheckTypingKey()
    {
        if (Input.anyKeyDown)
        {
            foreach (var key in USEKEYS)
            {
                if (Input.GetKeyDown(key))
                {
                    Debug.Log(key);
                    CorrectAnswer();
                    return;
                }
            }
            //Debug.Log("使わないキー");
        }
    }


    /// <summary>
    /// 正解時の処理
    /// </summary>
    private void CorrectAnswer()
    {
        
    }

    /// <summary>
    /// 不正解の時の処理
    /// </summary>
    private void IncorrectAnswer()
    {
        GenerateRandomNums(5);
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

}

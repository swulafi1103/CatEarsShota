using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviour
{
    [SerializeField, Range(0.1f, 15f)]
    private float timeLimit = 10f;          //  ミニゲームの制限時間
    private const int MISTAKECOUNT = 4;     //  失敗カウント
    private static readonly KeyCode[] USEKEY = { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.LeftArrow, KeyCode.RightArrow };    //  ミニゲームに使うキー配列
    private KeyCode[] qustionKey;

    [SerializeField]
    private Canvas miniGameCanvas;
    //[SerializeField]
    //private Image[] countDownImage;

    void Start()
    {
        StartCoroutine(CountDown());
        GenerateRandomNums(5);
    }


    void Update()
    {
        CheckTypingKey();
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
            int randNum = UnityEngine.Random.Range(0, size + 1);
            if (tmp != randNum)
            {
                tmp = randNum;
                randomNums[i] = randNum;
            }
            else
            {
                while (tmp == randNum)
                {
                    randNum = UnityEngine.Random.Range(0, size + 1);
                }
                tmp = randNum;
                randomNums[i] = randNum;
            }
        }
        CreateQuesAry(randomNums);
        DrawUI(randomNums);
    }

    /// <summary>
    /// 問題となるのキー配列を生成
    /// </summary>
    /// <param name="randomNums"></param>
    private void CreateQuesAry(int[] randomNums)
    {
        qustionKey = new KeyCode[randomNums.Length];
        for (int i = 0; i < randomNums.Length; i++)
        {
            qustionKey[i] = USEKEY[randomNums[i]];
        }
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
            for (int i = 0; i < USEKEY.Length; i++)
            {
                if (Input.GetKeyDown(USEKEY[i]))
                {
                    Debug.Log(USEKEY[i]);
                    return;
                }
            }
            Debug.Log("使わないキー");
        }
    }

}

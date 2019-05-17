using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    //[SerializeField, Range(0.1f, 15f)]
    //private float timeLimit = 10f;
    [SerializeField]
    private KeyCode[] targetKeys = new KeyCode[6];

    void Start()
    {
        GenerateRandomNums(5);
    }


    void Update()
    {
        CheckTypingKey();
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
                    tmp = UnityEngine.Random.Range(0, size + 1);
                }
                randomNums[i] = randNum;
            }
        }
        DrawUI(randomNums);
    }


    /// <summary>
    /// UIの描画
    /// </summary>
    /// <param name="rands"></param>
    private void DrawUI(int[] rands)
    {
        
    }


    /// <summary>
    /// タイピングしたキーの判定
    /// </summary>
    void CheckTypingKey()
    {
        for (int i = 0; i < targetKeys.Length; i++)
        {
            if (Input.GetKeyDown(targetKeys[i]))
            {
                Debug.Log(targetKeys[i]);
            }
        }
    }

}

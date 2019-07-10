using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    #region Singleton
    private static Fade instance;
    public static Fade Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogWarning("Fade is Null");
            }
            return instance;
        }
    }

    private bool CheckInstance()
    {
        if (instance == null)
        {
            instance = (Fade)this;
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

    private bool startfadeInOut = false;
    private bool fading = false;
    private bool fadeswitch = false;
    private int localcount = 0;

    private GameObject RedEffect;
    private GameObject FadeScreen;

    private void Awake()
    {
        CheckInstance();
    }

    void Start()
    {
        FadeScreen = transform.GetChild(0).gameObject;
        RedEffect = transform.GetChild(1).gameObject;
    }

    void Update()
    {
        if(startfadeInOut && !fading && localcount>0)
        {
            FadeInOut(RedEffect, 1.5f);
        }
        else if(startfadeInOut && !fading)
        {
            startfadeInOut = false;
            FlagManager.Instance.IsEventing = false;
        }
    }

    public void StartFade(float time,Color newcolor) //色と実行時間だけ入力
    {
        if (!fading)
        {
            fading = true;
            FlagManager.Instance.IsEventing = true;
            StartCoroutine(fadeprocess(FadeScreen,time,newcolor));
        }
    }
    public void StartFadeInOut(float time, Color newcolor)　//赤色のエフェクトの専用フェイド
    {
        if (!fading)
        {
            fading = true;
            FlagManager.Instance.IsEventing = true;
            StartCoroutine(fadeinoutProcess(FadeScreen, time, newcolor, FlagManager.Instance.ChegeFranPero));
        }
    }
    public void ClearFade(float time, Color newcolor) //エフェクトとフェイド画像を同時に透明化
    {
        if (!fading)
        {
            fading = true;
            FlagManager.Instance.IsEventing = true;
            StartCoroutine(fadeprocess(FadeScreen, time, newcolor));
            StartCoroutine(fadeprocess(RedEffect, time, newcolor));
        }
    }
    public void StartFade(float time, Color newcolor,System.Action task)//フェイド直後にコマンドを入れられる
    {
        if (!fading)
        {
            fading = true;
            FlagManager.Instance.IsEventing = true;
            StartCoroutine(fadeprocess(FadeScreen,time,newcolor,task));
        }
    }
    public void CallFadeIO(int count) //赤いエフェクトの実行（実行繰り返しの回数）
    {
        startfadeInOut = true;
        FlagManager.Instance.IsEventing = true;
        localcount = count;
    }
    private void FadeInOut(GameObject target,float time)
    {
        switch (fadeswitch)
        {
            case true:
                Color tmpClear = Color.clear;
                StartFade(time, tmpClear);
                break;
            case false:
                Color tmpWhite = Color.white;
                StartFade(time, tmpWhite);
                break;
        }
        fadeswitch = !fadeswitch;
    }
    IEnumerator fadeprocess(GameObject target,float time, Color newcolor, System.Action task=null)
    {
        float countTime = 0;

        while (countTime < time)
        {
            target.GetComponent<Image>().color = Color.Lerp(target.GetComponent<Image>().color, newcolor, countTime / time);
            countTime += Time.unscaledDeltaTime;
            yield return null;
        }
        target.GetComponent<Image>().color = Color.Lerp(target.GetComponent<Image>().color, newcolor, 1);
        if (task != null)
        {
            Debug.Log("Task通過");
            task();
        }        
        fading = false;
        FlagManager.Instance.IsEventing = false;
        yield return null;
    }
    IEnumerator fadeinoutProcess(GameObject target, float time, Color newcolor, System.Action task = null)
    {
        float countTime = 0;

        while (countTime < time)
        {
            target.GetComponent<Image>().color = Color.Lerp(target.GetComponent<Image>().color, newcolor, countTime / time);
            countTime += Time.unscaledDeltaTime;
            yield return null;
        }
        target.GetComponent<Image>().color = Color.Lerp(target.GetComponent<Image>().color, newcolor, 1);
        if (task != null)
        {
            task();
        }
        countTime = 0;
        while (countTime < time)
        {
            target.GetComponent<Image>().color = Color.Lerp(target.GetComponent<Image>().color, Color.clear, countTime / time);
            countTime += Time.unscaledDeltaTime;
            yield return null;
        }
        target.GetComponent<Image>().color = Color.Lerp(target.GetComponent<Image>().color, Color.clear, 0);
        fading = false;
        FlagManager.Instance.IsEventing = false;
        yield return null;
    }
}

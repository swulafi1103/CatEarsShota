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

    public GameObject RedEffect;
    public GameObject FadeScreen;

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

    public void StartFade(float time,Color newcolor)
    {
        if (!fading)
        {
            fading = true;
            FlagManager.Instance.IsEventing = true;
            StartCoroutine(fadeprocess(FadeScreen,time,newcolor));
        }
    }
    public void ClearFade(float time, Color newcolor)
    {
        if (!fading)
        {
            fading = true;
            FlagManager.Instance.IsEventing = true;
            StartCoroutine(fadeprocess(FadeScreen, time, newcolor));
            StartCoroutine(fadeprocess(RedEffect, time, newcolor));
        }
    }
    public void StartFade(float time, Color newcolor,System.Action task)
    {
        if (!fading)
        {
            fading = true;
            FlagManager.Instance.IsEventing = true;
            StartCoroutine(fadeprocess(FadeScreen,time,newcolor,task));
        }
    }
    public void CallFadeIO(int count)
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
        for (float i = 0.0f; i < time; i += 0.1f)
        {
            target.GetComponent<Image>().color = Color.Lerp(target.GetComponent<Image>().color, newcolor, i + 0.1f / time);
            yield return new WaitForSeconds(0.1f);
        }
        if (task != null)
        {
            task();
        }        
        fading = false;
        FlagManager.Instance.IsEventing = false;
        yield return null;
    }
}

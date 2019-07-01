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

    private void Awake()
    {
        CheckInstance();
    }

    void Start()
    {
        
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

    public void StartFade(GameObject target,float time,Color newcolor)
    {
        if (!fading)
        {
            fading = true;
            FlagManager.Instance.IsEventing = true;
            StartCoroutine(fadeprocess(target,time,newcolor));
        }
    }
    public void StartFade(GameObject target1, GameObject target2, float time, Color newcolor)
    {
        if (!fading)
        {
            fading = true;
            FlagManager.Instance.IsEventing = true;
            StartCoroutine(fadeprocess(target1, time, newcolor));
            StartCoroutine(fadeprocess(target2, time, newcolor));
        }
    }
    public void StartFade(GameObject target, float time, Color newcolor,System.Action task)
    {
        if (!fading)
        {
            fading = true;
            FlagManager.Instance.IsEventing = true;
            StartCoroutine(fadeprocess(target,time,newcolor,task));
        }
    }
    public void CallFadeIO(int count)
    {
        startfadeInOut = true;
        FlagManager.Instance.IsEventing = true;
        localcount = count;
    }
    private void FadeInOut(GameObject target, float time)
    {
        switch (fadeswitch)
        {
            case true:
                Color tmpClear = Color.clear;
                StartFade(target, time, tmpClear);
                break;
            case false:
                Color tmpWhite = Color.white;
                StartFade(target, time, tmpWhite);
                break;
        }
        fadeswitch = !fadeswitch;
    }
    IEnumerator fadeprocess(GameObject target, float time, Color newcolor, System.Action task=null)
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

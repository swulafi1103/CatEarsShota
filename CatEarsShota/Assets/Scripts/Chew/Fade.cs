using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public GameObject FlagManager;

    private bool startfadeInOut = false;
    private bool fading = false;
    private bool fadeswitch = false;
    private int localcount = 0;

    public GameObject RedEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(startfadeInOut && !fading && localcount>0)
        {
            FadeInOut(RedEffect, 1.5f);
        }
        else if(startfadeInOut && !fading)
        {
            startfadeInOut = false;
            FlagManager.GetComponent<FlagManager>().isEventing = false;
        }
    }

    public void StartFade(GameObject target,float time,Color newcolor)
    {
        if (!fading)
        {
            fading = true;
            FlagManager.GetComponent<FlagManager>().isEventing = true;
            StartCoroutine(fadeprocess(target,time,newcolor));
        }
    }
    public void StartFade(GameObject target1, GameObject target2, float time, Color newcolor)
    {
        if (!fading)
        {
            fading = true;
            FlagManager.GetComponent<FlagManager>().isEventing = true;
            StartCoroutine(fadeprocess(target1, time, newcolor));
            StartCoroutine(fadeprocess(target2, time, newcolor));
        }
    }
    public void StartFade(GameObject target, float time, Color newcolor,System.Action task)
    {
        if (!fading)
        {
            fading = true;
            FlagManager.GetComponent<FlagManager>().isEventing = true;
            StartCoroutine(fadeprocess(target,time,newcolor,task));
        }
    }
    public void CallFadeIO(int count)
    {
        startfadeInOut = true;
        FlagManager.GetComponent<FlagManager>().isEventing = true;
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
    IEnumerator fadeprocess(GameObject target, float time, Color newcolor, System.Action task = null)
    {
        for (float i = 0.0f; i < time; i += 0.1f)
        {
            target.GetComponent<Image>().color = Color.Lerp(target.GetComponent<Image>().color, newcolor, i + 0.1f / time);
            yield return new WaitForSeconds(0.1f);
        }
        task();
        fading = false;
        FlagManager.GetComponent<FlagManager>().isEventing = false;
        yield return null;
    }
}

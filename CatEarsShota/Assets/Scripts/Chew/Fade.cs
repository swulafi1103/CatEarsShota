using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    private bool fading = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartFade(GameObject target,float time,Color newcolor)
    {
        if (!fading)
        {
            fading = true;
            StartCoroutine(fadeprocess(target,time,newcolor));
        }
    }
    public void StartFade(GameObject target, float time, Color newcolor,System.Action task)
    {
        if (!fading)
        {
            fading = true;
            StartCoroutine(fadeprocess(target,time,newcolor,task));
        }
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
        yield return null;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExcecuteTest : MonoBehaviour
{
    UnityEvent flagEvent = new UnityEvent();

    IEnumerator test;

    private void OnDestroy()
    {
        
    }

    void Start()
    {
        
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            test = testCor();
            StartCoroutine(test);
            Destroy(gameObject, 2f);
            
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            //StopCoroutine(test);
            StopAllCoroutines();
        }


    }


    IEnumerator testCor()
    {
        int i = 0;
        string time = System.DateTime.Now.ToString();
        while (i < 30)
        {
            yield return new WaitForSeconds(0.1f);
            i++;
            Debug.Log(time + ":" + i.ToString());
        }
        yield break;
    }

}

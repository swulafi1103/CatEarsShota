using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//動画を流すとき上と下に黒いバーを付けるる
public class cinematicBar : MonoBehaviour
{ 
    public GameObject Topbar;
    public GameObject Btmbar;

    void Start()
    {
        Topbar.transform.position = new Vector3(0,600f,-5f);
        Btmbar.transform.position = new Vector3(0, -600f, -5f);
        StartCoroutine(ShowBar());
    }

    IEnumerator ShowBar()
    {
        Debug.Log("started");
        for(float i = 0; i < 1; i+=0.01f)
        {
            Topbar.transform.localPosition = Vector3.Lerp(new Vector3(0, 600f, 0f),new Vector3(0, 550f, 0f),i);
            Btmbar.transform.localPosition = Vector3.Lerp(new Vector3(0, -600f, 0f), new Vector3(0, -550f, 0f), i);
            yield return new WaitForSeconds(0.01f);
        }
    }
}

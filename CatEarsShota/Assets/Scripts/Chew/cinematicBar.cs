using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cinematicBar : MonoBehaviour
{
    public GameObject Topbar;
    public GameObject Btmbar;
    // Start is called before the first frame update
    void Start()
    {
        Topbar.transform.position = new Vector3(0,5.5f,90f);
        Btmbar.transform.position = new Vector3(0, -5.5f, 90f);
        StartCoroutine(ShowBar());
    }
    IEnumerator ShowBar()
    {
        Debug.Log("started");
        for(float i = 0; i < 1; i+=0.01f)
        {
            Topbar.transform.localPosition = Vector3.Lerp(new Vector3(0, 5.5f, 90f),new Vector3(0, 5f, 90f),i);
            Btmbar.transform.localPosition = Vector3.Lerp(new Vector3(0, -5.5f, 90f), new Vector3(0, -5f, 90f), i);
            yield return new WaitForSeconds(0.01f);
        }
    }
}

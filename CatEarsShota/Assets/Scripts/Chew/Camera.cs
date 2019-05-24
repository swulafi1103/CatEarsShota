using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Camera : MonoBehaviour
{
    public bool PastMode = false;
    public GameObject PastCam;
    public GameObject Player;
    public GameObject ScreenMask;
    public float RangeToPlayer = 10.0f;
    public float[] CameraLimit = new float[2];

    private bool fading = false;

    private Vector3 rangeToTarget;
    // Start is called before the first frame update
    void Start()
    {
        rangeToTarget = new Vector3(0,0,RangeToPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        if (PastMode)
        {
            PastCam.transform.position = transform.position + new Vector3(40, 0, 0);
        }
        /*if (transform.position.x < CameraLimit[1] && transform.position.x > CameraLimit[0])
            transform.position = Vector3.Lerp(transform.position, Player.transform.position - rangeToTarget, Time.deltaTime);
        else{
            int tmpindex = transform.position.x > CameraLimit[1] ? 1 : 0;
            transform.position = new Vector3(CameraLimit[tmpindex], transform.position.y, transform.position.z);
        }*/
        
    }
    void FixedUpdate()
    {
        if(!fading)
            PastCam.SetActive(PastMode);
    }

    IEnumerator FadeInOut()
    {
        float delta = 0.1f;
        for (int i = 0; i < 20; i++)
        {
            ScreenMask.GetComponent<Image>().color = Vector4.Lerp(Color.clear, Color.black, delta);
            if (i < 10)
                delta += 0.1f;
            else
                delta -= 0.1f;
            yield return new WaitForSeconds(0.01f);
        }
    }
}

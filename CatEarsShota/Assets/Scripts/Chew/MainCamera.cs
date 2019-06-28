using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MainCamera : MonoBehaviour
{
    public GameObject FlagManager;

    private bool Zooming = false;
    [Tooltip("ビデオをプレイする")]
    public bool PlayVideo = false;
    [Tooltip("過去のカメラ")]
    public GameObject PastCam;
    public GameObject Player;
    [Tooltip("ビデオの保存場所")]
    public GameObject ColorVideo;
    [Tooltip("カメラとプレイヤーの距離")]
    public float RangeToPlayer = 10.0f;
    [Tooltip("デフォルトの画面サイズ")]
    public float DefaultScreenSize = 5.0f;
    [Tooltip("カメラの移動範囲制限")]
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
        if (FlagManager.GetComponent<FlagManager>().isPast)
        {
            PastCam.transform.position = transform.position + new Vector3(40, 0, 0);
        }
        if (Player.transform.position.x < CameraLimit[1] && Player.transform.position.x > CameraLimit[0])
            transform.position = Vector3.Lerp(transform.position, Player.transform.position - rangeToTarget, Time.deltaTime);
        else{
            int tmpindex = Player.transform.position.x > CameraLimit[1] ? 0 : 1;
            Vector3 limitpos = new Vector3(CameraLimit[tmpindex], transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position,limitpos, Time.deltaTime);
     
        }
        if (PlayVideo)
        {
            ColorVideo.SetActive(true);
            if (ColorVideo.GetComponent<VideoPlayer>().isPrepared && !ColorVideo.GetComponent<VideoPlayer>().isPlaying)
            {
                ColorVideo.SetActive(false);
                PlayVideo = false;
            }
        }
    }
    void FixedUpdate()
    {
        if(!fading)
            PastCam.SetActive(FlagManager.GetComponent<FlagManager>().isPast);
    }
    public void TriggeredVideo(uint index)
    {
        PlayVideo = true;
        ColorVideo.GetComponent<VideoStorage>().index = index;
    }
    public void T_ChangeFocus(GameObject newtarget)
    {
        if(!Zooming)
        StartCoroutine(changefocus(newtarget));
    }
    public void T_ChangeFocus(GameObject newtarget, float zoomdelay)
    {
        if (!Zooming)
            StartCoroutine(changefocus(newtarget, zoomdelay));
    }
    public void T_ChangeFocus(GameObject newtarget, float zoomdelay, float zoomspeed)
    {
        if (!Zooming)
            StartCoroutine(changefocus(newtarget, zoomdelay, zoomspeed));
    }
    public void T_ChangeFocus(GameObject newtarget, float zoomdelay, float zoomspeed, float zoomsize)
    {
        if (!Zooming)
            StartCoroutine(changefocus(newtarget, zoomdelay, zoomspeed, zoomsize));
    }
    public void T_ChangeFocus(GameObject newtarget, float zoomdelay, float zoomspeed, float zoomsize, float zoompause)
    {
        if (!Zooming)
            StartCoroutine(changefocus(newtarget, zoomdelay, zoomspeed, zoomsize, zoompause));
    }
    IEnumerator changefocus(GameObject newtarget,float zoomdelay = 0.5f,float zoomspeed = 1.0f,float zoomsize= 1.0f,float zoompause=1.0f )
    {
        Player = newtarget;
        yield return new WaitForSeconds(zoomdelay);
        for(float i=0;i<1;i+=0.1f)
        {
            GetComponent<Camera>().orthographicSize = Mathf.Lerp(DefaultScreenSize,DefaultScreenSize-zoomsize,i+0.1f);
            yield return new WaitForSeconds(zoomspeed/10);
        }
        yield return new WaitForSeconds(zoompause);
        
    }
}

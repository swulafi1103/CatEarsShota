using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Camera : MonoBehaviour
{
    public bool PastMode = false;
    public bool PlayVideo = false;
    public GameObject PastCam;
    public GameObject Player;
    public GameObject ColorVideo;
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
        if (transform.position.x < CameraLimit[1] && transform.position.x > CameraLimit[0])
            transform.position = Vector3.Lerp(transform.position, Player.transform.position - rangeToTarget, Time.deltaTime);
        else{
            int tmpindex = transform.position.x > CameraLimit[1] ? 0 : 1;
            transform.position = new Vector3(CameraLimit[tmpindex], transform.position.y, transform.position.z);
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
            PastCam.SetActive(PastMode);
    }
    public void TriggeredVideo()
    {
        PlayVideo = true;
    }
    
}

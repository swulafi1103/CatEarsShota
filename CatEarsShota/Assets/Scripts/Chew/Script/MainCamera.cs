using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;


[DefaultExecutionOrder(-21)]
public class MainCamera : MonoBehaviour
{
    #region Singleton
    private static MainCamera instance;
    public static MainCamera Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogWarning("MainCamera is Null");
            }
            return instance;
        }
    }

    public bool CheckInstance()
    {
        if (instance == null)
        {
            instance = (MainCamera)this;
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

    private double vidoLength;
    public double VideoLength　//映像の長さ
    {
        get { return vidoLength; }
        private set { vidoLength = value; }
    }
    private bool Zooming = false;
    [Tooltip("ビデオをプレイする")]
    public bool PlayVideo = false;
    [Tooltip("過去のカメラ")]
    public GameObject PastCam;
    public GameObject Player;
    public GameObject Fran;
    [Tooltip("ビデオの保存場所")]
    public GameObject ColorVideo;
    [Tooltip("カメラとプレイヤーの距離")]
    public float RangeToPlayer = 10.0f;
    [Tooltip("デフォルトの画面サイズ")]
    public float DefaultScreenSize = 5.0f;
    [Tooltip("カメラの移動範囲制限")]
    public float[] CameraLimit = new float[2];
    public float[] PastCameraLimit = new float[2];

    private bool fading = false;

    private Vector3 rangeToTarget;

    void Awake()
    {
        CheckInstance();
    }

    // Start is called before the first frame update
    void Start()
    {
        rangeToTarget = new Vector3(0, 0, RangeToPlayer);
        ColorVideo.GetComponent<VideoPlayer>().loopPointReached += MovieFinished;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SkipVideo();
        }
        if (FlagManager.Instance.IsPast)
        {
            if (Player.transform.position.x < PastCameraLimit[1] && Player.transform.position.x > CameraLimit[0])
                PastCam.transform.position = Vector3.Lerp(PastCam.transform.position, Fran.transform.position - rangeToTarget, Time.deltaTime);
            else
            {
                int tmpindex = Fran.transform.position.x > PastCameraLimit[1] ? 0 : 1;
                Vector3 limitpos = new Vector3(PastCameraLimit[tmpindex], PastCam.transform.position.y, PastCam.transform.position.z);
                PastCam.transform.position = Vector3.Lerp(PastCam.transform.position, limitpos, Time.deltaTime);
            }
        }
        else
        {
            if (Player.transform.position.x < CameraLimit[1] && Player.transform.position.x > CameraLimit[0])
                transform.position = Vector3.Lerp(transform.position, Player.transform.position - rangeToTarget, Time.deltaTime);
            else
            {
                int tmpindex = Player.transform.position.x > CameraLimit[1] ? 0 : 1;
                Vector3 limitpos = new Vector3(CameraLimit[tmpindex], transform.position.y, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, limitpos, Time.deltaTime);

            }
        }
        if (PlayVideo)
        {
            ColorVideo.SetActive(true);
            VideoLength = ColorVideo.GetComponent<VideoPlayer>().length;
            FlagManager.Instance.IsMovie = true;
            if (ColorVideo.GetComponent<VideoPlayer>().isPrepared && !ColorVideo.GetComponent<VideoPlayer>().isPlaying)
            {
                FlagManager.Instance.IsMovie = false;
                ColorVideo.SetActive(false);
                PlayVideo = false;
            }
        }
    }
    void FixedUpdate()
    {
        Fade.Instance.SwitchCanvasCam(FlagManager.Instance.IsPast);
        PastCam.SetActive(FlagManager.Instance.IsPast);
    }
    public void SkipVideo() //スキップの機能追加
    {
        if (ColorVideo.GetComponent<VideoPlayer>().isPlaying)
        {
            ColorVideo.GetComponent<VideoPlayer>().Stop();
            FlagManager.Instance.IsMovie = false;
            Fade.Instance.StartFade(1f, Color.clear);
            ColorVideo.SetActive(false);
            PlayVideo = false;
        }

    }
    public void TriggeredVideo(uint index)　//動画を放送
    {
        FlagManager.Instance.IsMovie = true;
        //StartCoroutine(FadeInMovie());
        ColorVideo.GetComponent<VideoStorage>().index = index;
        PlayVideo = true;
    }
    //  動画終了時のフェード
    void MovieFinished(VideoPlayer sorce)
    {
        Fade.Instance.StartFade(1f, Color.clear);
        //StartCoroutine(FadeOutMovie());
    }

    public void T_ChangeFocus(GameObject newtarget)　//カメラを映るターゲット
    {
        if (!Zooming)
            StartCoroutine(changefocus(newtarget));
    }
    public void T_ChangeFocus(GameObject newtarget, float zoomdelay)　//遅延時間の追加
    {
        if (!Zooming)
            StartCoroutine(changefocus(newtarget, zoomdelay));
    }
    public void T_ChangeFocus(GameObject newtarget, float zoomdelay, float zoomspeed)　//拡大速度の追加
    {
        if (!Zooming)
            StartCoroutine(changefocus(newtarget, zoomdelay, zoomspeed));
    }
    public void T_ChangeFocus(GameObject newtarget, float zoomdelay, float zoomspeed, float zoomsize)　//拡大倍数の追加
    {
        if (!Zooming)
            StartCoroutine(changefocus(newtarget, zoomdelay, zoomspeed, zoomsize));
    }
    public void T_ChangeFocus(GameObject newtarget, float zoomdelay, float zoomspeed, float zoomsize, float zoompause)　//拡大後止まる時間の追加
    {
        if (!Zooming)
            StartCoroutine(changefocus(newtarget, zoomdelay, zoomspeed, zoomsize, zoompause));
    }
    //  動画のフェードイン・アウト
    IEnumerator FadeInMovie()
    {
        float time = 0;
        while (time < 1)
        {
            yield return null;
            ColorVideo.GetComponent<VideoPlayer>().targetCameraAlpha = Mathf.Lerp(0, 1, time / 1);
            time += Time.unscaledDeltaTime;
        }
        ColorVideo.GetComponent<VideoPlayer>().targetCameraAlpha = 1;
        yield break;
    }
    IEnumerator FadeOutMovie()
    {
        float time = 0;
        while (time < 1)
        {
            yield return null;
            ColorVideo.GetComponent<VideoPlayer>().targetCameraAlpha = Mathf.Lerp(1, 0, time / 1);
            time += Time.unscaledDeltaTime;
        }
        ColorVideo.GetComponent<VideoPlayer>().targetCameraAlpha = 0;
        yield break;
    }
    IEnumerator changefocus(GameObject newtarget, float zoomdelay = 0.5f, float zoomspeed = 1.0f, float zoomsize = 1.0f, float zoompause = 1.0f)
    {
        GameObject tmp;
        GameObject currentcam;
        if (FlagManager.Instance.IsPast)
        {
            tmp = Fran;
            Fran = newtarget;
            currentcam = PastCam;
        }
        else
        {
            tmp = Player;
            Player = newtarget;
            currentcam = gameObject;
        }

        yield return new WaitForSeconds(zoomdelay);
        for (float i = 0; i < 1; i += 0.025f)
        {
            currentcam.GetComponent<Camera>().orthographicSize = Mathf.Lerp(DefaultScreenSize, DefaultScreenSize - zoomsize, i + 0.025f);
            yield return new WaitForSeconds(zoomspeed / 40);
        }
        yield return new WaitForSeconds(zoompause);
        for (float i = 0; i < 1; i += 0.025f)
        {
            currentcam.GetComponent<Camera>().orthographicSize = Mathf.Lerp(DefaultScreenSize - zoomsize, DefaultScreenSize, i + 0.025f);
            yield return new WaitForSeconds(zoomspeed / 40);
        }
        if (FlagManager.Instance.IsPast)
            Fran = tmp;
        else
            Player = tmp;
    }
}

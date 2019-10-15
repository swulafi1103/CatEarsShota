using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;


[DefaultExecutionOrder(-21)]
public class MainCamera : MonoBehaviour
{
    private int atcurrentmap = 0;
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
    public GameObject videoCanvas;
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

    public Vector3[] map_point = new Vector3[4];
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
        if (Input.GetKeyDown(KeyCode.M))
        {
            MovingMap(0);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            MovingMap(1);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            SkipVideo();
        }
        if (FlagManager.Instance.IsPast)
        {
            if (Fran.transform.position.x < PastCameraLimit[1] && Fran.transform.position.x > PastCameraLimit[0])
                PastCam.transform.position = Vector3.Lerp(PastCam.transform.position, Fran.transform.position - rangeToTarget, Time.deltaTime);
            else
            {
                //Debug.Log("limited");
                int tmpindex = Fran.transform.position.x > PastCameraLimit[1] ? 1 : 0;
                Vector3 limitpos = new Vector3(PastCameraLimit[tmpindex], Fran.transform.position.y, Fran.transform.position.z) - rangeToTarget;
                PastCam.transform.position = Vector3.Lerp(PastCam.transform.position, limitpos, Time.deltaTime);
            }
        }
        else
        {
            if (Player.transform.position.x < CameraLimit[1 + (atcurrentmap * 2)] && Player.transform.position.x > CameraLimit[0 + (atcurrentmap * 2)])
                transform.position = Vector3.Lerp(transform.position, Player.transform.position - rangeToTarget, Time.deltaTime);
            else
            {
                int tmpindex = Player.transform.position.x > CameraLimit[1+(atcurrentmap*2)] ? 1 : 0;
                Vector3 limitpos = new Vector3(CameraLimit[tmpindex + (atcurrentmap * 2)], Player.transform.position.y, Player.transform.position.z) - rangeToTarget;
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
    public void MovingMap(int mapNumber)
    {
        if (!FlagManager.Instance.IsEventing)
        {
            StartCoroutine(MovingBetweenMap(mapNumber));
            atcurrentmap=mapNumber;
        }
    }
    public void MovingMapnotEventing(int mapNumber)
    {
        switch (mapNumber)
        {
            case 0:
                PlayerManager.Instance.Pero.transform.position = map_point[mapNumber];
                break;
            case 1:
                PlayerManager.Instance.Pero.transform.position = map_point[mapNumber];
                break;
            case 2:
                PlayerManager.Instance.Fran.transform.position = map_point[mapNumber];
                break;
            case 3:
                PlayerManager.Instance.Fran.transform.position = map_point[mapNumber];
                break;
            default:
                Debug.LogWarning("キャラ移動でエラー");
                break;
        }
    }
    public void SkipVideo() //スキップの機能追加
    {
        if (ColorVideo.GetComponent<VideoPlayer>().isPlaying)
        {
            ColorVideo.GetComponent<VideoPlayer>().Stop();
            videoCanvas.SetActive(false);
            FlagManager.Instance.IsMovie = false;
            Fade.Instance.StartFade(1f, Color.clear);
            ColorVideo.SetActive(false);
            PlayVideo = false;
        }

    }
    public void TriggeredVideo(uint index)　//動画を放送
    {
        videoCanvas.SetActive(true);
        if (FlagManager.Instance.IsPast)
        {
            videoCanvas.GetComponent<Canvas>().worldCamera = PastCam.GetComponent<Camera>();
        }
        else
        {
            videoCanvas.GetComponent<Canvas>().worldCamera = gameObject.GetComponent<Camera>();
        }
        FlagManager.Instance.IsMovie = true;
        //StartCoroutine(FadeInMovie());
        ColorVideo.GetComponent<VideoStorage>().index = index;
        PlayVideo = true;
    }
    //  動画終了時のフェード
    void MovieFinished(VideoPlayer sorce)
    {
        videoCanvas.SetActive(false);
        //Fade.Instance.StartFade(1f, Color.clear);
        StartCoroutine(DisableFade());
    }

    IEnumerator DisableFade()
    {
        yield return new WaitForSeconds(0.15f);
        if (FlagManager.Instance.IsMovie) yield break;
        //float time = 0;
        //while (time < 2f)
        //{
        //    time += Time.deltaTime;
        //    yield return null;
        //}
        ColorVideo.GetComponent<VideoPlayer>().Stop();
        videoCanvas.SetActive(false);
        FlagManager.Instance.IsMovie = false;
        Fade.Instance.StartFade(0.25f, Color.clear);
        ColorVideo.SetActive(false);
        PlayVideo = false;
        yield break;
    }
    //カメラを映るターゲット
    public void T_ChangeFocus(GameObject newtarget)　
    {
        if (!Zooming)
            StartCoroutine(changefocus(newtarget));
    }
    //遅延時間の追加
    public void T_ChangeFocus(GameObject newtarget, float zoomdelay)　
    {
        if (!Zooming)
            StartCoroutine(changefocus(newtarget, zoomdelay));
    }
    //拡大速度の追加
    public void T_ChangeFocus(GameObject newtarget, float zoomdelay, float zoomspeed)　
    {
        if (!Zooming)
            StartCoroutine(changefocus(newtarget, zoomdelay, zoomspeed));
    }
    //拡大倍数の追加
    public void T_ChangeFocus(GameObject newtarget, float zoomdelay, float zoomspeed, float zoomsize)　
    {
        if (!Zooming)
            StartCoroutine(changefocus(newtarget, zoomdelay, zoomspeed, zoomsize));
    }
    //拡大後止まる時間の追加
    public void T_ChangeFocus(GameObject newtarget, float zoomdelay, float zoomspeed, float zoomsize, float zoompause)　
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
    IEnumerator MovingBetweenMap(int mapnumber)
    {
        FlagManager.Instance.IsEventing = true;
        Fade.Instance.StartFade(1.0f,Color.black);
        yield return new WaitForSeconds(1.0f);
        if (FlagManager.Instance.IsPast)
            Fran.transform.position = map_point[mapnumber+2];
        else
            Player.transform.position = map_point[mapnumber];
        yield return new WaitForSeconds(2f);
        Fade.Instance.StartFade(1.0f, Color.clear);
        yield return new WaitForSeconds(1.0f);
        FlagManager.Instance.IsEventing = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    #region Singleton
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogWarning("SoundManager is Null");
            }
            return instance;
        }
    }

    private bool CheckInstance()
    {
        if (instance == null)
        {
            instance = (SoundManager)this;
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

    [SerializeField]
    private AudioClip[] BGM;
    [SerializeField]
    private AudioClip[] SE;

    private AudioSource[] audioSource;
    [SerializeField]
    float fadeTime = 1;

    public enum SE_Name
    {
        SE_00_Alerm,
        SE_01_BreakWin,
        SE_02_Wator,
        SE_03,
        SE_04,
        SE_05,
    };

    public enum BGM_Name {
        BGM_00_Opening,
        BGM_01_Gray,
        BGM_02_Yellow,
        BGM_03_Red,
        BGM_04_Blue,
        BGM_05_Green,
        BGM_06_Past1,
        BGM_07_Past2
    };

    BGM_Name _perraultBGM;
    BGM_Name _franBGM = BGM_Name.BGM_06_Past1;

    void Awake()
    {
        CheckInstance();
        audioSource = GetComponents<AudioSource>();
    }

    private void Start() {
        audioSource[0].clip = BGM[(int)BGM_Name.BGM_05_Green];
        audioSource[0].Play();
    }

    /// <summary>
    /// debug
    /// </summary>
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha0)) {
            PlayBGM(BGM_Name.BGM_00_Opening);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            PlayBGM(BGM_Name.BGM_01_Gray);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            PlayBGM(BGM_Name.BGM_02_Yellow);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            PlayBGM(BGM_Name.BGM_03_Red);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9)) {
            ChangeTimesBGM();
        }
    }

    /// <summary>
    /// その時間軸内でのBGM切り替え
    /// </summary>
    /// <param name="_Name"></param>
    public void PlayBGM(BGM_Name _Name)
    {
        switch (_Name) {
            case BGM_Name.BGM_00_Opening:
                _perraultBGM = _Name;
                audioSource[0].clip = BGM[(int)_Name];
                StartCoroutine(FadeIn());
                break;
            case BGM_Name.BGM_01_Gray:
                _perraultBGM = _Name;
                audioSource[1].clip = BGM[(int)_Name];
                StartCoroutine(WithBGMPlay());
                break;
            case BGM_Name.BGM_02_Yellow:
                _perraultBGM = _Name;
                StartCoroutine(WithBGMStop());
                break;
            default:
                StartCoroutine(ChangeBGM(_Name));
                break;
        }
    }

    /// <summary>
    /// 過去未来切り替え時の切り替え
    /// </summary>
    public void ChangeTimesBGM() {
        StartCoroutine(ChangeTimeBGM());
    }

    /// <summary>
    /// gray時の特殊再生
    /// </summary>
    /// <returns></returns>
    IEnumerator WithBGMPlay() {
        audioSource[0].volume = 0.5f;
        audioSource[1].volume = 0;
        float time = 0;
        audioSource[1].Play();
        while (time <= fadeTime) {
            audioSource[1].volume = time / fadeTime;
            time += Time.unscaledDeltaTime;
            yield return null;
        }
        audioSource[1].volume = 1;
        yield break;
    }

    /// <summary>
    /// gray→yellowの特殊切り替え
    /// </summary>
    /// <returns></returns>
    IEnumerator WithBGMStop() {
        float time = 0;
        while (time <= fadeTime) {
            audioSource[1].volume = 1 - (time / fadeTime);
            audioSource[0].volume = (1 - (time / fadeTime)) / 2;
            time += Time.unscaledDeltaTime;
            yield return null;
        }
        audioSource[0].volume = 0;
        audioSource[1].volume = 0;
        audioSource[0].Stop();
        audioSource[1].Stop();

        audioSource[0].clip = BGM[(int)BGM_Name.BGM_02_Yellow];

        var col = StartCoroutine(FadeIn());
        yield return col;
        yield break;
    }

    /// <summary>
    /// サウンド切り替え
    /// </summary>
    /// <param name="_Name"></param>
    /// <returns></returns>
    IEnumerator ChangeBGM(BGM_Name _Name) {
        bool isFran = FlagManager.Instance.IsPast;
        if (isFran) {
            _franBGM = _Name;
        }
        else {
            _perraultBGM = _Name;
        }
        var col = StartCoroutine(FadeOut());
        yield return col;
        audioSource[0].clip = BGM[(int)_Name];
        col = StartCoroutine(FadeIn());
        yield return col;
        yield break;
    }

    /// <summary>
    /// 時間切り替えのみのBGM切り替え
    /// </summary>
    /// <returns></returns>
    IEnumerator ChangeTimeBGM() {
        bool isFran = FlagManager.Instance.IsPast;
        var col = StartCoroutine(FadeOut());
        yield return col;
        if (isFran) {
            audioSource[0].clip = BGM[(int)_perraultBGM];
        }
        else {
            audioSource[0].clip = BGM[(int)_franBGM];
        }
        col = StartCoroutine(FadeIn());
        yield return col;
        yield break;
    }

    /// <summary>
    /// fadein
    /// </summary>
    /// <returns></returns>
    IEnumerator FadeIn() {
        float time = 0;
        audioSource[0].Play();
        while (time <= fadeTime) {
            audioSource[0].volume = time / fadeTime;
            time += Time.unscaledDeltaTime;
            yield return null;
        }
        audioSource[0].volume = 1;
        yield break;
    }

    /// <summary>
    /// fadeout
    /// </summary>
    /// <returns></returns>
    IEnumerator FadeOut() {
        float time = 0;
        while (time <= fadeTime) {
            audioSource[0].volume = 1 - (time / fadeTime);
            time += Time.unscaledDeltaTime;
            yield return null;
        }
        audioSource[0].volume = 0;
        audioSource[0].Stop();
        yield break;
    }

    /// <summary>
    /// 止める
    /// </summary>
    public void StopBGM()
    {
        StartCoroutine(FadeOut());
    }

    /// <summary>SEの再生</summary>
    /// <param name="_Name"></param>
    public void PlaySE(SE_Name _Name)
    {
        audioSource[0].PlayOneShot(SE[(int)_Name]);
    }
    /// <summary>SEの再生(音量調整)</summary>
    /// <param name="_Name"></param>
    /// <param name="_Vol"></param>
    public void PlaySE(SE_Name _Name, float _Vol)
    {
        audioSource[0].PlayOneShot(SE[(int)_Name], _Vol);
    }


}

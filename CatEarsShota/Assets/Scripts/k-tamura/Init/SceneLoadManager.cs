using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// フェードシーンローダー
/// </summary>
public class SceneLoadManager : MonoBehaviour
{
    #region Singleton
    private static SceneLoadManager instance = null;
    public static SceneLoadManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SceneLoadManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject(typeof(SceneLoadManager).Name);
                    instance = obj.AddComponent<SceneLoadManager>();
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        if (CheckInstance())
        {
            DontDestroyOnLoad(this);
        }
    }

    bool CheckInstance()
    {
        if (instance == null)
        {
            instance = this;
            return true;
        }
        else if (Instance == this)
        {
            return true;
        }

        enabled = false;
        DestroyImmediate(gameObject);
        return false;
    }
    #endregion

    // ADVシーンで読み込む変数
    static string filePath = "";
    public static string TextFilePath { get { return filePath; } }

    // LoadSceneで読み込むシーン名
    static string nextScene = "";
    public static string NextScene { get { return nextScene; } }

    static bool isFading = false;
    float fadeAlpha = 0;

    public float fadeTime = 2f;
    public Color fadeColor = Color.white;

    IEnumerator fadeOut;
    IEnumerator fadeIn;

    public void OnGUI()
    {
        // Fade
        if (isFading)
        {
            //色と透明度を更新して白テクスチャを描画 .
            fadeColor.a = fadeAlpha;
            GUI.color = fadeColor;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
        }
    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        Instance.StartCoroutine(Instance.FadeOutScene(Instance.fadeTime, () =>
        {
            //シーン切替 .
            SceneManager.LoadScene("Loading");
        }));
    }
    /*
    public static void LoadADVScene(string textFilePath)
    {
        filePath = textFilePath;
        nextScene = "ADVScene";
        Instance.StartCoroutine(Instance.FadeOutScene(Instance.fadeTime, () =>
        {
            //シーン切替 .
            SceneManager.LoadScene("Loading");
        }));
    }
    */
    public static void FadeOut(float time, System.Action callback = null)
    {
        if (Instance.fadeIn != null) Instance.StopCoroutine(Instance.fadeIn);
        Instance.fadeOut = Instance.FadeOutScene(time, callback);
        Instance.StartCoroutine(Instance.fadeOut);
    }

    public static void FadeOut(System.Action callback)
    {
        FadeOut(Instance.fadeTime, callback);
    }

    public static void FadeOut()
    {
        FadeOut(Instance.fadeTime);
    }

    public static void FadeIn(float time, System.Action callback = null)
    {
        if (Instance.fadeOut != null) Instance.StopCoroutine(Instance.fadeOut);
        Instance.fadeIn = Instance.FadeInScene(time, callback);
        Instance.StartCoroutine(Instance.fadeIn);
    }

    public static void FadeIn(System.Action callback)
    {
        FadeIn(Instance.fadeTime, callback);
    }

    public static void FadeIn(float time)
    {
        FadeIn(time, null);
    }

    public static void FadeIn()
    {
        FadeIn(Instance.fadeTime);
    }

    IEnumerator FadeOutScene(float interval, System.Action callback)
    {
        //だんだん暗く
        isFading = true;
        float time = 0;
        while (time <= interval)
        {
            fadeAlpha = Mathf.Lerp(0f, 1f, time / interval);
            time += Time.unscaledDeltaTime;
            yield return 0;
        }

        if (callback != null)
        {
            callback();
        }
    }

    IEnumerator FadeInScene(float interval, System.Action callback)
    {
        //だんだん明るく
        isFading = true;
        float time = 0;
        while (time <= interval)
        {
            fadeAlpha = Mathf.Lerp(1f, 0f, time / interval);
            time += Time.unscaledDeltaTime;
            yield return 0;
        }

        if (callback != null)
        {
            callback();
        }
        isFading = false;
    }
}

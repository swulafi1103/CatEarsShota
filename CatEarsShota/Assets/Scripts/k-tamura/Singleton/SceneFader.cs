using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

[DisallowMultipleComponent]
public class SceneFader : SingletonMonoBehaviour<SceneFader>
{
    public bool IsFading
    {
        get { return _fader.IsFading || _fader.Alpha != 0; }
    }
    private string _beforeSceneName = "";
    public string BeforeSceneName
    {
        get { return _beforeSceneName; }
    }
    private string _currentSceneName = "";
    public string CurrentSceneName
    {
        get { return _currentSceneName; }
    }
    private string _nextSceneName = "";

    public string NextSceneName
    {
        get { return _nextSceneName; }
    }
    public event Action FadeOutFinished = delegate { };
    public event Action FadeInFinished = delegate { };

    [SerializeField]
    private CanvasFader _fader = null;
    public const float FADE_TIME = 0.5f;
    private float _fadeTime = FADE_TIME;
    protected override void Init()
    {
        base.Init();
        if (_fader == null)
        {
            Reset();
        }
        _currentSceneName = SceneManager.GetSceneAt(0).name;
        DontDestroyOnLoad(gameObject);
        _fader.gameObject.SetActive(false);
    }
    private void Reset()
    {
        gameObject.name = "SceneNavigator";
        GameObject fadeCanvas = new GameObject("FadeCanvas");
        fadeCanvas.transform.SetParent(transform);
        fadeCanvas.SetActive(false);
        Canvas canvas = fadeCanvas.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 999;
        fadeCanvas.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        fadeCanvas.AddComponent<GraphicRaycaster>();
        _fader = fadeCanvas.AddComponent<CanvasFader>();
        _fader.Alpha = 0;
        GameObject imageObject = new GameObject("Image");
        imageObject.transform.SetParent(fadeCanvas.transform, false);
        imageObject.AddComponent<Image>().color = Color.black;
        imageObject.GetComponent<RectTransform>().sizeDelta = new Vector2(2000, 2000);
    }
    public void Change(string sceneName, float fadeTime = FADE_TIME)
    {
        if (IsFading)
        {
            return;
        }
        
        _nextSceneName = sceneName;
        _fadeTime = fadeTime;
        _fader.gameObject.SetActive(true);
        _fader.Play(isFadeOut: false, duration: _fadeTime, onFinished: OnFadeOutFinish);
    }
    private void OnFadeOutFinish()
    {
        FadeOutFinished();
        SceneManager.LoadScene(_nextSceneName);
        _beforeSceneName = _currentSceneName;
        _currentSceneName = _nextSceneName;
        _fader.gameObject.SetActive(true);
        _fader.Play(isFadeOut: true, duration: _fadeTime, onFinished: OnFadeInFinish);
    }
    private void OnFadeInFinish()
    {
        _fader.gameObject.SetActive(false);
        FadeInFinished();
    }
}

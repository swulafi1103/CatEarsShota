using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartSneneManager : MonoBehaviour
{
    [SerializeField]
    private GameObject fadeAfterImage;
    [SerializeField]
    private GameObject startButtonImage;

    float alpha_Sin = 0f;
    const float interval = 1f;
    const float waitTime = 0.25f;
    bool loading = false;

    void Start()
    {
        ProductionTitle();
    }

    void Update()
    {
        alpha_Sin = Mathf.Sin(Time.time) * 0.5f + 0.5f;
        LoadMainScene();
    }



    //  背景画像の変更後、シーン移動
    void LoadMainScene()
    {
        if (Input.GetKeyDown(KeyCode.Space) && loading == false)
        {
            loading = true;
            StartCoroutine(ChengeBackImage());
        }
    }

    IEnumerator ChengeBackImage()
    {
        float countTime = 0;
        while (countTime < interval)
        {
            yield return null;
            Color color = fadeAfterImage.GetComponent<Image>().color;
            float alpha = Mathf.Lerp(0f, 1f, countTime / interval);
            color.a = alpha;
            fadeAfterImage.GetComponent<Image>().color = color;
            countTime += Time.unscaledDeltaTime;
        }
        yield return new WaitForSeconds(waitTime);
        SceneLoadManager.LoadScene("main");
        yield break;
    }

    //  スタートボタンの点滅
    void ProductionTitle()
    {
        StartCoroutine(BlinkImage());
    }

    IEnumerator BlinkImage()
    {
        while (!loading)
        {
            yield return new WaitForEndOfFrame();
            startButtonImage.GetComponent<CanvasGroup>().alpha = alpha_Sin;
        }
        yield break;
    }
}

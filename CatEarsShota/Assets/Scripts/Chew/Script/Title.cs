using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    private bool started =false;
    private bool fading = false;
    public GameObject SecondScreen; // second image
    public GameObject WhiteScreen;
    public GameObject ButterFly;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && !started)
        {
            started = true;
            fading = true;
            SoundManager.Instance.PlayBGM(SoundManager.BGM_Name.BGM_00_Opening);
            ButterFly.GetComponent<Animator>().SetBool("pressed", true);
            StartCoroutine(LoadScene());
        }
    }
   /* IEnumerator StartingAnime()
    {
        yield return new WaitForSeconds(3.0f);
        for(int i = 0; i < 50; i++)
        {
            SecondScreen.GetComponent<Image>().color = Color.Lerp(Color.clear, Color.white, (float)i / 49);
            yield return new WaitForSeconds(0.01f);
        }
        StartCoroutine(LoadScene());
    }*/
    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(6.0f);
        for (float i = 0; i < 1; i += 0.01f)
        {
            SecondScreen.GetComponent<Image>().color = Color.Lerp(Color.clear, Color.white, i);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1.5f);
        for (float i = 0; i < 1; i += 0.01f)
        {
            WhiteScreen.GetComponent<Image>().color = Color.Lerp(Color.clear, Color.white, i);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1.5f);
        for (float i = 0; i < 1; i += 0.01f)
        {
            WhiteScreen.GetComponent<Image>().color = Color.Lerp(Color.white, Color.black, i);
            yield return new WaitForSeconds(0.01f);
        }
        fading = false;
        StartCoroutine(StartLoading());
    }
    IEnumerator StartLoading()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(1);
        while (!async.isDone && !fading)
        {
            yield return null;
        }
    }
}

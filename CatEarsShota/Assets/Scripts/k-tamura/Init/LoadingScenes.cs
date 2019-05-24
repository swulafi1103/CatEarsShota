using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadingScenes : MonoBehaviour {
	/*
	[SerializeField]
	private AsyncOperation async;
	public GameObject LoadingUI;
	public Slider Slider;
	public void LoadNextScene(){
		LoadingUI.SetActive(true);
		StartCoroutine(LoadScene());
	}

	IEnumerator LoadScene(){
		async = SceneManager.LoadSceneAsync("");
		while(!async.isDone){
			Slider.value = async.progress;
			yield return null;
		}
	}
	*/

	[SerializeField]
	private float FadeInTime;

	private Image image;
	private void Start()
	{
		image = transform.Find("Panel").GetComponent<Image>();
		FadeInTime = 1f * FadeInTime / 10f;
		StartCoroutine("FadeIn");

	}
	IEnumerator FadeIn(){
		for (var i = 1f; i >= 0;i-=0.1f){
			image.color = new Color(0f, 0f, 0f, i);
			yield return new WaitForSeconds(FadeInTime);
		}
	}

}

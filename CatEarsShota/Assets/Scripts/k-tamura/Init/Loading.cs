using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour {
	[SerializeField]
	private AsyncOperation async;
	[SerializeField]
	private GameObject loadUI;
	[SerializeField]
	private Slider slider;
	string nextScene;
	public void NextScene(){
		loadUI.SetActive(true);
		StartCoroutine("Loaddata");
	}
	IEnumerator Loaddata(){
		Time.timeScale = 1.0f;
        SceneLoadManager.FadeIn();

        nextScene = SceneLoadManager.NextScene;

		async = SceneManager.LoadSceneAsync(nextScene);
		while(!async.isDone){
			var progressVal = Mathf.Clamp01(async.progress / 0.9f);
			slider.value = progressVal;
			yield return null;
		}
	}
	// Use this for initialization
	void Start () {
		StartCoroutine(Loaddata());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

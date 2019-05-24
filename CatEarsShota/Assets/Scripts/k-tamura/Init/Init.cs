using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : MonoBehaviour {

	bool onLoad = false;

	private void Awake()
	{
		int width = Screen.width;
        int height = Screen.height;
		Screen.SetResolution(width, height, Screen.fullScreen);
	}
    
	void Update()
    {
        if (!Application.isShowingSplashScreen && !onLoad)
        {
            onLoad = true;
            SceneLoadManager.LoadScene("Title");
        }
    }

}

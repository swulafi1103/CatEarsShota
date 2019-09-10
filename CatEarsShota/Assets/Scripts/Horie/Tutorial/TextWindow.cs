using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWindow : MonoBehaviour
{
    [SerializeField]
    Sprite[] TextImages;

    Image Window;

    float hiddenTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        Window = GetComponent<Image>();
        Window.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetWindow(int num) {
        Window.sprite = TextImages[num];
        Window.SetNativeSize();
        Window.enabled = true;
        StartCoroutine(HiddenWindow());
    }

    IEnumerator HiddenWindow() {

        yield return new WaitForSeconds(hiddenTime);

        Window.enabled = false;

        yield break;
    }
}

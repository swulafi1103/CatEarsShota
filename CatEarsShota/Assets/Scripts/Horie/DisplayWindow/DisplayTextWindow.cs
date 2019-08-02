using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayTextWindow : MonoBehaviour
{
    static DisplayTextWindow instance;
    public static DisplayTextWindow Instance
    {
        get { return instance; }
    }
    private void Awake()
    {
        if (instance == null) instance = this;
    }

    [SerializeField]
    Sprite[] TextSprites = new Sprite[5];

    Image image;

    float ActTime = 3;
    float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        image.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (image.enabled == false) return;
        time += Time.unscaledDeltaTime;
        if (time >= ActTime)
        {
            image.enabled = false;
            time = 0;
        }
    }

    public void SetDisplayWindow(int num)
    {
        transform.parent.GetComponent<Canvas>().worldCamera = Camera.main;
        image.sprite = TextSprites[num];
        image.SetNativeSize();
        image.enabled = true;
        time = 0;
    }
}

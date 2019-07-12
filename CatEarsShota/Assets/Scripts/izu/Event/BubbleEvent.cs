using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleEvent : MonoBehaviour
{
    #region Singleton
    private static BubbleEvent instance;
    public static BubbleEvent Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogWarning("BubbleEvent is Null");
            }
            return instance;
        }
    }

    private bool CheckInstance()
    {
        if (instance == null)
        {
            instance = (BubbleEvent)this;
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

    public enum BubbleType
    {
        Door,
        Repair,
        Tana,
        Dark,
        Escape,
        Power,
    }

    //  説明用の吹き出し
    [SerializeField]
    private GameObject baseBubble;
    private GameObject childBubble;
    [SerializeField]
    private List<BubbleImages> bubbleSprites = new List<BubbleImages>();

    private const float chengeTime = 0.5f;

    void Awake()
    {
        CheckInstance();
    }

    void Start()
    {
        childBubble = baseBubble.transform.GetChild(0).gameObject;
    }


    void Update()
    {
        //  デバッグ
        if (Input.GetKeyDown(KeyCode.F6))
        {
            Debug.Log("aaaa");
            DisplayBubbles(BubbleType.Repair);
        }
        
        if (baseBubble.activeSelf == true)
        {
            baseBubble.transform.position = PlayerManager.Instance.GetPlayerPos() + new Vector2(2, 1);
        }
    }

    public void DisplayBubbles(BubbleType type, float deleyTime = 0)
    {
        List<Sprite> bubbles = new List<Sprite>();
        switch (type)
        {
            //  例外
            case BubbleType.Repair:
                bubbles.Add(bubbleSprites[(int)type].sprites[0]);
                bubbles.Add(bubbleSprites[(int)type].sprites[1]);
                bubbles.Add(bubbleSprites[(int)type].sprites[0]);
                bubbles.Add(bubbleSprites[(int)type].sprites[1]);
                bubbles.Add(bubbleSprites[(int)type].sprites[2]);
                bubbles.Add(bubbleSprites[(int)type].sprites[3]);
                bubbles.Add(bubbleSprites[(int)type].sprites[2]);
                bubbles.Add(bubbleSprites[(int)type].sprites[3]);
                break;
            //  通常
            default:
                foreach (Sprite sprite in bubbleSprites[(int)type].sprites)
                {
                    if (sprite != null)
                    {
                        bubbles.Add(sprite);
                    }
                }
                break;
        }
        //  Spriteの配列の長さ
        int count = bubbles.Count;
        if (count <= 0)
        {
            return;
        }
        StartCoroutine(BubbleAnimation(bubbles.ToArray(), count, deleyTime));
    }

    IEnumerator BubbleAnimation(Sprite[] bubbles, int length, float delaytime = 0)
    {
        yield return new WaitForSeconds(delaytime);
        baseBubble.SetActive(true);
        float time = 0;
        int now = 0;
        int count = 0;
        while (time < 3)
        {
            yield return null;
            childBubble.GetComponent<SpriteRenderer>().sprite = bubbles[now];
            time += Time.deltaTime + chengeTime;
            yield return new WaitForSeconds(chengeTime);
            count++;
            now = count % length;
        }
        yield return null;
        baseBubble.SetActive(false);
        yield break; 
    }

    [System.Serializable]
    struct BubbleImages
    {
        public List<Sprite> sprites;
    }

}

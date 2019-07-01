using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
struct BubbleImages
{
    public List<Sprite> sprites;
}

public class BubbleEvent : MonoBehaviour
{
    public enum BubbleType
    {
        Door,
        Repair,
    }

    //  説明用の吹き出し
    [SerializeField]
    private List<BubbleImages> bubbleSprites = new List<BubbleImages>();


    public void DisplayBubbles(BubbleType type)
    {
        List<Sprite> bubbles = new List<Sprite>();
        foreach (Sprite sprite in bubbleSprites[(int)type].sprites)
        {
            if (sprite != null)
            {
                bubbles.Add(sprite);
            }
        }
        int count = bubbles.Count;
        if (count <= 0)
        {
            return;
        }
        

    }

    IEnumerator BubbleAnimation()
    {

        yield break; 
    }
}

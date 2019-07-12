using UnityEngine;
using UnityEngine.UI;

public class ChangePressKey : MonoBehaviour
{

    [SerializeField]
    private Sprite afterSprite;
    private Sprite beforeSprite;

    void Start()
    {
        beforeSprite = GetComponent<Image>().sprite;
    }

    public void ChangeAfterSprite()
    {
        GetComponent<Image>().sprite = afterSprite;
    }
}

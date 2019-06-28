using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBase : MonoBehaviour
{
    [EnumFlags]
    public ItemFlag needFlag;

    [SerializeField]
    protected Sprite bubbleImage;
    [SerializeField]
    protected Vector2 babblePos;
    [SerializeField]
    protected bool isDisplayBabble;


    void Start()
    {
        
    }

    //void Update()
    //{
        
    //}
}

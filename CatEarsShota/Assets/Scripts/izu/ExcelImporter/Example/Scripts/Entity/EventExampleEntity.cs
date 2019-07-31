using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EventExampleEntity
{
    public EventCategory category;
    public int num;
    public float delayTime;
}

//public enum EventCategory
//{
//    Moive = 0,
//    Fade,
//    CameraZoom,
//    Bubble,
//    DropItem,
//    PickupItem,
//    Warp,
//    ChangeTime,
//    MiniGame1,
//    MiniGame2,
//    SE,
//    BGM,
//}
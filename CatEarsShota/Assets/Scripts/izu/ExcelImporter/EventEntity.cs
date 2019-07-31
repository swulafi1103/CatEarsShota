using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EventEntity
{
    public EventCategory category;
    public int value = 0;
    public float delayTime = 0f;
    public bool waitMovie = false;
}

public enum EventCategory
{
    Movie,
    FadeIn,
    FadeOut,
    CameraZoom,
    Bubble,
    ChangeTime,
    Warp,
    DropItem,
    PickupItem,
    Tutorial,
    Minigame1,
    Minigame2,
    BGM,
    SE,
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoStorage : MonoBehaviour
{
    public uint index =0;
    public VideoClip[] VideoStore;

    // Update is called once per frame
    void Update()
    {
        if(index < VideoStore.Length && GetComponent<VideoPlayer>().clip != VideoStore[index])
        {
            GetComponent<VideoPlayer>().clip = VideoStore[index];
        }
    }
}

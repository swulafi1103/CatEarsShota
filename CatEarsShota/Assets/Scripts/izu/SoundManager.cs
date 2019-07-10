using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    #region Singleton
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogWarning("SoundManager is Null");
            }
            return instance;
        }
    }

    private bool CheckInstance()
    {
        if (instance == null)
        {
            instance = (SoundManager)this;
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

    [SerializeField]
    private AudioClip[] BGM;
    [SerializeField]
    private AudioClip[] SE;

    private AudioSource audioSource;

    public enum SE_Name
    {
        SE_00_Alerm,
        SE_01_BreakWin,
        SE_02_Wator,
        SE_03,
        SE_04,
        SE_05,
    };

    void Awake()
    {
        CheckInstance();
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayBGM()
    {
        audioSource.Play();
    }

    public void StopBGM()
    {
        audioSource.Stop();
    }

    /// <summary>SEの再生</summary>
    /// <param name="_Name"></param>
    public void PlaySE(SE_Name _Name)
    {
        audioSource.PlayOneShot(SE[(int)_Name]);
    }
    /// <summary>SEの再生(音量調整)</summary>
    /// <param name="_Name"></param>
    /// <param name="_Vol"></param>
    public void PlaySE(SE_Name _Name, float _Vol)
    {
        audioSource.PlayOneShot(SE[(int)_Name], _Vol);
    }


}

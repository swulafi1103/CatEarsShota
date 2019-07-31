using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    #region Singleton
    private static EventManager instance;
    public static EventManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogWarning("EventManager is Null");
            }
            return instance;
        }
    }

    private bool CheckInstance()
    {
        if (instance == null)
        {
            instance = (EventManager)this;
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

    private List<GameObject> gimmickList = new List<GameObject>();
    private List<GameObject> itemList = new List<GameObject>();

    void Awake()
    {
        CheckInstance();
        SetGimmickItemObject();
    }

    void Start()
    {
        
    }

    void Update()
    {
        //
        if (Input.GetKeyDown(KeyCode.F5))
        {
            UpdateEvent();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            PlayEvent();
        }        
    }

    /// <summary>
    /// フラグのチェックと更新
    /// </summary>
    public void UpdateEvent()
    {
        foreach(GameObject obj in gimmickList)
        {
            if (obj != null)
            {
                if (obj.GetComponent<EventBase>() != null)
                {
                    obj.GetComponent<EventBase>().CheckFlag();
                }
            }
        }
        foreach (GameObject obj in itemList)
        {
            if (obj != null)
            {
                if (obj.GetComponent<EventBase>() != null)
                {
                    obj.GetComponent<EventBase>().CheckFlag();
                }
            }
        }
    }

    void SetGimmickItemObject()
    {
        GameObject[] GimmickObjects = GameObject.FindGameObjectsWithTag("Gimmick");
        foreach (var obj in GimmickObjects)
        {
            if (!gimmickList.Contains(obj))
            {
                gimmickList.Add(obj);
            }
        }
        GameObject[] ItemObjects = GameObject.FindGameObjectsWithTag("Item");
        foreach (var obj in ItemObjects)
        {
            if (!itemList.Contains(obj))
            {
                itemList.Add(obj);
            }
        }
    }

    private IEnumerator CallCorutine(EventCategory eventCategory, int value, float delayTime, bool waitMovie)
    {
        switch (eventCategory)
        {
            case EventCategory.Movie:
                return PlayMovie(value, delayTime, waitMovie);
            case EventCategory.FadeIn:
                return FadeIn(value, delayTime, waitMovie);
            case EventCategory.FadeOut:
                return FadeOut(value, delayTime, waitMovie);
            case EventCategory.CameraZoom:
                return CameraZoom(value, delayTime, waitMovie);
            case EventCategory.Bubble:
                return Bubble(value, delayTime, waitMovie);
            case EventCategory.ChangeTime:
                return ChangeTime(value, delayTime, waitMovie);
            case EventCategory.Warp:
                return WarpPositionFran(value, delayTime, waitMovie);
            case EventCategory.DropItem:
                return DropItem(value, delayTime, waitMovie);
            case EventCategory.PickupItem:
                return PickupItem(value, delayTime, waitMovie);
            case EventCategory.Tutorial:
                return Tutorial(value, delayTime, waitMovie);
            case EventCategory.Minigame1:
                return Minigame1(value, delayTime, waitMovie);
            case EventCategory.Minigame2:
                return Minigame2(value, delayTime, waitMovie);
            case EventCategory.BGM:
                return PlayBGM(value, delayTime, waitMovie);
            case EventCategory.SE:
                return PlaySE(value, delayTime, waitMovie);
            default:
                Debug.LogWarning("EventCategoryError");
                break;
        }
        Debug.LogWarning("EventCategoryError");
        return null;
    }


    public EventExcel eventExcel;

    public void PlayEvent()
    {
        StartCoroutine(EventCorutine());
    }

    IEnumerator EventCorutine()
    {
        int count = eventExcel.EventTest1.Count;
        Debug.Log("EventCount : " + count);
        yield return null;
        for (int i = 0; i < count; i++)
        {
            FlagManager.Instance.IsEventing = true;
            yield return StartCoroutine(CallCorutine(eventExcel.EventTest1[i].category, eventExcel.EventTest1[i].value, eventExcel.EventTest1[i].delayTime, eventExcel.EventTest1[i].waitMovie));
        }
        Debug.Log("Event_END");
        yield break;
    }

    #region イベントコルーチン

    IEnumerator PlayMovie(int value = 0, float delayTime = 0, bool waitMovie = false)
    {
        Debug.Log("Movie");
        yield return new WaitForSeconds(delayTime);
        Debug.Log("Uint = " + (uint)value);
        MainCamera.Instance.TriggeredVideo((uint)value);
        yield break;
    }
    IEnumerator FadeIn(int value = 0, float delayTime = 0, bool waitMovie = false)
    {
        Debug.Log("FadeIn");
        yield return new WaitForSeconds(delayTime);
        switch (value)
        {
            case 0:
                Fade.Instance.StartFade(0.5f, Color.black);
                break;
            case 1:
                Fade.Instance.StartFade(0.5f, Color.white);
                break;
            default:
                Debug.LogWarning("FadeInError, Value is Over");
                break;
        }        
        yield break;
    }
    IEnumerator FadeOut(int value = 0, float delayTime = 0, bool waitMovie = false)
    {
        Debug.Log("FadeOut");
        yield return new WaitForSeconds(delayTime);
        Fade.Instance.ClearFade(0.5f, Color.clear);
        yield break;
    }
    IEnumerator CameraZoom(int value = 0, float delayTime = 0, bool waitMovie = false)
    {
        Debug.Log("CameraZoom");
        yield return new WaitForSeconds(delayTime);
        yield break;
    }
    IEnumerator Bubble(int value = 0, float delayTime = 0, bool waitMovie = false)
    {
        Debug.Log("Bubble");
        yield return new WaitForSeconds(delayTime);
        switch (value)
        {
            case 0:
                BubbleEvent.Instance.DisplayBubbles(BubbleEvent.BubbleType.Door);
                break;
            case 1:
                BubbleEvent.Instance.DisplayBubbles(BubbleEvent.BubbleType.Repair);
                break;
            case 2:
                BubbleEvent.Instance.DisplayBubbles(BubbleEvent.BubbleType.Tana);
                break;
            case 3:
                BubbleEvent.Instance.DisplayBubbles(BubbleEvent.BubbleType.Dark);
                break;
            case 4:
                BubbleEvent.Instance.DisplayBubbles(BubbleEvent.BubbleType.Escape);
                break;
            case 5:
                BubbleEvent.Instance.DisplayBubbles(BubbleEvent.BubbleType.Power);
                break;
            default:
                Debug.Log("この吹き出しは未実装");
                break;
        }
        yield break;
    }
    IEnumerator ChangeTime(int value = 0, float delayTime = 0, bool waitMovie = false)
    {
        Debug.Log("ChangeTime");
        yield return new WaitForSeconds(delayTime);
        if (value == 0)
            FlagManager.Instance.ChegeFranPero(true);
        else
            FlagManager.Instance.ChegeFranPero(false);
        yield break;
    }
    // セーブの処理にも座標情報があるのでそこを流用予定
    IEnumerator WarpPositionPero(int value = 0, float delayTime = 0, bool waitMovie = false)
    {
        Debug.Log("WarpPero");
        yield return new WaitForSeconds(delayTime);
        yield break;
    }
    IEnumerator WarpPositionFran(int value = 0, float delayTime = 0, bool waitMovie = false)
    {
        Debug.Log("WarpFran");
        yield return new WaitForSeconds(delayTime);
        yield break;
    }
    IEnumerator DropItem(int value = 0, float delayTime = 0, bool waitMovie = false)
    {
        Debug.Log("DropItem");
        yield return new WaitForSeconds(delayTime);
        yield break;
    }
    IEnumerator PickupItem(int value = 0, float delayTime = 0, bool waitMovie = false)
    {
        Debug.Log("PickUpItem");
        yield return new WaitForSeconds(delayTime);
        yield break;
    }
    IEnumerator Tutorial(int value = 0, float delayTime = 0, bool waitMovie = false)
    {
        Debug.Log("Tutorial");
        yield return new WaitForSeconds(delayTime);
        switch (value)
        {
            case 0:
                break;
            case 1:
                break;
        }
        yield break;
    }
    IEnumerator Minigame1(int value = 0, float delayTime = 0, bool waitMovie = false)
    {
        Debug.Log("Minigame1");
        yield return new WaitForSeconds(delayTime);
        yield break;
    }
    IEnumerator Minigame2(int value = 0, float delayTime = 0, bool waitMovie = false)
    {
        Debug.Log("Minigame2");
        yield return new WaitForSeconds(delayTime);
        yield break;
    }
    IEnumerator PlayBGM(int value = 0, float delayTime = 0, bool waitMovie = false)
    {
        Debug.Log("BGM");
        yield return new WaitForSeconds(delayTime);
        switch (value)
        {
            case 0:
                SoundManager.Instance.PlayBGM(SoundManager.BGM_Name.BGM_00_Opening);
                break;
            case 1:
                SoundManager.Instance.PlayBGM(SoundManager.BGM_Name.BGM_01_Gray);
                break;
            case 2:
                SoundManager.Instance.PlayBGM(SoundManager.BGM_Name.BGM_02_Yellow);
                break;
            case 3:
                SoundManager.Instance.PlayBGM(SoundManager.BGM_Name.BGM_03_Red);
                break;
            case 4:
                SoundManager.Instance.PlayBGM(SoundManager.BGM_Name.BGM_04_Blue);
                break;
            case 5:
                SoundManager.Instance.PlayBGM(SoundManager.BGM_Name.BGM_05_Green);
                break;
            case 6:
                SoundManager.Instance.PlayBGM(SoundManager.BGM_Name.BGM_06_Past1);
                break;
            case 7:
                SoundManager.Instance.PlayBGM(SoundManager.BGM_Name.BGM_07_Past2);
                break;
            default:
                Debug.LogWarning(value +"番のBGMは未実装");
                break;
        }
        yield break;
    }
    IEnumerator PlaySE(int value = 0, float delayTime = 0, bool waitMovie = false)
    {
        Debug.Log("SE");
        yield return new WaitForSeconds(delayTime);
        switch (value)
        {
            case 0:
                SoundManager.Instance.PlaySE(SoundManager.SE_Name.SE_00_Alerm);
                break;
            case 1:
                SoundManager.Instance.PlaySE(SoundManager.SE_Name.SE_01_BreakWin);
                break;
            case 2:
                SoundManager.Instance.PlaySE(SoundManager.SE_Name.SE_02_Wator);
                break;
            default:
                Debug.LogWarning(value + "番のSEは未実装");
                break;
        }
        yield break;
    }
    #endregion
}


/// <summary>
/// キャラの調べる反応させる関数
/// </summary>
public interface ICheckable
{
    void Check();
}

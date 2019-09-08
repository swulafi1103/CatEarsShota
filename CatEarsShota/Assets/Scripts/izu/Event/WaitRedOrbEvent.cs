using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WaitRedOrbEvent : MonoBehaviour
{
    [SerializeField]
    private GimmickFlag_Map2 needFlag = GimmickFlag_Map2.G_25_RedOrbAnimation;
    [SerializeField]
    private EventName selectEvent = EventName.E24_Alone_start_past;

    void Start()
    {
        EventManager.Instance._callBack_useHashigo += ForceToPero;
    }
    public void ForceToPero()
    {
        if (FlagManager.Instance.CheckGimmickFlag(needFlag) == true)
        {
            EventManager.Instance.PlayEvent(selectEvent, gameObject);
            gameObject.SetActive(false);
        }
        if (FlagManager.Instance.CheckGimmickFlag(needFlag) == false)
        {
            StartCoroutine(ChangePero());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ForceToPero();
    }
    IEnumerator ChangePero()
    {
        while (!Fade.Instance.Fading == false)
            yield return null;
        Fade.Instance.StartFade(0.5f, Color.black);
        FlagManager.Instance.IsLockPast = true;
        FlagManager.Instance.ChageFranPero(true);
        EventManager.Instance._callBack_useHashigo = null;
        yield break;
    }
}
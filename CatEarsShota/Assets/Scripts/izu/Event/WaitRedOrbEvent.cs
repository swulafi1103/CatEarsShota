using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WaitRedOrbEvent : MonoBehaviour
{
    [SerializeField]
    private GimmickFlag_Map2 needFlag = GimmickFlag_Map2.G_25_RedOrbAnimation;
    [SerializeField]
    private GimmickFlag_Map2 standFlag = GimmickFlag_Map2.G_26_LevingMovie;
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
            FlagManager.Instance.SetGimmickFlag(standFlag);
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
        yield return new WaitForSeconds(0.5f);
        FlagManager.Instance.IsLockPast = true;
        FlagManager.Instance.ChageFranPero(true);
        Fade.Instance.StartFade(0.5f, Color.clear);
        EventManager.Instance._callBack_useHashigo = null;
        yield break;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitRedOrbEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance._callBack_useHashigo += ForceToPero;
    }

    public void ForceToPero()
    {
        StartCoroutine(ChangePero());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ForceToPero();
        EventManager.Instance._callBack_useHashigo = null;
        gameObject.SetActive(false);
    }

    IEnumerator ChangePero()
    {
        while (!Fade.Instance.Fading == false)
            yield return null;
        Fade.Instance.StartFade(0.5f, Color.black);
        FlagManager.Instance.IsLockPast = true;
        FlagManager.Instance.ChageFranPero(true);
        yield break;
    }
}

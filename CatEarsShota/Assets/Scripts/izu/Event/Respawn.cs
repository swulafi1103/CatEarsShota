using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField]
    private Transform respawnPos = default;

    void CallCortineFallDead()
    {
        StartCoroutine(FallDeadCor());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.transform.position.y > gameObject.transform.position.y)
            CallCortineFallDead();
    }

    private IEnumerator FallDeadCor()
    {
        Fade.Instance.StartFade(0.5f, Color.black);
        while (!Fade.Instance.Fading == false)
            yield return null;
        PlayerManager.Instance.Pero.transform.position = respawnPos.position;
        Fade.Instance.ClearFade(0.5f, Color.clear);
        TutorialContriller.Instance.SetTextWindow(6);
        yield break;
    }
}

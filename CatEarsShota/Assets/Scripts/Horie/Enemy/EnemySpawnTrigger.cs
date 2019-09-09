using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{
    [SerializeField]
    GimmickFlag_Map2 needGimmickFlag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != PlayerManager.Instance.Fran) return;
        if (!FlagManager.Instance.CheckGimmickFlag(needGimmickFlag)) return;

        EventManager.Instance.PlayEvent(EventName.E14_Enemy_exit_past, this.gameObject);

        gameObject.SetActive(false);
    }
}

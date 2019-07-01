using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandEnemyJudgement : MonoBehaviour
{
    LandEnemy parent;

    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.GetComponent<LandEnemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        parent.JudgementEnter(collision);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        parent.JudgementExit(collision);
    }
}

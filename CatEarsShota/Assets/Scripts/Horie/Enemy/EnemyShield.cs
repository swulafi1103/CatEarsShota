using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShield : MonoBehaviour
{
    LandEnemy parentEnemy;

    int Hp = 2;

    // Start is called before the first frame update
    void Start()
    {
        parentEnemy = transform.parent.GetComponent<LandEnemy>();
    }

    public void PlayerAttack() {
        Hp--;
        if (Hp == 0) {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag != "Player") return;
        parentEnemy.NearPlayer = true;
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.tag != "Player") return;
        parentEnemy.NearPlayer = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShield : MonoBehaviour
{
    LandEnemy parentEnemy;

    CapsuleCollider2D col;
    

    //int Hp = 2;

    // Start is called before the first frame update
    void Start()
    {
        parentEnemy = transform.parent.GetComponent<LandEnemy>();
        col = GetComponent<CapsuleCollider2D>();
    }

    //public void PlayerAttack() {
    //    Hp--;
    //    if (Hp == 0) {
    //        gameObject.SetActive(false);
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D collision) {
        if (!collision.enabled) return;
        if (collision.gameObject.tag == "Player") {
            //parentEnemy.NearPlayer = true;
            //当たったらワープ
            parentEnemy.FranWarp();
        }
        else if (collision.gameObject.tag == "Gimmick") {
            parentEnemy.CheckMove();
        }
        else if (collision.gameObject.tag == "Enemy") {
            parentEnemy.CheckMove();
        }
    }

    //private void OnCollisionExit2D(Collision2D collision) {
    //    if (collision.gameObject.tag != "Player") return;
    //    //parentEnemy.NearPlayer = false;
    //}

    public void SetDirection() {
        Vector2 offset = col.offset;
        offset.x = -offset.x;
        col.offset = offset;
    }
}

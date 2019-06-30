using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShield : MonoBehaviour
{
    LandEnemy parentEnemy;

    CapsuleCollider2D col;

    float colliderOffset = 0;

    int Hp = 2;

    // Start is called before the first frame update
    void Start()
    {
        parentEnemy = transform.parent.GetComponent<LandEnemy>();
        col = GetComponent<CapsuleCollider2D>();
        colliderOffset = col.offset.x;
        Mathf.Abs(colliderOffset);
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

    public void SetDirection(bool left) {
        if (left) {
            GetComponent<SpriteRenderer>().flipX = false;
            col.offset = new Vector2(colliderOffset, col.offset.y);
        }
        else {
            GetComponent<SpriteRenderer>().flipX = true;
            col.offset = new Vector2(-colliderOffset, col.offset.y);
        }
    }
}

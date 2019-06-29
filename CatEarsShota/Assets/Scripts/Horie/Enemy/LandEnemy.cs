using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandEnemy : Enemy
{

    [SerializeField, Range(0.5f, 10f)]
    float MoveSpeed = 1;

    [SerializeField]
    float[] maxMove = new float[2];
    
    float bulletHoseiX;

    [SerializeField]
    float bulletHoseiY = -0.5f;

    float moves = 1;
    
    EnemyBulletPool bulletBase;

    bool inPlayer = false;

    GameObject player;

    int PlayerLR;

    bool nearPlayer = false;
    /// <summary>
    /// 盾にプレイヤーが当たった時用
    /// </summary>
    public bool NearPlayer {
        set { nearPlayer = value; }
        get { return nearPlayer; }
    }

    protected override void Start() {
        base.Start();
        Hp = 1;
        nowHp = Hp;
        bulletBase = FindObjectOfType<EnemyBulletPool>();
        inPlayer = false;
        nearPlayer = false;
        bulletHoseiX = GetComponent<SpriteRenderer>().bounds.size.x / 2;
    }

    protected override void ResetData() {
        base.ResetData();
        inPlayer = false;
        nearPlayer = false;
    }

    protected override void Attack() {
        Timer += Time.deltaTime;
        if (Timer < attackInterval) return;
        Timer = 0;
        if (!inPlayer) return;
        PushBullet();

    }

    protected override void EnemyMove() {
        if (inPlayer) {
            GoToPlayer();
        }
        else {
            DefaltMove();
        }
    }

    /// <summary>
    /// 通常時の移動
    /// </summary>
    void DefaltMove() {
        float x = moves * MoveSpeed * Time.deltaTime;
        transform.position += new Vector3(x, 0, 0);

        if (transform.position.x <= maxMove[0]) {
            moves = 1;
        }
        if (transform.position.x >= maxMove[1]) {
            moves = -1;
        }
    }

    /// <summary>
    /// プレイヤー追尾
    /// </summary>
    void GoToPlayer() {
        if (nearPlayer) return;
        PlayerLR = transform.position.x < player.transform.position.x ? 1 : -1;
        float x = (float)PlayerLR * MoveSpeed * Time.deltaTime;
        transform.position += new Vector3(x, 0, 0);
    }

    private void PushBullet() {
        PlayerLR = transform.position.x < player.transform.position.x ? 1 : -1;
        int q = PlayerLR == 1 ? 0 : 180;
        Vector3 pos = transform.position + new Vector3(PlayerLR * bulletHoseiX, bulletHoseiY, 0);
        GameObject bullet = bulletBase.ReturnBullet(pos);
        bullet.transform.rotation = Quaternion.Euler(0, 0, q);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag != "Player") return;
        player = collision.gameObject;
        inPlayer = true;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag != "Player") return;
        player = null;
        inPlayer = false;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag != "Player") return;
        nearPlayer = true;
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.tag != "Player") return;
        nearPlayer = false;
    }
}

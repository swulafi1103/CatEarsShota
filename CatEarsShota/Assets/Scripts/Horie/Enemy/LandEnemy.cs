using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandEnemy : Enemy
{

    [SerializeField, Range(0.1f, 10f)]
    float MoveSpeed = 1;

    [SerializeField]
    float[] maxMove = new float[2];
    
    //float bulletHoseiX;

    //[SerializeField]
    //float bulletHoseiY = -0.5f;
    
    float moves = -1;
    
    //EnemyBulletPool bulletBase;

    //bool inPlayer = false;

    //GameObject player;

    [SerializeField, Range(0.1f, 5)]
    float WaitTime = 3f;

    //int PlayerLR;

    bool isAnim = false;

    [SerializeField]
    bool isLeft = true;

    SpriteRenderer[] bodyParts;

    //bool nearPlayer = false;
    /// <summary>
    /// 盾にプレイヤーが当たった時用
    /// </summary>
    //public bool NearPlayer {
    //    set { nearPlayer = value; }
    //    get { return nearPlayer; }
    //}

    EnemyShield shield;

    protected override void Awake() {
        base.Awake();
        //Hp = 1;
        //nowHp = Hp;
        //bulletBase = FindObjectOfType<EnemyBulletPool>();
        //inPlayer = false;
        //nearPlayer = false;
        //bulletHoseiX = GetComponent<SpriteRenderer>().bounds.size.x / 2;
        shield = GetComponentInChildren<EnemyShield>();
        isAnim = false;

        bodyParts = GetComponentsInChildren<SpriteRenderer>();
        moves = isLeft ? -1 : 1;
    }

    protected override void ResetData() {
        base.ResetData();
        //inPlayer = false;
        //nearPlayer = false;
        isAnim = false;
    }

    //protected override void Attack() {
    //    Timer += Time.deltaTime;
    //    if (Timer < attackInterval) return;
    //    Timer = 0;
    //    if (!inPlayer) return;
    //    PushBullet();

    //}

    protected override void EnemyMove() {
        //if (inPlayer) {
        //    GoToPlayer();
        //}
        //else {
        //}
        DefaltMove();
    }

    /// <summary>
    /// 通常時の移動
    /// </summary>
    void DefaltMove() {
        if (isAnim) return;
        float x = moves * MoveSpeed * Time.deltaTime;
        transform.position += new Vector3(x, 0, 0);

        if (transform.position.x <= FirstPos.x + maxMove[0]) {
            StartCoroutine(ChangeDirection(false));
            
        }
        if (transform.position.x >= FirstPos.x + maxMove[1]) {
            StartCoroutine(ChangeDirection(true));
        }
    }

    /// <summary>
    /// プレイヤー追尾
    /// </summary>
    //void GoToPlayer() {
    //    if (nearPlayer) return;
    //    int LR = transform.position.x < player.transform.position.x ? 1 : -1;
    //    if (LR != moves) {
    //        bool left = LR == -1;
    //        StartCoroutine(ChangeDirection(left));
    //        return;
    //    }
    //    if (isAnim) {
    //        StopAllCoroutines();
    //        isAnim = false;
    //    }
    //    moves = LR;
    //    float x = (float)moves * MoveSpeed * Time.deltaTime;
    //    transform.position += new Vector3(x, 0, 0);
    //}

    IEnumerator ChangeDirection(bool left) {
        if (isAnim) yield break;
        isAnim = true;
        int lr = left ? -1 : 1;
        moves = lr;
        isLeft = left;
        SetDirection(left);
        isAnim = false;
        yield break;
    }

    //private void PushBullet() {
    //    if (isAnim || nearPlayer) return;
    //    int q = moves == 1 ? 0 : 180;
    //    Vector3 pos = transform.position + new Vector3(moves * bulletHoseiX, bulletHoseiY, 0);
    //    GameObject bullet = bulletBase.ReturnBullet(pos);
    //    bullet.transform.rotation = Quaternion.Euler(0, 0, q);
    //}
    
    //public void JudgementEnter(Collider2D collision) {
    //    if (collision.gameObject.tag != "Player") return;
    //    player = collision.gameObject;
    //    inPlayer = true;
    //}
    
    //public void JudgementExit(Collider2D collision) {
    //    if (collision.gameObject.tag != "Player") return;
    //    player = null;
    //    inPlayer = false;
    //}

    private void OnCollisionEnter2D(Collision2D collision) {
        if (!collision.enabled) return;
        if (collision.gameObject == PlayerManager.Instance.Fran) {
            //nearPlayer = true;

            //当たったらワープ
            EnemyController.Instance.RespawnFran();
        }
        else if(collision.gameObject.tag == "Gimmick") {
            CheckMove();
        }
        else if (collision.gameObject.tag == "Enemy") {
            CheckMove();
        }
    }

    public void CheckMove() {
        StartCoroutine(ChangeDirection(!isLeft));
    }

    //private void OnCollisionExit2D(Collision2D collision) {
    //    if (collision.gameObject.tag != "Player") return;
    //    //nearPlayer = false;
    //}


    /// <summary>
    /// 方向転換
    /// </summary>
    /// <param name="left"></param>
    private void SetDirection(bool left) {
        //if (left) {
        //    GetComponent<SpriteRenderer>().flipX = false;
        //}
        //else {
        //    GetComponent<SpriteRenderer>().flipX = true;
        //}

        foreach(SpriteRenderer parts in bodyParts) {
            Vector3 pos = parts.transform.localPosition;
            Debug.Log(pos);
            pos.x = -pos.x;
            parts.transform.localPosition = pos;

            parts.flipX = left;
        }

        if (shield == null) return;
        shield.SetDirection();
    }
}

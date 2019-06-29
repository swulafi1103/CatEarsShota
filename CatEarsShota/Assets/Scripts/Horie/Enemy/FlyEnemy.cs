using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : Enemy
{
    GameObject[] lasers;

    int laserCount = 0;

    [SerializeField, Range(0.5f, 10f)]
    float MoveSpeed = 1;

    [SerializeField]
    float[] maxMove = new float[2];

    float moves = 1;

    protected override void Start() {
        base.Start();
        Hp = 1;
        nowHp = Hp;
        laserCount = 0;
        EnemyLaser[] child = GetComponentsInChildren<EnemyLaser>();
        lasers = new GameObject[child.Length];
        for (int i = 0; i < child.Length; i++) {
            lasers[i] = child[i].gameObject;
            if (i == 0) {
                lasers[i].SetActive(true);
            }
            else {
                lasers[i].SetActive(false);
            }
        }
    }

    protected override void ResetData() {
        base.ResetData();
        laserCount = 0;
    }

    protected override void Attack() {
        Timer += Time.deltaTime;
        if (Timer < attackInterval) return;
        Timer = 0;
        SetLaser();
    }
    
    protected override void EnemyMove() {
        float x = moves * MoveSpeed * Time.deltaTime;
        transform.position += new Vector3(x, 0, 0);

        if (transform.position.x <= maxMove[0]) {
            moves = 1;
        }
        if (transform.position.x >= maxMove[1]) {
            moves = -1;
        }
    }


    void SetLaser() {
        laserCount++;
        if (laserCount == 2) laserCount = 0;
        for(int i = 0; i < lasers.Length; i++) {
            if (i == laserCount) {
                lasers[i].SetActive(true);
            }
            else {
                lasers[i].SetActive(false);
            }
        }
    }
}

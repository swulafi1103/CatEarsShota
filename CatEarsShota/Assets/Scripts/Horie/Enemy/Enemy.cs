using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Enemy : MonoBehaviour
{
    protected int Hp;
    protected int nowHp = 0;

    [SerializeField]
    protected float attackInterval = 2;

    protected float Timer;
    

    Vector3 FirstPos;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        gameObject.SetActive(true);
        Timer = 0;
        FirstPos = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        Attack();
        EnemyMove();
        if (Input.GetKeyDown(KeyCode.S)) { PlayerAttack(); }    //debug
    }

    protected virtual void Attack() {
    }

    protected virtual void EnemyMove() {
        
    }

    public void PlayerAttack() {
        nowHp--;
        //Debug.Log(this.gameObject.name + " : " + nowHp);
        if (nowHp == 0) {
            gameObject.SetActive(false);
        }
    }

    public void SetStartPos() {
        ResetData();
    }

    protected virtual void ResetData() {
        transform.position = FirstPos;
        nowHp = Hp;
    }
}

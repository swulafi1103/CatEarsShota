using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerraultMove : MonoBehaviour
{
    /*
     * 移動                (画像の反転)
     * ジャンプ            (2回まで)
     * アニメーション      (待機・移動・ジャンプ・その他)
     * アクション          (調べる)
     * ステータス          (HP・ATK ここに書くべきではない気がする)
     */

    
    [SerializeField]
    private float moveSpeed;            //  移動速度
    [SerializeField]
    private float moveForceMultiplier;  //  移動速度の入力に対する追従度

    Rigidbody2D _rb;
    private float _horizontalInput;
    private float _verticalInput;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        Vector2 moveVector = _rb.velocity;
        moveVector.x = moveSpeed * _horizontalInput;
        //moveVector.y = moveSpeed * _verticalInput;

        _rb.AddForce(moveForceMultiplier * (moveVector - _rb.velocity));
    }
}

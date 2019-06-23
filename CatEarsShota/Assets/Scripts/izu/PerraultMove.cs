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

    /*
     * 着地していれば
     * 2回飛べる
     * 
     */

    private Rigidbody2D rb;
    private Vector2 localScale;
    private bool isGrounded = true;
    private bool isJumping = false;
    private bool isJumpingCheck = true;
    private float jumpTimeCounter;
    private float jumpTime = 0.25f;
    private float _jumpPower;
    [SerializeField]
    private LayerMask platformLayer;

    [SerializeField]
    ContactFilter2D filter2d;

    InputManager inputManager;
    PlayerManager playerManager;

    [SerializeField]
    private float moveSpeed;            //  移動速度
    [SerializeField]
    private float moveForceMultiplier;  //  移動速度の入力に対する追従度

    void Awake()
    {
        localScale = transform.localScale;
        jumpTimeCounter = jumpTime;
    }


    void Start()
    {
        playerManager = PlayerManager.Instance;
        inputManager = InputManager.Instance;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //isGrounded = Physics2D.Linecast(transform.position - transform.up * 0.4f, transform.position - transform.up * 0.6f, platformLayer);
        isGrounded = rb.IsTouching(filter2d);
        Debug.Log("着地：" + isGrounded);
        //_horizontalInput = Input.GetAxis("Horizontal");
        //_verticalInput = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {

        //_rb.AddForce(moveForceMultiplier * (moveVector - _rb.velocity));

        if (inputManager.MoveKey != 0)
        {
            // 向きを変える
            //localScale.x = inputManager.MoveKey * -1 * transform.localScale.x;
            //transform.localScale = localScale;
        }

        //  着地中
        if (isGrounded)
        {
            rb.velocity = new Vector2(inputManager.MoveKey * playerManager.MoveSpeed, 0);

            if (isJumpingCheck && inputManager.JumpKey != 0)
            {
                jumpTimeCounter = jumpTime;
                isJumpingCheck = false;
                isJumping = true;
                _jumpPower = playerManager.JumpPower;
            }
        }
        else
        {
            if (inputManager.JumpKey == 0)
            {
                isJumping = false;
            }
            if (!isJumping)
            {
                rb.velocity = new Vector2(inputManager.MoveKey * playerManager.JumpMoveSpeed, Physics.gravity.y * playerManager.GravityRate);
            }
        }


        //  ジャンプできる時間
        if (isJumping)
        {
            jumpTimeCounter -= Time.deltaTime;

            if (inputManager.JumpKey == 2)
            {
                _jumpPower -= 0.2f;
                rb.velocity = new Vector2(inputManager.MoveKey * playerManager.JumpMoveSpeed, 1 * _jumpPower);
            }
            if (jumpTimeCounter < 0)
            {
                isJumping = false;
            }
        }

        if (inputManager.JumpKey == 0)
        {
            isJumpingCheck = true;
        }
    }


}

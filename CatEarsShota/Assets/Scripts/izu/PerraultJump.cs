using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerraultJump : MonoBehaviour
{
    [SerializeField]
    private float jumpPower;
    [SerializeField]
    ContactFilter2D filter2d;
    Rigidbody2D _rb;
    bool _isGround;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        Jump();
    //    }
    //    _isGround = _rb.IsTouching(filter2d);
    //    //Debug.Log("着地：" + _isGround);
    //}
    private void Jump()
    {
        _rb.AddForce(Vector2.up * jumpPower);
    }
}

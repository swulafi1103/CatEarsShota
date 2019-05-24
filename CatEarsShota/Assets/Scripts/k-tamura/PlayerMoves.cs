using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerMoves : MonoBehaviour
{
    [Header("PlayerMoveParamater")]
    [SerializeField]
    [Range(1,50)]
    private float MoveSpeed = 0.1f;
    [SerializeField]
    [Range(1,500)]
    private float JumpPower = 0.1f;
    private Rigidbody2D rb2d;
    private Vector2 AddVector = new Vector2(1.00f,1.00f);
    private Animator anim;
    // Start is called before the first frame update
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }
    /// <summary>
    /// Character Move this instance.
    /// </summary>
    private void Move()
    {
        //RightMove
        if (Input.GetKey(KeyCode.D))
        {
            AddVector.x = MoveSpeed;
            rb2d.AddForce(AddVector);
            anim.SetBool("SetWaitAnimator", false);
            anim.SetBool("SetWalkAnimator", true);
        }
        //LeftMove
        else if (Input.GetKey(KeyCode.A))
        {
            AddVector.x = -1 * MoveSpeed;
            rb2d.AddForce(AddVector);
            anim.SetBool("SetWaitAnimator", false);
            anim.SetBool("SetWalkAnimator", true);
        }
        else
        {
            //AddVector.x = 1;
            //AddVector.y = 1;
            anim.SetBool("SetWaitAnimator", true);
            anim.SetBool("SetWalkAnimator", false);
        }

    }
    /// <summary>
    /// Character Jump this instance.
    /// </summary>
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddVector.y *= JumpPower;
            rb2d.AddForce(AddVector);
        }
        else
        {
            AddVector.y = 1;
        }
    }
}

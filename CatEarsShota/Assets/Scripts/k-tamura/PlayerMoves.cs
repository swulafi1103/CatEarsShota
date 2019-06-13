using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerMoves : MonoBehaviour
{
    [Header("PlayerMoveParamater")]
    [SerializeField]
    [Range(1, 50)]
    private float MoveSpeed = 0.1f;
    [SerializeField]
    [Range(1, 500)]
    private float JumpPower = 0.1f;
    private Rigidbody2D rb2d;
    private Vector2 WalkVector = new Vector2(1.00f, 1.00f);
    private Vector2 JumpVector = new Vector2(1.00f, 1.00f);
    private Animator anim;
    private int JumpNum = 0;
    private bool Ground = false;
    public bool Notmoves = false;
    Vector3 scale;
    [SerializeField]
    GameObject MinigameMgr;
    [SerializeField]
    GameObject fran;
  


    // Start is called before the first frame update
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        scale = transform.localScale;
    }
    void Start()
    {
        MinigameMgr = GameObject.Find("MiniGameCanvas");
    }

    // Update is called once per frame
    void Update()
    {

        Move();
        Jump();
        Action();
    }
    /// <summary>
    /// Character Move this instance.
    /// </summary>
    private void Move()
    {
        //RightMove
        if (Notmoves == false)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (scale.x > 0)
                {
                    scale.x *= -1;
                    transform.localScale = scale;
                }
                WalkVector.x = MoveSpeed;
                rb2d.AddForce(WalkVector - rb2d.velocity, ForceMode2D.Force);
                anim.SetBool("SetWaitAnimator", false);
                anim.SetBool("SetWalkAnimator", true);
            }
            //LeftMove

            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (scale.x < 0)
                {
                    scale.x *= -1f;
                    transform.localScale = scale;
                }
                WalkVector.x = -1 * MoveSpeed;
                rb2d.AddForce(WalkVector - rb2d.velocity, ForceMode2D.Force);
                anim.SetBool("SetWaitAnimator", false);
                anim.SetBool("SetWalkAnimator", true);
            }
            else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
            {
                anim.SetBool("SetWaitAnimator", true);
                anim.SetBool("SetWalkAnimator", false);
            }
        }


    }
    /// <summary>
    /// Character Jump this instance.
    /// </summary>
    void Jump()
    {
        if (Notmoves == false)
        {
            if (Ground == true)
            {


                if (Input.GetKeyDown(KeyCode.Space))
                {

                    if (JumpNum < 2)
                    {
                        JumpNum++;
                        JumpVector.y += JumpPower;
                        rb2d.AddForce(JumpVector);
                        Debug.Log(JumpNum);
                    }

                }
                else
                {

                    JumpVector.y = 1;
                }
            }
            else
            {
                Ground = false;
                JumpNum = 0;
            }
        }
    }
    /// <summary>
    /// Action
    /// </summary>
    void Action()
    {
        if (Input.GetKeyDown(KeyCode.S))//攻撃
        {
            anim.SetTrigger("AttackTrigger");
        }
        if (Input.GetKeyDown(KeyCode.A))//調べ
        {

        }
        if (Input.GetKeyDown(KeyCode.D))//アイテム欄を開く
        {

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") Ground = true;
        JumpNum = 0;
        Debug.Log(Ground);
        if (collision.gameObject.tag == "MiniGames")
        {
            if (Input.GetKeyDown(KeyCode.A))//調べ
            {
                Notmoves = true;
                MinigameMgr.GetComponent<MiniGameManager>().TouchGenerator();
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                MinigameMgr.GetComponent<MiniGameManager>().StartMiniGame();
            }
        }
    }
}

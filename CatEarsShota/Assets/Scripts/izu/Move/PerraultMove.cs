using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerraultMove : MonoBehaviour
{
    [Header("PerraultMoveParamater")]
    [SerializeField, Range(1, 50)]
    private float MoveSpeed = 13f;
    [SerializeField, Range(1, 15)]
    private float MaxSpeed = 4f;
    [SerializeField, Range(0.01f, 0.5f)]
    private float brakePower = 0.05f;
    [SerializeField, Range(1, 500)]
    private float JumpPower = 250f;
    [SerializeField, Range(0.01f, 3)]
    private float gravityRate = 1f;
    private Rigidbody2D rb;
    private Vector2 WalkVector = new Vector2(0, 0);
    //private Vector2 JumpVector = new Vector2(0, 0);
    private Animator anim;
    private int JumpNum = 0;
    private bool isGround = false;
    private bool isJump = false;
    public bool isNotmoves = false;
    Vector3 scale;
    [SerializeField]
    private ContactFilter2D filter2d;
    private GameObject MinigameMgr;
    [SerializeField]
    private List<GameObject> checkObjects = new List<GameObject>();

    public GameObject Pants;
    public GameObject FrontShadow;
    public GameObject BackShadow;
    public AudioSource[] SE;
    private Animator animpants;

    private void Awake()
    {
        FrontShadow.SetActive(false);
        BackShadow.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        animpants = Pants.GetComponent<Animator>();
        scale = transform.localScale;
        anim.SetBool("FaceLeft", true);
    }
    void Start()
    {
        MinigameMgr = GameObject.Find("MiniGameCanvas");
    }

    void Update()
    {
        //  着地チャック
        CheckGround();
        //  操作停止中ではないか
        if (CanMove())
        {
            Jump();
            Move();
            Action();
        }
        else
            Breaking();
    }

    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {
        //RightMove
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (scale.x > 0)
            {
                anim.SetBool("FaceLeft", false);
                scale.x *= -1;
                transform.localScale = scale;
            }
            WalkVector.x = MoveSpeed;
            rb.AddForce(WalkVector - rb.velocity, ForceMode2D.Force);
            //  最高速度に制限
            rb.velocity = new Vector2(Mathf.Min(MaxSpeed, rb.velocity.x), rb.velocity.y);
            anim.SetBool("SetWaitAnimator", false);
            anim.SetBool("SetWalkAnimator", true);
            animpants.SetBool("SetWaitAnimator", false);
            animpants.SetBool("SetWalkAnimator", true);
        }
        //LeftMove
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (scale.x < 0)
            {
                anim.SetBool("FaceLeft", true);
                scale.x *= -1f;
                transform.localScale = scale;
            }
            WalkVector.x = -1 * MoveSpeed;
            rb.AddForce(WalkVector - rb.velocity, ForceMode2D.Force);
            //  最高速度に制限
            rb.velocity = new Vector2(Mathf.Max(-MaxSpeed, rb.velocity.x), rb.velocity.y);
            anim.SetBool("SetWaitAnimator", false);
            anim.SetBool("SetWalkAnimator", true);
            animpants.SetBool("SetWaitAnimator", false);
            animpants.SetBool("SetWalkAnimator", true);
        }
        if (Mathf.Abs(rb.velocity.x) > 0 && (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)))
        {
            Breaking();
        }
    }
    void Breaking()
    {
            if (Mathf.Abs(rb.velocity.x) >= 0.1f)
            {
                rb.velocity = new Vector2(rb.velocity.x * (1 - brakePower), rb.velocity.y);
            }
            else if (Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                //  完全に停止
                rb.velocity = new Vector2(0, rb.velocity.y);
                // Idle状態に
                anim.SetBool("SetWaitAnimator", true);
                anim.SetBool("SetWalkAnimator", false);
                animpants.SetBool("SetWaitAnimator", true);
                animpants.SetBool("SetWalkAnimator", false);
            }
        
    }

    /// <summary>
    /// ジャンプ
    /// </summary>
    void Jump()
    {
        //  地面に触れているか
        if (isGround)
        {
            JumpNum = 0;
            isJump = false;

            anim.SetTrigger("GroundTrigger");
            anim.ResetTrigger("JumpTrigger");
            animpants.SetTrigger("GroundTrigger");
            animpants.ResetTrigger("JumpTrigger");
        }
        //  ジャンプせずに足場から離れたとき
        if (!isGround && !isJump)
        {
            JumpNum = 1;

        }
        //  2回ジャンプしてない状態でスペースが押されたとき
        if (Input.GetKeyDown(KeyCode.Space) && JumpNum < 2)
        {
            JumpNum++;
            if (JumpNum == 2)
            {
                anim.SetTrigger("DoubleJumpTrigger");
                animpants.SetTrigger("DoubleJumpTrigger");
            }
            isJump = true;
            Vector2 JumpVector = new Vector2(0, JumpPower);
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(JumpVector);
            anim.ResetTrigger("GroundTrigger");
            anim.SetTrigger("JumpTrigger");
            animpants.ResetTrigger("GroundTrigger");
            animpants.SetTrigger("JumpTrigger");
        }
        //  落下のスピードの調整
        else
        {
            if (rb.velocity.y < -0.01f)
            {
                float gravity = rb.velocity.y * gravityRate;
                rb.AddForce(new Vector2(rb.velocity.x, gravity));
            }
        }

    }

    //  移動できるか
    bool CanMove()
    {
        if (FlagManager.Instance.IsOpenUI)
        {
            return false;
        }
        if (FlagManager.Instance.IsEventing)
        {
            return false;
        }
        if (FlagManager.Instance.IsMovie)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// 攻撃・調べる・アイテム欄
    /// </summary>
    void Action()
    {
        if (Input.GetKeyDown(KeyCode.S))//攻撃
        {
            anim.SetTrigger("AttackTrigger");
            animpants.SetTrigger("AttackTrigger");
        }
        if (Input.GetKeyDown(KeyCode.A))//調べ
        {
            if (checkObjects.Count != 0)
            {
                //  ギミックの発動
                Debug.Log("ギミック作動");
                checkObjects[0].GetComponent<ICheckable>().Check();
                checkObjects.Remove(checkObjects[0]);
            }
        }
        if (Input.GetKeyDown(KeyCode.D))//アイテム欄を開く
        {
            ItemManager.Instance.SetItemUI();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("ChangeTime");
            //  バンドを拾うまで過去に飛べないように
            if (!FlagManager.Instance.CheckGimmickFlag(GimmickFlag.G_04_TouchYellowGenerator))
                return;
            if (FlagManager.Instance.IsLockPast != true)
                FlagManager.Instance.ChangeTimeFade();
            else
                TutorialContriller.Instance.SetTextWindow(0);
        }
    }

    //  地面に触れているか
    void CheckGround()
    {
        isGround = rb.IsTouching(filter2d);
        if (!isGround)
        {
            anim.ResetTrigger("GroundTrigger");
            animpants.ResetTrigger("GroundTrigger");
        }
        anim.SetBool("SetFloatAnimator", !isGround);
        animpants.SetBool("SetFloatAnimator", !isGround);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //  要素の追加
        if (collision.gameObject.GetComponent<ICheckable>() != null && collision.gameObject.GetComponent<EventBase>().isFinish != true)
        {
            if (!checkObjects.Contains(collision.gameObject))
            {
                checkObjects.Add(collision.gameObject);
            }
        }
    }
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    //  要素の追加
    //    if (collision.gameObject.GetComponent<ICheckable>() != null && collision.gameObject.GetComponent<EventBase>().isFinish != true)
    //    {
    //        if (!checkObjects.Contains(collision.gameObject))
    //        {
    //            checkObjects.Add(collision.gameObject);
    //        }
    //    }
    //    if (collision.gameObject.GetComponent<ICheckable>() != null && collision.gameObject.GetComponent<EventBase>().isFinish)
    //    {
    //        if (checkObjects.Contains(collision.gameObject))
    //        {
    //            checkObjects.Remove(collision.gameObject);
    //        }
    //    }
    //}
    private void OnTriggerExit2D(Collider2D collision)
    {
        //  要素の破棄
        checkObjects.Remove(collision.gameObject);
    }
    public void PeroSe(int index)
    {
        switch (index){
             case 0:
                SoundManager.Instance.PlaySE(SoundManager.SE_Name.SE_17_PeroWalk, 1f);
                break;
            case 1:
                SoundManager.Instance.PlaySE(SoundManager.SE_Name.SE_18_PeroJump, 1f);
                SoundManager.Instance.PlaySE(SoundManager.SE_Name.SE_19_PeroJump2, 1f);
                break;
            default:
                break;
        }
    }

    public void SetShadowForm(bool OnOff)
    {
        FrontShadow.SetActive(OnOff);
        BackShadow.SetActive(OnOff);
    }
}

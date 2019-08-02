using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NowMushroom : MonoBehaviour
{
    Animator Anim;

    float miniSize;

    bool IsBig = false;

    bool timeStart = false;

    float Timer = 0;

    [SerializeField]
    float MaxTime = 5;

    // Start is called before the first frame update
    void Start()
    {
        SetData();
    }

    // Update is called once per frame
    void Update() {
        if (!timeStart) return;
        Timer += Time.deltaTime;
        if (Timer >= MaxTime) {
            ToSmall();
        }
    }

    void SetData() {
        Anim = GetComponent<Animator>();
        float halfY = GetComponent<SpriteRenderer>().bounds.size.y / 2;
        miniSize = transform.position.y + halfY;
        IsBig = false;
        timeStart = false;
        Timer = 0;
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag != "Player") return;
        if (timeStart) {
            timeStart = false;
            Timer = 0;
        }
        ToBig(collision);
    }

    void ToBig(Collision2D collision) {
        if (collision.transform.position.y < miniSize) return;
        if (IsBig) return;
        Anim.SetTrigger("ToBig");
        IsBig = true;
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.tag != "Player") return;
        if (!IsBig) return;
        timeStart = true;
        Timer = 0;
    }

    void ToSmall() {
        timeStart = false;
        Timer = 0;
        Anim.SetTrigger("ToSmall");
        IsBig = false;
    }

    public void SetMush()
    {
        gameObject.SetActive(true);
    }
}

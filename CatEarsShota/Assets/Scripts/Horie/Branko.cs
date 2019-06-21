using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branko : MonoBehaviour
{
    LineRenderer[] lines;
    Vector3[] linePos;

    [SerializeField]
    Vector3[] MaxPos = new Vector3[2];

    [SerializeField]
    float moveTime = 3;

    bool IsRight = true;

    float timer = 0;

    float harfSize = 0;

    // Start is called before the first frame update
    void Start()
    {
        SetData();
    }

    // Update is called once per frame
    void Update()
    {
        MoveBuranko();
    }

    void SetData() {
        IsRight = true;
        lines = GetComponentsInChildren<LineRenderer>();
        linePos = new Vector3[lines.Length];

        for(int i = 0; i < lines.Length; i++) {
            linePos[i] = lines[i].transform.position;
            lines[i].SetPosition(0, linePos[i]);
            lines[i].SetColors(Color.white, Color.white);
        }
        transform.position = MaxPos[0];

        harfSize = GetComponent<SpriteRenderer>().bounds.size.x / 2;
    }

    void MoveBuranko() {
        timer += Time.deltaTime;
        if (timer >= moveTime) {
            timer = 0;
            IsRight = !IsRight;
        }

        float t = timer / moveTime;
        
        Vector3 pos;
        
        if (IsRight) {
            pos = Vector3.Slerp(MaxPos[0], MaxPos[1], t);
        }
        else {
            pos = Vector3.Slerp(MaxPos[1], MaxPos[0], t);
        }

        transform.position = pos;

        Vector3 line1 = pos;
        line1.x -= harfSize;

        lines[0].SetPosition(1, line1);

        Vector3 line2 = pos;
        line2.x += harfSize;

        lines[1].SetPosition(1, line2);
    }
}

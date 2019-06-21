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

    float harfSizeX = 0;
    float harfSizeY = 0;

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
        }
        transform.position = MaxPos[0];

        harfSizeX = GetComponent<SpriteRenderer>().bounds.size.x / 2;
        harfSizeY = GetComponent<SpriteRenderer>().bounds.size.y / 2;
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
        line1.x -= (harfSizeX - 0.1f);
        line1.y += harfSizeY - 0.1f;

        lines[0].SetPosition(1, line1);

        Vector3 line2 = pos;
        line2.x += (harfSizeX - 0.1f);
        line2.y += harfSizeY - 0.1f;

        lines[1].SetPosition(1, line2);
    }
}

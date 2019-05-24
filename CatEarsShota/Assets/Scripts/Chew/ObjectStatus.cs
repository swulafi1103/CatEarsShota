using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStatus : MonoBehaviour
{
    public int index = 0;
    public Sprite[] status = new Sprite[2];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (index > -1 && index < 2)
            gameObject.GetComponent<SpriteRenderer>().sprite = status[index];
        else
            Debug.Log("sprite index error");
    }
}

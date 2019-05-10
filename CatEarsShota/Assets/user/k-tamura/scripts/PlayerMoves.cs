using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoves : MonoBehaviour
{
    [SerializeField]
    private float MoveSpeed = 0.1f;
    private Rigidbody2D rb2d;
    private Vector2 AddVector = new Vector2(0.00f,0.00f);
    // Start is called before the first frame update
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //RightMove
        if (Input.GetKeyDown(KeyCode.A))
        {
            AddVector.x *= MoveSpeed;
            rb2d.AddForce(AddVector);
        }
        //LeftMove
        if (Input.GetKeyDown(KeyCode.B))
        {
            AddVector.x *= -MoveSpeed;
            rb2d.AddForce(AddVector*-1);
        }
        AddVector = Vector2.zero;
    }
}

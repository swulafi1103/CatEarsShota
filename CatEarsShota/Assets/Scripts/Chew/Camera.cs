using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject Player;
    public float RangeToPlayer = 10.0f;
    public float[] CameraLimit = new float[2];

    private Vector3 rangeToTarget;
    // Start is called before the first frame update
    void Start()
    {
        rangeToTarget = new Vector3(0,0,RangeToPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < CameraLimit[1] && transform.position.x > CameraLimit[0])
            transform.position = Vector3.Lerp(transform.position, Player.transform.position - rangeToTarget, Time.deltaTime);
        else{
            int tmpindex = transform.position.x > CameraLimit[1] ? 1 : 0;
            transform.position = new Vector3(CameraLimit[tmpindex], transform.position.y, transform.position.z);
        }
        
    }
}

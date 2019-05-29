using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 過去モードFran用現在PerraultPositionSync
/// </summary>
public class PlayerPositionSync : MonoBehaviour
{
    [SerializeField]
    Vector3 PerraultPositon;
    [SerializeField]
    GameObject Perrault;
    [SerializeField]
    GameObject Fran;
    MapStatus _mapStatus;
    [SerializeField]
    GameObject CameraMain;
    [SerializeField]
    Vector3 past;
    public bool PastMode;

    private void Start()
    {
        Perrault = GameObject.Find("Perrault");
        Fran = GameObject.Find("fran");
        _mapStatus = GetComponent<MapStatus>();
        CameraMain = GameObject.Find("Main Camera");
        past.x =40f;
        past.y = 1.1f;
    }


    void Update()
    {


        if (Input.GetKeyDown(KeyCode.F) && PastMode==false)
        {
            Fran.transform.position = PerraultPositon;
            CameraMain.GetComponent<Camera>().PastMode = true;
            PastMode = true;

        }
        else if (Input.GetKeyDown(KeyCode.F) && PastMode == true)
        {
            PastMode = false;
            Perrault.transform.position = Fran.transform.position-past;
            CameraMain.GetComponent<Camera>().PastMode = false;
        }
        if (PastMode)
        {
            Perrault.transform.position = Fran.transform.position - past;
        }
        else
        {
            Fran.transform.position = Perrault.transform.position + past;
            PerraultPositon = Fran.transform.position;
        }

    }
}

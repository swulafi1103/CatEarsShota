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

    private void Start()
    {
        Perrault = GameObject.Find("Perrault");
        Fran = GameObject.Find("fran");
        _mapStatus = GetComponent<MapStatus>();
        CameraMain = GameObject.Find("Main Camera");
        past.x =40f;
    }


    void Update()
    {
        PerraultPositon = Perrault.transform.position;
        if (Input.GetKeyDown(KeyCode.F) && Perrault.active == true)
        {
            Fran.transform.position = PerraultPositon+past;
            CameraMain.GetComponent<Camera>().PastMode = true;
            Perrault.SetActive(false);

        }
        else if (Input.GetKeyDown(KeyCode.F) && Perrault.active == false)
        {
            Perrault.SetActive(true);
            Perrault.transform.position = Fran.transform.position-past;
            CameraMain.GetComponent<Camera>().PastMode = false;
        }

    }
}
